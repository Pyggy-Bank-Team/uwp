namespace piggy_bank_uwp.Enums
{
    public enum IdentityResultEnum
    {
        Successful,
        DuplicateUserName,
        TokenIsNullOrEmpty,
        TokenGenerateError,
        InternalServerError,
        PasswordTooShort
    }
}
