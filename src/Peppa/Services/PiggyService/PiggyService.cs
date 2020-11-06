using System.Net.Http;
using Peppa.Workers;

namespace Peppa.Services.PiggyService
{
    public partial class PiggyService
    {
        private const string BaseUrl = @"http://piggy-api.somee.com/api";

        private readonly IHttpClientFactory _httpClientFactory;

        public PiggyService(IHttpClientFactory httpClientFactory)
            => _httpClientFactory = httpClientFactory;

        public bool IsAuthorized => SettingsWorker.Current.HaveValue(Constants.AccessToken);
    }
}