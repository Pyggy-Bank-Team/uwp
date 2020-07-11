using System.Net.Http;

namespace piggy_bank_uwp.Services.PiggyService
{
    public partial class PiggyService
    {
        private const string IdentityServer = @"http://dtrest1-001-site1.itempurl.com";
        private const string BaseUrl = @"http://dtrest-001-site1.etempurl.com/api";

        private readonly IHttpClientFactory _httpClientFactory;

        public PiggyService(IHttpClientFactory httpClientFactory)
            => _httpClientFactory = httpClientFactory;
    }
}