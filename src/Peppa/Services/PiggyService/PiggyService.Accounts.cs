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
            using (var response = await client.GetAsync($"{BaseUrl}/Accounts"))
            {
                return response.IsSuccessStatusCode
                     ? JsonConvert.DeserializeObject<AccountContract[]>(await response.Content.ReadAsStringAsync())
                     : null;
            }
        }

        public async Task<bool> CreateAccount(AccountContract contract)
        {
            var client = _httpClientFactory.CreateClient("CreateAccount");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.PostAsync($"{BaseUrl}/Accounts", new StringContent(JsonConvert.SerializeObject(contract), Encoding.UTF8, "application/json")))
            {
                return response.IsSuccessStatusCode;
            }

        }

        public async Task UpdateAccount(AccountContract contract)
        {
            throw new System.NotImplementedException();
        }
    }
}
