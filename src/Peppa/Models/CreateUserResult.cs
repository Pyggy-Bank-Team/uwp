using Peppa.Contracts.Responses;
using Peppa.Enums;

namespace Peppa.Models
{
    public class CreateUserResult
    {
        public CreateUserResultEnum Result { get; set; }
        public ErrorResponse Error { get; set; }
        public AccessTokenResponse Token { get; set; }
    }
}
