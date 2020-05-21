using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

using UnityEngine;

namespace com.adjust.sdk.purchase
{
#if UNITY_IOS
    public class AdjustPurchaseiOS : IAdjustPurchase
    {
        #region Fields
        private const string sdkPrefix = "unity1.0.3";
        #endregion

        #region External methods
        [DllImport("__Internal")]
        private static extern void _AdjustPurchaseInit(string appToken, string environment, string sdkPrefix, int logLevel);

        [DllImport("__Internal")]
        private static extern void _AdjustPurchaseVerifyPurchase(string receipt, string transactionId, string productId, string sceneName);
        #endregion

        #region Constructors
        public AdjustPurchaseiOS()
        {
        }
        #endregion

        #region Public methods
        public void Init(ADJPConfig config)
        {
            string appToken = config.appToken;
            string environment = config.environment.LowercaseToString();

            int logLevel = ADJPUtils.ConvertLogLevel(config.logLevel);

            _AdjustPurchaseInit(appToken, environment, sdkPrefix, logLevel);
        }

        public void VerifyPurchaseiOS(string receipt, string transactionId, string productId, string sceneName)
        {
            _AdjustPurchaseVerifyPurchase(receipt, transactionId, productId, sceneName);
        }

        public void VerifyPurchaseAndroid(string itemSku, string itemToken, string developerPayload, Action<ADJPVerificationInfo> verificationInfoCallback)
        {
        }
        #endregion
    }
#endif
}
