using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using Peppa.Contracts.Requests;
using Peppa.Interface;
using Peppa.Workers;
using Peppa.Interface.Services;
using Peppa.Contracts;
using Peppa.Context.Entities;

namespace Peppa.Services.PiggyService
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
