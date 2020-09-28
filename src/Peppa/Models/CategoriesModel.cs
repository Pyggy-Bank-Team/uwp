using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;
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

        public async Task<Category[]> GetAccounts(CancellationToken token)
        {
            //If user is logged in then sync accounts
            if (_service.IsAuthorized)
            {
                var categories = await _service.GetCategories();
                if (categories != null)
                {
                    foreach (var account in categories)
                    {
                        var accountEntity = new Category
                        {
                            Id = account.Id,
                            Title = account.Title,
                            Type = account.Type,
                            IsArchived = account.IsArchived,
                            IsDeleted = account.IsDeleted,
                            IsSynchronized = true
                        };

                        if (await _repository.HaveCategories(account.Id, token))
                            await _repository.UpdateCategory(accountEntity, token);
                        else
                            await _repository.CreateCategory(accountEntity, token);
                    }
                }
            }

            return await _repository.GetCategories(token);
        }

        public async Task CreatedAccount(Account account, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public async Task DeleteAccount(int id, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public async Task UpdateAccount(Account account, CancellationToken token)
        {
            throw new System.NotImplementedException();
        }

        public void Dispose()
        {
            throw new System.NotImplementedException();
        }
    }
}