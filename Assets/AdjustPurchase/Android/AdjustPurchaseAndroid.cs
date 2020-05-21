using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;

namespace com.adjust.sdk.purchase
{
#if UNITY_ANDROID
    public class AdjustPurchaseAndroid : IAdjustPurchase
    {
        #region Fields
        private const string sdkPrefix = "unity1.0.3";
        private AndroidJavaClass ajcAdjustPurchase;
        private VerificationInfoListener verificationInfoListener;
        private Action<ADJPVerificationInfo> verificationInfoCallback;
        #endregion

        #region Proxy listener classes
        private class VerificationInfoListener : AndroidJavaProxy
        {
            private Action<ADJPVerificationInfo> callback;

            public VerificationInfoListener(Action<ADJPVerificationInfo> pCallback) : base("com.adjust.sdk.purchase.OnADJPVerificationFinished")
            {
                this.callback = pCallback;
            }

            public void onVerificationFinished(AndroidJavaObject verificationInfo)
            {
                ADJPVerificationInfo purchaseVerificationInfo = new ADJPVerificationInfo();

                purchaseVerificationInfo.Message = verificationInfo.Get<string>(ADJPUtils.KeyMessage);
                
                AndroidJavaObject ajoStatusCode = verificationInfo.Get<AndroidJavaObject>(ADJPUtils.KeyStatusCode);
                purchaseVerificationInfo.StatusCode = ajoStatusCode.Call<int>("intValue");

                AndroidJavaObject ajoVerificationState = verificationInfo.Get<AndroidJavaObject>(ADJPUtils.KeyVerificationState);
                string verificationStateName = ajoVerificationState.Call<string>("name");
                purchaseVerificationInfo.VerificationState = ADJPUtils.StringToVerificationState(verificationStateName);

                if (callback != null)
                {
                    callback(purchaseVerificationInfo);
                }
            }
        }
        #endregion

        #region Constructors
        public AdjustPurchaseAndroid()
        {
            ajcAdjustPurchase = new AndroidJavaClass("com.adjust.sdk.purchase.AdjustPurchase");
        }
        #endregion

        #region Public methods
        public void Init(ADJPConfig config)
        {
            // Thank you, Unity 2019.2.0, for breaking this.
            // AndroidJavaObject ajoEnvironment = config.environment == ADJPEnvironment.Sandbox ? 
            //     new AndroidJavaClass("com.adjust.sdk.purchase.ADJPConstants").GetStatic<AndroidJavaObject>("ENVIRONMENT_SANDBOX") :
            //         new AndroidJavaClass("com.adjust.sdk.purchase.ADJPConstants").GetStatic<AndroidJavaObject>("ENVIRONMENT_PRODUCTION");

            // Get environment variable.
            string ajoEnvironment = config.environment == ADJPEnvironment.Production ? "production" : "sandbox";

            // Create adjust config object.
            AndroidJavaObject ajoConfig = new AndroidJavaObject("com.adjust.sdk.purchase.ADJPConfig", config.appToken, ajoEnvironment);

            // Check log level.
            if (config.logLevel.HasValue)
            {
                AndroidJavaObject ajoLogLevel = new AndroidJavaClass("com.adjust.sdk.purchase.ADJPLogLevel").GetStatic<AndroidJavaObject>(config.logLevel.Value.UppercaseToString());

                if (ajoLogLevel != null)
                {
                    ajoConfig.Call("setLogLevel", ajoLogLevel);
                }
            }

            // Set unity SDK prefix.
            ajoConfig.Call("setSdkPrefix", sdkPrefix);

            // Initialise and start the SDK.
            ajcAdjustPurchase.CallStatic("init", ajoConfig);
        }

        public void VerifyPurchaseiOS(string receipt, string transactionId, string productId, string sceneName)
        {
        }

        public void VerifyPurchaseAndroid(string itemSku, string itemToken, string developerPayload, Action<ADJPVerificationInfo> verificationInfoCallback)
        {
            verificationInfoListener = new VerificationInfoListener(verificationInfoCallback);
            ajcAdjustPurchase.CallStatic("verifyPurchase", itemSku, itemToken, developerPayload, verificationInfoListener);
        }
        #endregion
    }
#endif
}
