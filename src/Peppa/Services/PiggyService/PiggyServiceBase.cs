using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Peppa.Contracts;
using Peppa.Workers;

namespace Peppa.Services.PiggyService
{
    public abstract class PiggyServiceBase
    {
        protected PiggyServiceBase(IHttpClientFactory httpClientFactory)
            => HttpClientFactory = httpClientFactory;

        protected async Task<TResponse> Get<TResponse, TRequest>(TRequest request, string requestUrl, CancellationToken token) where TResponse : class, new()
        {
            var client = HttpClientFactory.CreateClient("Get");
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", (string) SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.GetAsync($"{BaseUrl}/{requestUrl}", token))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }

        public IHttpClientFactory HttpClientFactory { get; }
        public string BaseUrl { get; } = @"http://piggy-api.somee.com/api";
        public bool IsAuthorized => SettingsWorker.Current.HaveValue(Constants.AccessToken);
    }
}