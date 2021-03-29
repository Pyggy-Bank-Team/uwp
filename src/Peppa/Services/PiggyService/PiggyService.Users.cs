using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Enums;
using Peppa.Models;
using Peppa.Interface.Services;
using Peppa.Workers;

namespace Peppa.Services.PiggyService
{
    public partial class PiggyService : IUserService
    {
        public async Task<RegitrationResult> RegistrationUser(CreateUserRequest request, CancellationToken token)
        {
            var client = _httpClientFactory.CreateClient("Regitration user");
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync($"{BaseUrl}/Users", content, token))
            {
                var result = new RegitrationResult();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                        var error = JsonConvert.DeserializeObject<ErrorResponse>(await response.Content.ReadAsStringAsync());

                        //TODO Add all errors in a log
                        result.Error = error;
                        result.IdentityResult = Enum.Parse<IdentityResultEnum>(error.Type);
                        return result;
                    case System.Net.HttpStatusCode.OK:
                        result.Token = JsonConvert.DeserializeObject<AccessTokenResponse>(await response.Content.ReadAsStringAsync());
                        result.IdentityResult = IdentityResultEnum.Successful;
                        return result;
                    default:
                        result.IdentityResult = IdentityResultEnum.InternalServerError;
                        return result;
                }
            }
        }

        public async Task<AccessTokenResponse> GetAccessToken(GetTokenRequest request, CancellationToken token)
        {
            var client = _httpClientFactory.CreateClient("Token");
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync($"{BaseUrl}/Tokens/Connect", content, token))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<AccessTokenResponse>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }

        public async Task<CurrencyResponse[]> GetAvailableCurrencies(CancellationToken token)
        {
            var client = _httpClientFactory.CreateClient("Available currencies");

            using (var response = await client.GetAsync($"{BaseUrl}/Currencies", token))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<CurrencyResponse[]>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }

        public async Task<UserInfoResponse> GetUserInfo(CancellationToken token)
        {
            var client = _httpClientFactory.CreateClient("User info");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string) SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.GetAsync($"{BaseUrl}/users/userInfo", token))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<UserInfoResponse>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }
    }
}