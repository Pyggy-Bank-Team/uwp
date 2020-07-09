using System.Net.Http;

namespace piggy_bank_uwp.Services
{
    public class PiggyService
    {
        private const string IdentityServer = @"http://dtrest1-001-site1.itempurl.com";
        private const string BaseUrl = @"http://dtrest-001-site1.etempurl.com/api";
        
        private readonly HttpClient _client;

        public PiggyService()
            => _client = new HttpClient();
    }
}