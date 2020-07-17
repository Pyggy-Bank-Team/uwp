﻿using System.Collections.Generic;
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
        private const string Scopes = "api1 offline_access";

        public async Task<bool> RegistrationUser(UserRequest request)
        {
            var client = _httpClientFactory.CreateClient("Registratin user");
            var content = new StringContent(JsonConvert.SerializeObject(request));
            using (var response = await client.PostAsync($"{IdentityServer}/users", content))
            {
                return response.IsSuccessStatusCode;
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
    }
}