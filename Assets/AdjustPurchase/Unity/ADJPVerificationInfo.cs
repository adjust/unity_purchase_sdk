using System;
using System.Collections.Generic;

namespace com.adjust.sdk.purchase
{
    public class ADJPVerificationInfo
    {
        #region Properties
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public ADJPVerificationState? VerificationState { get; set; }
        #endregion

        #region Constructors
        public ADJPVerificationInfo()
        {
        }

        public ADJPVerificationInfo (string jsonString)
        {
            var jsonNode = JSON.Parse (jsonString);

            if (jsonNode == null)
            {
                return;
            }

            Message = ADJPUtils.GetJsonString(jsonNode, ADJPUtils.KeyMessage);

            string stringStatusCode = ADJPUtils.GetJsonString(jsonNode, ADJPUtils.KeyStatusCode);
            string stringVerificationState = ADJPUtils.GetJsonString(jsonNode, ADJPUtils.KeyVerificationState);
            
            StatusCode = Int32.Parse(stringStatusCode);
            VerificationState = ADJPUtils.StringToVerificationState(stringVerificationState);
        }
        #endregion
    }
}
