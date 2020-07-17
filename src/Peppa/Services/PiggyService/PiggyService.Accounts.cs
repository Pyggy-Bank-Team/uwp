using Newtonsoft.Json;
using piggy_bank_uwp.Entities;
using piggy_bank_uwp.Interface;
using piggy_bank_uwp.Workers;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
    }
}
