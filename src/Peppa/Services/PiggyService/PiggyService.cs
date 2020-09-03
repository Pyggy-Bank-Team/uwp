using System.Net.Http;

namespace Peppa.Services.PiggyService
{
    public partial class PiggyService
    {
        private const string IdentityServer = @"http://piggy-identity.somee.com";
        private const string BaseUrl = @"http://piggy-api.somee.com/api";

        private readonly IHttpClientFactory _httpClientFactory;

        public PiggyService(IHttpClientFactory httpClientFactory)
            => _httpClientFactory = httpClientFactory;
    }
}