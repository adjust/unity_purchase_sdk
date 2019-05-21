using System;
using System.Collections.Generic;

namespace com.adjust.sdk.purchase
{
    public interface IAdjustPurchase
    {
        void Init(ADJPConfig config);

        // iOS specific methods
        void VerifyPurchaseiOS(string receipt, string transactionId, string productId, string sceneName = "AdjustPurchase");

        // Android specific methods
        void VerifyPurchaseAndroid(string itemSku, string itemToken, string developerPayload, Action<ADJPVerificationInfo> verificationInfoCallback);
    }
}
