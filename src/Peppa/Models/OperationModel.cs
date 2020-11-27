using Peppa.Context.Entities;
using Peppa.Interface;
using Peppa.Interface.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Peppa.Models
{
    public class OperationModel : IOperationModel
    {
        private readonly IPiggyRepository _repository;

        public OperationModel(IPiggyRepository repository)
            => _repository = repository;

        public Task<Account[]> GetAccounts(CancellationToken token)
            => _repository.GetAccounts(token, all: false);

        public Task<Category[]> GetCategories(CancellationToken token)
            => _repository.GetCategories(token, all: false);
    }
}
