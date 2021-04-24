using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Responses;
using Peppa.Interface.Services;

namespace Peppa.Services.PiggyService
{
    public class CategoryService : PiggyServiceBase, ICategoryService
    {
        public CategoryService(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
        {
        }

        public Task<CategoryResponse[]> GetCategories(bool showArchivedCategories, CancellationToken token)
            => Get<CategoryResponse[]>($"categories?all={showArchivedCategories}", token);

        public Task<CategoryResponse> CreateCategory(CategoryResponse response, CancellationToken token)
            => Post<CategoryResponse, CategoryResponse>("categories", response, token);

        public Task<bool> UpdateCategory(CategoryResponse response, CancellationToken token)
            => Put($"categories/{response.Id}", response, token);

        public Task<bool> DeleteCategory(int id, CancellationToken token)
            => Delete($"categories/{id}", token);
    }
}