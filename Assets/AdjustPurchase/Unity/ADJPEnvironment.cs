namespace com.adjust.sdk.purchase
{
    public enum ADJPEnvironment
    {
        Sandbox,
        Production
    }

    public static class ADJPEnvironmentExtension
    {
        public static string LowercaseToString(this ADJPEnvironment adjustPurchaseEnvironment)
        {
            switch (adjustPurchaseEnvironment)
            {
                case ADJPEnvironment.Sandbox:
                    return "sandbox";
                case ADJPEnvironment.Production:
                    return "production";
                default:
                    return "unknown";
            }
        }
    }
}
