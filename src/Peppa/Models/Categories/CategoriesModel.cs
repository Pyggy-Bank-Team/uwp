using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Enums;
using Peppa.Interface;
using Peppa.Interface.Models.Categories;
using Peppa.Interface.Services;
using Category = Peppa.Context.Entities.Category;

namespace Peppa.Models.Categories
{
    public class CategoriesModel : ICategoriesModel
    {
        private readonly IPiggyRepository _repository;
        private readonly ICategoryService _service;

        public CategoriesModel(IPiggyRepository repository, ICategoryService service)
        {
            _repository = repository;
            _service = service;
            Categories = new List<ICategoryModel>();
        }
        
        public ICategoryModel CreateNewCategory()
        {
            var entity = new Category
            {
                HexColor = "#ffb900",
                Type = CategoryType.Expense
            };
            
            return new CategoryModel(entity, _service, isNew: true);
        }

        public async Task UpdateCategories(CancellationToken token)
        {
            //If user is logged in then sync accounts
            if (_service.IsAuthorized)
            {
                var categories = await _service.GetCategories(true, token);
                if (categories != null)
                {
                    foreach (var category in categories)
                    {
                        var categoryEntity = new Category
                        {
                            Id = category.Id,
                            Title = category.Title,
                            HexColor = category.HexColor,
                            Type = category.Type,
                            IsArchived = category.IsArchived,
                            IsDeleted = category.IsDeleted,
                            IsSynchronized = true
                        };

                        if (await _repository.HaveCategories(category.Id, token))
                            await _repository.UpdateCategory(categoryEntity, token);
                        else
                            await _repository.CreateCategory(categoryEntity, token);
                    }
                }
            }
            
            Categories.Clear();

            foreach (var category in await _repository.GetCategories(token))
                Categories.Add(new CategoryModel(category, _service));
        }

        public async Task SaveCategory(ICategoryModel newCategory, CancellationToken token)
        {
            if (newCategory == null)
                return;

            await newCategory.Save(token);
        }

        public async Task UpdateCategory(ICategoryModel category, CancellationToken token)
        {
            if (category == null)
                return;

            await category.Update(token);
        }

        public async Task DeleteCategory(ICategoryModel category, CancellationToken token)
        {
            if (category == null)
                return;

            await category.Delete(token);
        }

        public List<ICategoryModel> Categories { get; set; }
        
        public void Dispose()
            => _repository?.Dispose();
    }
}