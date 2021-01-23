using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Peppa.Workers;

namespace Peppa.Services.PiggyService
{
    public abstract class PiggyServiceBase
    {
        protected PiggyServiceBase(IHttpClientFactory httpClientFactory)
            => HttpClientFactory = httpClientFactory;

        protected async Task<TResponse> Get<TResponse>(string requestUrl, CancellationToken token) where TResponse : class
        {
            var client = HttpClientFactory.CreateClient("Get");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string) SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.GetAsync($"{BaseUrl}/{requestUrl}", token))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }
        
        protected async Task<TResponse> Post<TResponse, TRequest>(string requestUrl, TRequest request, CancellationToken token) where TResponse : class 
            where TRequest : class
        {
            var client = HttpClientFactory.CreateClient("Post");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string) SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.PostAsync($"{BaseUrl}/{requestUrl}", ToStringContent(request), token))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }
        
        protected async Task<bool> Post<TRequest>(string requestUrl, TRequest request, CancellationToken token) where TRequest : class
        {
            var client = HttpClientFactory.CreateClient("Post");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string) SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.PostAsync($"{BaseUrl}/{requestUrl}", ToStringContent(request), token))
            {
                return response.IsSuccessStatusCode;
            }
        }
        
        protected async Task<bool> Put<TRequest>(string requestUrl, TRequest request, CancellationToken token) where TRequest : class
        {
            var client = HttpClientFactory.CreateClient("Put");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string) SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.PutAsync($"{BaseUrl}/{requestUrl}", ToStringContent(request), token))
            {
                return response.IsSuccessStatusCode;
            }
        }
        
        protected async Task<bool> Delete(string requestUrl, CancellationToken token)
        {
            var client = HttpClientFactory.CreateClient("Delete");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string) SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.DeleteAsync($"{BaseUrl}/{requestUrl}", token))
            {
                return response.IsSuccessStatusCode;
            }
        }

        private static StringContent ToStringContent<T>(T content) where T : class
            => new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

        public IHttpClientFactory HttpClientFactory { get; }
        public string BaseUrl { get; } = @"https://dev.piggybank.pro/api";
        public bool IsAuthorized => SettingsWorker.Current.HaveValue(Constants.AccessToken);
    }
}