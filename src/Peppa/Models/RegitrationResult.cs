using piggy_bank_uwp.Contracts.Responses;
using piggy_bank_uwp.Enums;

namespace piggy_bank_uwp.Models
{
    public class RegitrationResult
    {
        public IdentityResultEnum IdentityResult { get; set; }
        public IdentityErrorResponse Error { get; set; }
        public AccessTokenResponse Token { get; set; }
    }
}
