using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Enums;
using Peppa.Models;
using Peppa.Interface.Services;
using piggy_bank_uwp.Contracts.Requests;

namespace Peppa.Services.PiggyService
{
    public partial class PiggyService : IUserService
    {
        private const string ClientSecret = "secret";
        private const string Scopes = "api1 offline_access";

        public async Task<RegitrationResult> RegistrationUser(UserRequest request)
        {
            var client = _httpClientFactory.CreateClient("Regitration user");
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync($"{BaseUrl}/api/Users", content))
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

        public async Task<AccessTokenResponse> GetAccessToken(GetTokenRequest request)
        {
            var client = _httpClientFactory.CreateClient("Token");
            var content = new StringContent(JsonConvert.SerializeObject(request));
            using (var response = await client.PostAsync($"{BaseUrl}/api/Tokens/Connect", content))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<AccessTokenResponse>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }

        public async Task<AvailableCurrency[]> GetAvailableCurrencies()
        {
            var client = _httpClientFactory.CreateClient("Available currencies");

            using (var response = await client.GetAsync($"{BaseUrl}/api/Currencies"))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<AvailableCurrency[]>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }
    }
}