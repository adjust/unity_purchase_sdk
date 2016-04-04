using System;

namespace com.adjust.sdk.purchase
{
    public class ADJPConfig
    {
        #region Fields
        internal string appToken;

        internal ADJPLogLevel? logLevel;
        internal ADJPEnvironment environment;

        internal Action<ADJPVerificationInfo> attributionChangedDelegate;
        #endregion

        #region Constructors
        public ADJPConfig (string appToken, ADJPEnvironment environment)
        {
            this.appToken = appToken;
            this.environment = environment;
        }
        #endregion

        #region Public methods
        public void SetLogLevel (ADJPLogLevel logLevel)
        {
            this.logLevel = logLevel;
        }

        /*
        public void setAttributionChangedDelegate (Action<AdjustAttribution> attributionChangedDelegate, string sceneName = "Adjust")
        {
            this.attributionChangedDelegate = attributionChangedDelegate;
            this.sceneName = sceneName;
        }

        public Action<AdjustAttribution> getAttributionChangedDelegate ()
        {
            return this.attributionChangedDelegate;
        }
        */
        #endregion
    }
}
