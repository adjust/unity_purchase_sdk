namespace com.adjust.sdk.purchase
{
    public enum ADJPVerificationState
    {
        ADJPVerificationStatePassed,
        ADJPVerificationStateFailed,
        ADJPVerificationStateUnknown,
        ADJPVerificationStateNotVerified
    }

    public static class ADJPVerificationStateExtension
    {
        public static string VerificationStateToString(this ADJPVerificationState verificationState)
        {
            switch (verificationState)
            {
                case ADJPVerificationState.ADJPVerificationStatePassed:
                    return "ADJPVerificationStatePassed";
                case ADJPVerificationState.ADJPVerificationStateFailed:
                    return "ADJPVerificationStateFailed";
                case ADJPVerificationState.ADJPVerificationStateUnknown:
                    return "ADJPVerificationStateUnknown";
                case ADJPVerificationState.ADJPVerificationStateNotVerified:
                    return "ADJPVerificationStateNotVerified";
                default:
                    return "INVALID";
            }
        }
    }
}
