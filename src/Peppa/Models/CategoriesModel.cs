using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using Peppa.Contracts;
using Peppa.Interface;
using Peppa.Interface.Models;
using Peppa.Interface.Services;

namespace Peppa.Models
{
    public class CategoriesModel : ICategoriesModel
    {
        private readonly IPiggyRepository _repository;
        private readonly ICategoryService _service;

        public CategoriesModel(IPiggyRepository repository, ICategoryService service)
            => (_repository, _service) = (repository, service);

        public async Task<Category[]> GetCategories(CancellationToken token)
        {
            //If user is logged in then sync accounts
            if (_service.IsAuthorized)
            {
                var categories = await _service.GetCategories();
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

            return await _repository.GetCategories(token);
        }

        public async Task CreateCategory(Category category, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var request = new CategoryContract
                {
                    HexColor = category.HexColor,
                    Title = category.Title,
                    Type = category.Type,
                    IsArchived = category.IsArchived
                };

                var createdAccount = await _service.CreateCategory(request);
                //TODO Add a log about service result
                if (createdAccount != null)
                {
                    if (await _repository.HaveAccount(category.Id, token))
                        await _repository.DeleteAccount(category.Id, token);

                    category.Id = createdAccount.Id;
                    category.HexColor = createdAccount.HexColor;
                    category.Title = createdAccount.Title;
                    category.Type = createdAccount.Type;
                    category.IsArchived = createdAccount.IsArchived;
                    category.IsDeleted = createdAccount.IsDeleted;
                    category.IsSynchronized = true;
                }
            }

            await _repository.CreateCategory(category, token);
        }

        public async Task DeleteCategory(int id, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var isSuccessful = await _service.DeleteCategory(id);
                //TODO Add a log about service result
                if (isSuccessful)
                {
                    //TODO Added in database
                    return;
                }
            }

            //TODO Add case for non-login user
        }

        public async Task UpdateCategory(Category category, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var request = new CategoryContract
                {
                    Id = category.Id,
                    HexColor = category.HexColor,
                    Title = category.Title,
                    Type = category.Type,
                    IsArchived = category.IsArchived
                };

                var isSuccessful = await _service.UpdateCategory(request);
                //TODO Add a log about service result
                if (isSuccessful)
                {
                    //TODO Added in database
                }
            }

            //TODO Add case for non-login user
        }

        public void Dispose()
            => _repository?.Dispose();
    }
}