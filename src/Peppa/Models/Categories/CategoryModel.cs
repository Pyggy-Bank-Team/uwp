using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using Peppa.Contracts.Requests.Categories;
using Peppa.Enums;
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
            Id = entity.Id;
            Title = entity.Title;
            HexColor = entity.HexColor;
            Type = entity.Type;
            IsArchived = entity.IsArchived;
        }
        
        public async Task Save(CancellationToken token)
        {
            if (!_service.IsAuthorized)
                return;

            var request = new CreateCategoryRequest
            {
                Title = Title,
                Type = Type,
                HexColor = HexColor,
                IsArchived = IsArchived
            };

            await _service.CreateCategory(request, token);
        }

        public async Task Update(CancellationToken token)
        {
            if (!_service.IsAuthorized)
                return;

            var request = new UpdateCategoryRequest
            {
                Id = Id,
                Title = Title,
                Type = Type,
                HexColor = HexColor,
                IsArchived = IsArchived
            };

            await _service.UpdateCategory(request, token);
        }

        public async Task Delete(CancellationToken token)
        {
            if (!_service.IsAuthorized)
                return;

            await _service.DeleteCategory(Id, token);
        }

        public string HexColor { get; set; }
        public string Title { get; set; }
        public CategoryType Type { get; set; }
        public bool IsArchived { get; set; }
        public int Id { get ; }
    }
}
