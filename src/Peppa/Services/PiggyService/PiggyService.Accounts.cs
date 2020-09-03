using Newtonsoft.Json;
using piggy_bank_uwp.Entities;
using piggy_bank_uwp.Interface;
using piggy_bank_uwp.Workers;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using piggy_bank_uwp.Contracts.Requests;
using System.Net.Http;
using System.Text;

namespace piggy_bank_uwp.Services.PiggyService
{
    public partial class PiggyService : IAccountService
    {
        public async Task<Account[]> GetAccounts()
        {
            var client = _httpClientFactory.CreateClient("GetAccounts");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.GetAsync($"{BaseUrl}/Accounts"))
            {
                return response.IsSuccessStatusCode
                     ? JsonConvert.DeserializeObject<Account[]>(await response.Content.ReadAsStringAsync())
                     : null;
            }
        }

        public async Task<bool> CreateAccount(AccountRequest request)
        {
            var client = _httpClientFactory.CreateClient("CreateAccount");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.PostAsync($"{BaseUrl}/Accounts", new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json")))
            {
                return response.IsSuccessStatusCode;
            }

        }

        public async Task UpdateAccount(AccountRequest request)
        {
            throw new System.NotImplementedException();
        }
    }
}
