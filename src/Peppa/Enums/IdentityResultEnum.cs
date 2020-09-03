namespace Peppa.Enums
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
