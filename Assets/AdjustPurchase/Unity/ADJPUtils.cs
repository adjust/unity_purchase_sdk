using System;
using System.Collections.Generic;

using UnityEngine;

namespace com.adjust.sdk.purchase
{
    public class ADJPUtils
    {
        #region Constants
        public static string KeyMessage = "message";
        public static string KeyStatusCode = "statusCode";
        public static string KeyVerificationState = "verificationState";
        #endregion

        #region Public methods
        public static int ConvertLogLevel(ADJPLogLevel? logLevel)
        {
            if (logLevel == null)
            {
                return -1;
            }

            return (int)logLevel;
        }

        public static String GetJsonString(JSONNode node, string key)
        {
            var jsonValue = GetJsonValue(node, key);

            if (jsonValue == null)
            {
                return null;
            }

            return jsonValue.Value;
        }

        public static JSONNode GetJsonValue(JSONNode node, string key)
        {
            if (node == null)
            {
                return null;
            }

            var nodeValue = node[key];

            if (nodeValue.GetType() == typeof(JSONLazyCreator))
            {
                return null;
            }

            return nodeValue;
        }

        public static ADJPVerificationState? StringToVerificationState(string verificationStateString)
        {
            if (verificationStateString.Equals("ADJPVerificationStatePassed"))
            {
                return ADJPVerificationState.ADJPVerificationStatePassed;
            }
            else if (verificationStateString.Equals("ADJPVerificationStateFailed"))
            {
                return ADJPVerificationState.ADJPVerificationStateFailed;
            }
            else if (verificationStateString.Equals("ADJPVerificationStateUnknown"))
            {
                return ADJPVerificationState.ADJPVerificationStateUnknown;
            }
            else if (verificationStateString.Equals("ADJPVerificationStateNotVerified"))
            {
                return ADJPVerificationState.ADJPVerificationStateNotVerified;
            }
            else
            {
                return null;
            }
        }
        #endregion
    }
}
