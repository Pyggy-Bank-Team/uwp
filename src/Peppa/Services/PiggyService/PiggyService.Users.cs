using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Enums;
using Peppa.Interface;
using Peppa.Models;

namespace Peppa.Services.PiggyService
{
    public partial class PiggyService : IUserService
    {
        private const string ClientSecret = "secret";
        private const string Scopes = "api1 offline_access";

        public async Task<RegitrationResult> RegistrationUser(UserRequest request)
        {
            var client = _httpClientFactory.CreateClient("Registratin user");
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");
            using (var response = await client.PostAsync($"{IdentityServer}/users", content))
            {
                var result = new RegitrationResult();
                switch (response.StatusCode)
                {
                    case System.Net.HttpStatusCode.BadRequest:
                        var errors = JsonConvert.DeserializeObject<IdentityErrorResponse[]>(await response.Content.ReadAsStringAsync());

                        //TODO Add all errors in a log
                        result.Error = errors.First();
                        result.IdentityResult = Enum.Parse<IdentityResultEnum>(result.Error.Code);
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

        public async Task<AccessTokenResponse> GetAccessToken(UserRequest userRequest)
        {
            var client = _httpClientFactory.CreateClient("Token");

            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", "client"),
                new KeyValuePair<string, string>("client_secret", ClientSecret),
                new KeyValuePair<string, string>("scope", Scopes),
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string>("username", userRequest.UserName),
                new KeyValuePair<string, string>("password", userRequest.Password)
            };

            using (var response = await client.PostAsync($"{IdentityServer}/connect/token", new FormUrlEncodedContent(body)))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<AccessTokenResponse>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }

        public async Task<AvailableCurrency[]> GetAvailableCurrencies()
        {
            var client = _httpClientFactory.CreateClient("Available currencies");

            using (var response = await client.GetAsync($"{IdentityServer}/users/AvailableCurrencies"))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<AvailableCurrency[]>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }
    }
}