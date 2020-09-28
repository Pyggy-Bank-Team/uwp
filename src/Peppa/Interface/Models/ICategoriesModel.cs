using System;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;

namespace Peppa.Interface.Models
{
    public interface ICategoriesModel : IDisposable
    {
        Task<Category[]> GetCategories(CancellationToken token);
        Task CreateCategory(Category category, CancellationToken token);
        Task DeleteCategory(int id, CancellationToken token);
        Task UpdateCategory(Category category, CancellationToken token);
    }
}