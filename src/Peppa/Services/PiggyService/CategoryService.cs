using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests.Categories;
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

        public Task<CategoryResponse> CreateCategory(CreateCategoryRequest request, CancellationToken token)
            => Post<CategoryResponse, CreateCategoryRequest>("categories", request, token);

        public Task<bool> UpdateCategory(UpdateCategoryRequest request, CancellationToken token)
            => Put($"categories/{request.Id}", request, token);

        public Task<bool> DeleteCategory(int id, CancellationToken token)
            => Delete($"categories/{id}", token);
    }
}