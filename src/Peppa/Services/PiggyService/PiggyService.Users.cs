using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using piggy_bank_uwp.Interface;
using piggy_bank_uwp.Models.Requests;
using piggy_bank_uwp.Models.Responses;

namespace piggy_bank_uwp.Services.PiggyService
{
    public partial class PiggyService : IUserService
    {
        private const string ClientSecret = "secret";
        private const string Scope = "api1";

        public Task<bool> RegistrationUser(UserRequest request)
        {
            throw new System.NotImplementedException();
        }

        public async Task<AccessTokenResponse> GetAccessToken(UserRequest userRequest)
        {
            var client = _httpClientFactory.CreateClient("Users");

            var body = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("client_id", "client"),
                new KeyValuePair<string, string>("client_secret", ClientSecret),
                new KeyValuePair<string, string>("scope", Scope),
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
    }
}