using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Peppa.Contracts;
using Peppa.Interface.Services;
using Peppa.Workers;

namespace Peppa.Services.PiggyService
{
    public partial class PiggyService : ICategoryService
    {
        public async Task<CategoryContract[]> GetCategories()
        {
            var client = _httpClientFactory.CreateClient("GetCategories");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.GetAsync($"{BaseUrl}/Categories?all=true"))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<CategoryContract[]>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }

        public async Task<CategoryContract> CreateCategory(CategoryContract contract)
        {
            var client = _httpClientFactory.CreateClient("CreateCategory");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.PostAsync($"{BaseUrl}/Categories", new StringContent(JsonConvert.SerializeObject(contract), Encoding.UTF8, "application/json")))
            {
                return response.IsSuccessStatusCode
                    ? JsonConvert.DeserializeObject<CategoryContract>(await response.Content.ReadAsStringAsync())
                    : null;
            }
        }

        public async Task<bool> UpdateCategory(CategoryContract contract)
        {
            var client = _httpClientFactory.CreateClient("UpdateCategory");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.PutAsync($"{BaseUrl}/Categories/{contract.Id}", new StringContent(JsonConvert.SerializeObject(contract), Encoding.UTF8, "application/json")))
            {
                return response.IsSuccessStatusCode;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            var client = _httpClientFactory.CreateClient("DeleteCategory");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", (string)SettingsWorker.Current.GetValue(Constants.AccessToken));
            using (var response = await client.DeleteAsync($"{BaseUrl}/Categories/{id}"))
            {
                return response.IsSuccessStatusCode;
            }
        }
    }
}
