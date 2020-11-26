using piggy_bank_uwp.Context.Entities;
using piggy_bank_uwp.Interface.Models;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Interface;
using Peppa.Interface.Services;
using System.Collections.Generic;

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
                            CategoryId = operation.CategoryId,
                            CategoryType = operation.CategoryType,
                            CategoryHexColor = operation.CategoryHexColor,
                            CategoryTitle = operation.CategoryTitle,
                            Amount = operation.Amount,
                            AccountId = operation.AccountId,
                            AccountTitle = operation.AccountTitle,
                            Currency = operation.Currency,
                            Symbol = GetSymbol(operation.Currency),
                            Comment = operation.Comment,
                            Type = operation.Type,
                            CreatedOn = operation.CreatedOn,
                            PlanDate = operation.PlanDate,
                            FromTitle = operation.FromTitle,
                            ToTitle = operation.ToTitle,
                            IsDeleted = operation.IsDeleted
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

        public void Dispose()
        {
            _repository?.Dispose();
        }

        private string GetSymbol(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                return null;

            try
            {
                return _availableCurrencies[currency];
            }
            catch
            {
                //TODO log
            }

            return null;
        }
    }
}