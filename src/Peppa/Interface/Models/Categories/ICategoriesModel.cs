using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Peppa.Interface.Models.Categories
{
    public interface ICategoriesModel : IDisposable
    {
        ICategoryModel CreateNewCategory();
        Task UpdateCategories(CancellationToken token);
        Task SaveCategory(ICategoryModel newCategory, CancellationToken token);
        Task UpdateCategory(ICategoryModel category, CancellationToken token);
        Task DeleteCategory(ICategoryModel category, CancellationToken token);
        List<ICategoryModel> Categories { get; set; }
    }
}