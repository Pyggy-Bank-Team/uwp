using Peppa.Contracts.Responses;
using Peppa.Enums;

namespace Peppa.Contracts.Results
{
    public class CreateUserResult
    {
        public CreateUserResultEnum Result { get; set; }
        public ErrorResponse Error { get; set; }
        public AccessTokenResponse Token { get; set; }
    }
}
