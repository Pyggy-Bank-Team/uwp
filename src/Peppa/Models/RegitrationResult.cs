using Peppa.Contracts.Responses;
using Peppa.Enums;

namespace Peppa.Models
{
    public class RegitrationResult
    {
        public IdentityResultEnum IdentityResult { get; set; }
        public ErrorResponse Error { get; set; }
        public AccessTokenResponse Token { get; set; }
    }
}
