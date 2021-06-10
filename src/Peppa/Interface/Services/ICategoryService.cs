using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Requests.Categories;
using Peppa.Contracts.Responses;

namespace Peppa.Interface.Services
{
    public interface ICategoryService : IAuthorization
    {
        Task<CategoryResponse[]> GetCategories(bool showArchivedCategories, CancellationToken token);

        Task<CategoryResponse> CreateCategory(CreateCategoryRequest request, CancellationToken token);

        Task<bool> UpdateCategory(UpdateCategoryRequest response, CancellationToken token);

        Task<bool> DeleteCategory(int id, CancellationToken token);
    }
}