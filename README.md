## Summary

This is the Unity purchase SDK of adjust™. It supports iOS and Android targets. You can read more about adjust™ at 
[adjust.com].

## Table of contents

* [Basic integration](#basic-integration)
    * [Get the SDK](#sdk-get)
    * [Add the SDK to your project](#sdk-add)
    * [Integrate the SDK into your app](#sdk-integrate)
    * [Adjust Purchase logging](#sdk-logging)
* [Verify your purchases](#verify-purchases)
    * [Make the verification request](#verification-request)
    * [Process verification response](#verification-response)
    * [Track your verified purchases](#track-purchases)
* [Best practices](#best-practices)
* [License](#license)

## <a id="basic-integration">Basic integration

In order to use the adjust purchase SDK, you must **first enable fraud prevention** for your app. You can find the 
instructions in our official [fraud prevention guide][fraud-prevention] documentation.

These are the minimal steps required to integrate the adjust SDK into your Unity project.

### <a id="sdk-get">Get the SDK

Download the latest version from our [releases page][releases]. In there you will find two Unity packages:

- AdjustPurchase_vX.Y.Z_Unity_4.unitypackage - Use this package if you are using Unity IDE version 4.
- AdjustPurchase_vX.Y.Z_Unity_5.unitypackage - Use this package if you are using Unity IDE version 5.

### <a id="sdk-add">Add the SDK to your project

Open your project in the Unity Editor and navigate to `Assets → Import Package → Custom Package`. Select the downloaded 
Unity package file.

![][import_package]

### <a id="sdk-integrate">Integrate the SDK into your app

Add the prefab located at `Assets/AdjustPurchase/AdjustPurchase.prefab` to the first scene.

Edit the parameters of the `AdjustPurchase` script in the Inspector menu of the added prefab.

![][editor]

You have the possibility to set up the following options on the Adjust prefab:

* [App Token](#app-token)
* [Log Level](#sdk-logging)
* [Environment](#environment)

<a id="app-token">Replace `{YourAppToken}` with your App Token. You can find it in your [dashboard].

<a id="environment">Depending on whether or not you build your app for testing or for production, you must define 
`Environment` with one of these values:

```
'Sandbox'
'Production'
```

**Important:** This value should be set to `Sandbox` if and only if you or someone else is testing your app. Make sure to 
set the environment to `Production` just before you publish the app. Set it back to `Sandbox` when you start testing it 
again.

We use this environment to distinguish between real traffic and artificial traffic from test devices. It is very important 
that you keep this value meaningful at all times: we are using it to determine whether your purchases should be verified on 
Apple/Google sandbox or production servers!

### <a id="sdk-logging">Adjust Purchase logging

You can increase or decrease the amount of logs you see by changing the value of `Log Level` to one of the following:

- `Verbose` - enable all logging
- `Debug` - enable more logging
- `Info` - the default
- `Warn` - disable info logging
- `Error` - disable warnings as well
- `Assert` - disable errors as well

## <a id="verify-purchases">Verify your purchases

### <a id="verification-request">Make the verification request

In order to verify in-app purchases, you need to call the `VerifyPurchaseiOS` method on the `AdjustPurchase` instance for 
purchase verification on iOS, or the `VerifyPurchaseAndroid` method for purchase verification on Android. Please make sure 
to call this method after the transaction has been finished and your item purchased.

```csharp
using com.adjust.sdk.purchase;

// ...

// Purchase verification request on iOS.
AdjustPurchase.VerifyPurchaseiOS ("{Receipt}", "{TransactionID}", "{ProductId}", VerificationInfoDelegate);

// Purchase verification request on Android.
AdjustPurchase.VerifyPurchaseAndroid ("{ItemSKU}", "{ItemToken}", "{DeveloperPayload}", VerificationInfoDelegate);

// ...

private void VerificationInfoDelegate (ADJPVerificationInfo verificationInfo)
{
    Debug.Log ("Verification info arrived to unity callback!");
    Debug.Log ("Message: " + verificationInfo.Message);
    Debug.Log ("Status code: " + verificationInfo.StatusCode);
    Debug.Log ("Verification state: " + verificationInfo.VerificationState);
}
```

### <a id="verification-response">Process verification response

As described in the code above, you need to pass a method which is going to process the verification response to the 
`VerifyPurchaseiOS` or `VerifyPurchaseAndroid` methods.

In the example above, we designed the `VerificationInfoDelegate` method to be called once the response arrives. The response
to purchase verification is represented with an `ADJPVerificationInfo` object and it contains following information:

```csharp
verificationInfo.VerificationState   // State of purchase verification.
verificationInfo.StatusCode          // Integer which displays backend response status code.
verificationInfo.Message             // Message describing purchase verification state.
```

The verification state can have one of the following values:

```
ADJPVerificationState.ADJPVerificationStatePassed         - Purchase verification successful.
ADJPVerificationState.ADJPVerificationStateFailed         - Purchase verification failed.
ADJPVerificationState.ADJPVerificationStateUnknown        - Purchase verification state unknown.
ADJPVerificationState.ADJPVerificationStateNotVerified    - Purchase was not verified.
```

* If the purchase was successfully verified by Apple/Google servers, `ADJPVerificationStatePassed` will be reported together
with the status code `200`.
* If the Apple/Google servers recognized the purchase as invalid, `ADJPVerificationStateFailed` will be reported together 
with the status code `406`.
* If the Apple/Google servers did not provide us with an answer for our request to verify your purchase, 
`ADJPVerificationStateUnknown` will be reported together with the status code `204`. This means that we did not recieve any 
information from Apple/Google servers regarding validity of your purchase. This does not say anything about the purchase 
itself. It might be both - valid or invalid. This state will also be reported if any other situation prevents us from 
reporting the correct state of your purchase verification. More details about these errors can be found in the `Message` 
property of `ADJPVerificationInfo` object.
* If `ADJPVerificationStateNotVerified` is reported, that means that the call to `VerifyPurchaseiOS` or 
`VerifyPurchaseAndroid` method was done with invalid parameters or that in general for some reason verification request was 
not even sent from the purchase SDK. Again, more information about error which caused this may be found in `Message` 
property of `ADJPVerificationInfo` object.

### <a id="track-purchases">Track your verified purchases

After a purchase is successfully verified, you can track it with our official adjust SDK and keep track of revenue in your 
dashboard. You can also pass in an optional transaction ID created in an event in order to avoid tracking duplicate 
revenues. The last ten transaction IDs are remembered and revenue events with duplicate transaction IDs are skipped.

**At the moment, the transaction duplication protection mechanism is working only for the iOS platform.**

Using the examples from above, you can do this as follows:

```csharp
using com.adjust.sdk;
using com.adjust.sdk.purchase;

// ...

private void VerificationInfoDelegate (ADJPVerificationInfo verificationInfo)
{
    if (verificationInfo.VerificationState == ADJPVerificationState.ADJPVerificationStatePassed)
    {
        AdjustEvent adjustEvent = new AdjustEvent ("{YourEventToken}");
        adjustEvent.setRevenue (0.01, "EUR");

        // iOS feature only!
        adjustEvent.setTransactionId("{TransactionId}");

        Adjust.trackEvent (adjustEvent);
    }
}
```

## <a id="best-practices">Best practices

Once `ADJPVerificationStatePassed` or `ADJPVerificationStateFailed` are reported, you can be secure that this decision was 
made by Apple/Google servers and can rely on them to track or not to track your purchase revenue. Once 
`ADJPVerificationStateUnknown` is reported, you can decide what do you want to do with this purchase.

For statistical purposes, it may be wise to have a single defined event for each of these scenarios in the adjust dashboard.
This way, you can have better overview of how many of your purchases was marked as passed, how many of them failed and how 
many of them were not able to be verified and returned an unknown status. You can also keep track of unverified purchases if
you would like to.

If you decide to do so, your method for handling the response can look like this:

```csharp
private void VerificationInfoDelegate (ADJPVerificationInfo verificationInfo)
{
    if (verificationInfo.VerificationState == ADJPVerificationState.ADJPVerificationStatePassed)
    {
        AdjustEvent adjustEvent = new AdjustEvent ("{YourEventPassedToken}");
        adjustEvent.setRevenue (0.01, "EUR");

        // iOS feature only!
        adjustEvent.setTransactionId("{TransactionId}");

        Adjust.trackEvent (adjustEvent);
    }
    else if (verificationInfo.VerificationState == ADJPVerificationState.ADJPVerificationStateFailed)
    {
        AdjustEvent adjustEvent = new AdjustEvent ("{YourEventFailedToken}");
        Adjust.trackEvent (adjustEvent);
    }
    else if (verificationInfo.VerificationState == ADJPVerificationState.ADJPVerificationStateUnknown)
    {
        AdjustEvent adjustEvent = new AdjustEvent ("{YourEventUnknownToken}");
        Adjust.trackEvent (adjustEvent);
    }
    else
    {
        AdjustEvent adjustEvent = new AdjustEvent ("{YourEventNotVerifiedToken}");
        Adjust.trackEvent (adjustEvent);
    }
}
```

[dashboard]:    http://adjust.com
[adjust.com]:   http://adjust.com

[releases]:             https://github.com/adjust/ios_purchase_sdk/releases
[fraud-prevention]:     https://docs.adjust.com/en/fraud-prevention/

[editor]:           https://raw.github.com/adjust/adjust_sdk/master/Resources/unity_purchase/adjust_purchase_editor.png
[import_package]:   https://raw.github.com/adjust/adjust_sdk/master/Resources/unity_purchase/import_package.png

## <a id="livense">License

The adjust purchase SDK is licensed under the MIT License.

Copyright (c) 2016 adjust GmbH,
http://www.adjust.com

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies
of the Software, and to permit persons to whom the Software is furnished to do
so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
