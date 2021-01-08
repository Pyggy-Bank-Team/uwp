using piggy_bank_uwp.Interface.Models;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Interface;
using Peppa.Interface.Services;
using System.Collections.Generic;
using Peppa.Context.Entities;

namespace Peppa.Models
{
    public class OperationsModel : IOperationsModel
    {
        private readonly IPiggyRepository _repository;
        private readonly IOperationService _service;
        private readonly Dictionary<string, string> _availableCurrencies;       

        public OperationsModel(IPiggyRepository repository, IOperationService service)
        {
            _repository = repository;
            _service = service;
            _availableCurrencies = new Dictionary<string, string>
            {
                {"RUB", "₽"},
                {"BYN", "Br"},
                {"UAH", "₴"},
                {"KZT", "₸"},
                {"USD", "$"},
                {"EUR", "€"}
            };
        }

        public IPiggyRepository Repository => _repository;

        public async Task<Operation[]> GetOperations(CancellationToken token)
        {
            //If user is logged in then sync accounts
            if (_service.IsAuthorized)
            {
                var pageResult = await _service.GetOperations(token);
                if (pageResult != null)
                {
                    foreach (var operation in pageResult.Result)
                    {
                        var entity = new Operation
                        {
                            Id = operation.Id,
                            IsDeleted = operation.IsDeleted,
                            AccountTitle = operation.Account.Title,
                            ToTitle = operation.ToAccount?.Title,
                            CategoryType = operation.Category?.Type,
                            CategoryHexColor = operation.Category?.HexColor,
                            CategoryTitle = operation.Category?.Title,
                            Amount = operation.Amount,
                            Type = operation.Type,
                            CreatedOn = operation.Date
                        };

                        if (await _repository.HaveOperation(operation.Id, token))
                            await _repository.UpdateOperation(entity, token);
                        else
                            await _repository.CreateOperation(entity, token);
                    }
                }
            }

            return await _repository.GetOperations(token);
        }

        public Task<Account[]> GetAccounts(CancellationToken token)
           => _repository.GetAccounts(token, all: false);

        public Task<Category[]> GetCategories(CancellationToken token)
            => _repository.GetCategories(token, all: false);

        public void Dispose()
        {
            _repository?.Dispose();
        }

        public async Task<Operation> GetBudgetOperation(int operationId, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var response = await _service.GetBudgetOperation(operationId, token);
                if (response != null)
                {
                    var entity = new Operation
                    {
                        Id = response.Id,
                        CategoryId = response.CategoryId,
                        AccountId  = response.AccountId,
                        Amount = response.Amount,
                        CreatedOn = response.Date,
                        Type = response.Type,
                        Comment = response.Comment
                    };

                    await _repository.AddOrUpdateOperation(entity, token);
                }
            }

            return await _repository.GetOperation(operationId, token);
        }

        public async Task<Operation> GetTransaferOperation(int operationId, CancellationToken token)
        {
            if (_service.IsAuthorized)
            {
                var response = await _service.GetTransferOperation(operationId, token);
                if (response != null)
                {
                    var entity = new Operation
                    {
                        Id = response.Id,
                        AccountId = response.FromId,
                        ToId = response.ToId,
                        Amount = response.Amount,
                        CreatedOn = response.Date,
                        Type = response.Type,
                        Comment = response.Comment
                    };

                    await _repository.AddOrUpdateOperation(entity, token);
                }
            }

            return await _repository.GetOperation(operationId, token);
        }
    }
}