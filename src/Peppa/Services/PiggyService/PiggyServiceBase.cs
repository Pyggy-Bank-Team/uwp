using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Peppa.Contracts.Responses;
using Peppa.Contracts.Results;
using Peppa.Interface.InternalServices;

namespace Peppa.Services.PiggyService
{
    public abstract class PiggyServiceBase
    {
        private readonly ISettingsService _settingsService;

        protected PiggyServiceBase(IHttpClientFactory httpClientFactory, ISettingsService settingsService)
        {
            _settingsService = settingsService;
            HttpClientFactory = httpClientFactory;
        }

        protected async Task<TResponse> Get<TResponse>(string requestUrl, CancellationToken token) where TResponse : class
        {
            var client = HttpClientFactory.CreateClient("Get");
            AddBearerToken(client);
            using (var response = await client.GetAsync($"{BaseUrl}/{requestUrl}", token))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<TResponse>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }
        
        protected async Task<ServiceResult<TResponse>> Post<TResponse, TRequest>(string requestUrl, TRequest request, CancellationToken token) where TResponse : class 
            where TRequest : class
        {
            var client = HttpClientFactory.CreateClient("Post");
            AddBearerToken(client);
            using (var httpResponse = await client.PostAsync($"{BaseUrl}/{requestUrl}", ToStringContent(request), token))
            {
                return await ReturnServiceResultAsync<TResponse>(httpResponse);
            }
        }
        
        protected async Task<bool> Post<TRequest>(string requestUrl, TRequest request, CancellationToken token) where TRequest : class
        {
            var client = HttpClientFactory.CreateClient("Post");
            AddBearerToken(client);
            using (var response = await client.PostAsync($"{BaseUrl}/{requestUrl}", ToStringContent(request), token))
            {
                return response.IsSuccessStatusCode;
            }
        }
        
        protected async Task<bool> Put<TRequest>(string requestUrl, TRequest request, CancellationToken token) where TRequest : class
        {
            var client = HttpClientFactory.CreateClient("Put");
            AddBearerToken(client);
            using (var response = await client.PutAsync($"{BaseUrl}/{requestUrl}", ToStringContent(request), token))
            {
                return response.IsSuccessStatusCode;
            }
        }
        
        protected async Task<bool> Patch<TRequest>(string requestUrl, TRequest request, CancellationToken token) where TRequest : class
        {
            var client = HttpClientFactory.CreateClient("Patch");
            AddBearerToken(client);
            
            var httpRequest = new HttpRequestMessage(new HttpMethod("PATCH"), $"{BaseUrl}/{requestUrl}")
            {
                Content = ToStringContent(request)
            };
            
            using (var response = await client.SendAsync(httpRequest, token))
            {
                return response.IsSuccessStatusCode;
            }
        }
        
        protected async Task<bool> Delete(string requestUrl, CancellationToken token)
        {
            var client = HttpClientFactory.CreateClient("Delete");
            AddBearerToken(client);
            using (var response = await client.DeleteAsync($"{BaseUrl}/{requestUrl}", token))
            {
                return response.IsSuccessStatusCode;
            }
        }

        protected static StringContent ToStringContent<T>(T content) where T : class
            => new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");

        private void AddBearerToken(HttpClient client)
            => client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _settingsService.GetValue(Constants.AccessToken));

        private async Task<ServiceResult<TResponse>> ReturnServiceResultAsync<TResponse>(HttpResponseMessage httpResponse) where TResponse : class
        {
            var result = new ServiceResult<TResponse>();
            var content = await httpResponse.Content.ReadAsStringAsync();
            if (httpResponse.IsSuccessStatusCode)
                result.Ok = JsonConvert.DeserializeObject<TResponse>(content);
            else
                result.Error = JsonConvert.DeserializeObject<ErrorResponse>(content);

            return result;
        }
        
        protected IHttpClientFactory HttpClientFactory { get; }
        protected string BaseUrl { get; } = @"https://dev.piggybank.pro/api";
        public bool IsAuthorized => _settingsService.HaveValue(Constants.AccessToken);
    }
}