namespace Peppa.Contracts.Responses
{
    public class AccessTokenResponse
    {
        public string AccessToken { get; set; }

        public long ExpiresIn { get; set; }
        
        public string TokenType { get; set; }
    }
}