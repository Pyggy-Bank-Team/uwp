using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Peppa.Workers;
using Peppa.Interface.Services;
using Peppa.Contracts;

namespace Peppa.Services.PiggyService
{
    public partial class PiggyService : IAccountService
    {
        public async Task<AccountContract[]> GetAccounts()
        {
            var client = _httpClientFactory.CreateClient("GetAccounts");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.GetAsync($"{BaseUrl}/Accounts?all=true"))
            {
                return response.IsSuccessStatusCode
                     ? JsonConvert.DeserializeObject<AccountContract[]>(await response.Content.ReadAsStringAsync())
                     : null;
            }
        }

        public async Task<AccountContract> CreateAccount(AccountContract contract)
        {
            var client = _httpClientFactory.CreateClient("CreateAccount");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.PostAsync($"{BaseUrl}/Accounts", new StringContent(JsonConvert.SerializeObject(contract), Encoding.UTF8, "application/json")))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<AccountContract>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }

        public async Task<bool> UpdateAccount(AccountContract contract)
        {
            var client = _httpClientFactory.CreateClient("UpdateAccount");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)SettingsWorker.Current.GetValue(Constants.AccessToken));

            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, $"{BaseUrl}/Accounts/{contract.Id}")
            {
                Content = new StringContent(JsonConvert.SerializeObject(contract), Encoding.UTF8, "application/json")
            };

            using (var response = await client.SendAsync(request))
            {
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> DeleteAccount(int id)
        {
            var client = _httpClientFactory.CreateClient("DeleteAccount");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.DeleteAsync($"{BaseUrl}/Accounts/{id}"))
            {
                return response.IsSuccessStatusCode;
            }
        }
    }
}
