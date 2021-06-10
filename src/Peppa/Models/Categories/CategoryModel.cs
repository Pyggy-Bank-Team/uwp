using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using Peppa.Interface;
using Peppa.Interface.Models.Categories;
using Peppa.Interface.Services;

namespace Peppa.Models.Categories
{
    public sealed class CategoryModel : ICategoryModel
    {
        private readonly ICategoryService _service;

        public CategoryModel(Category entity, ICategoryService service)
        {
            _service = service;
        }
        
        public string HexColor { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Id { get ; set; }
        public Task Save(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(CancellationToken token)
        {
            throw new System.NotImplementedException();
        }
    }
}
