using System.Threading.Tasks;
using Peppa.Contracts;

namespace Peppa.Interface.Services
{
    public interface ICategoryService
    {
        Task<CategoryContract[]> GetCategories();

        Task<CategoryContract> CreateCategory(CategoryContract contract);
        
        Task<bool> UpdateCategory(CategoryContract contract);

        Task<bool> DeleteCategory(int id);
    }
}
