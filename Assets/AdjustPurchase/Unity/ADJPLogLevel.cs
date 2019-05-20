namespace com.adjust.sdk.purchase
{
    public enum ADJPLogLevel
    {
        Verbose = 1,
        Debug,
        Info,
        Warn,
        Error,
        Assert
    }

    public static class ADJPLogLevelExtension
    {
        public static string LowercaseToString(this ADJPLogLevel adjustPurchaseLogLevel)
        {
            switch (adjustPurchaseLogLevel)
            {
                case ADJPLogLevel.Verbose:
                    return "verbose";
                case ADJPLogLevel.Debug:
                    return "debug";
                case ADJPLogLevel.Info:
                    return "info";
                case ADJPLogLevel.Warn:
                    return "warn";
                case ADJPLogLevel.Error:
                    return "error";
                case ADJPLogLevel.Assert:
                    return "assert";
                default:
                    return "unknown";
            }
        }

        public static string UppercaseToString(this ADJPLogLevel adjustPurchaseLogLevel)
        {
            switch (adjustPurchaseLogLevel)
            {
                case ADJPLogLevel.Verbose:
                    return "VERBOSE";
                case ADJPLogLevel.Debug:
                    return "DEBUG";
                case ADJPLogLevel.Info:
                    return "INFO";
                case ADJPLogLevel.Warn:
                    return "WARN";
                case ADJPLogLevel.Error:
                    return "ERROR";
                case ADJPLogLevel.Assert:
                    return "ASSERT";
                default:
                    return "UNKNOWN";
            }
        }
    }
}
