using piggy_bank_uwp.Context.Entities;
using piggy_bank_uwp.Interface.Models;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Interface;
using Peppa.Interface.Services;

namespace Peppa.Models
{
    public class OperationsModel : IOperationsModel
    {
        private readonly IPiggyRepository _repository;
        private readonly IOperationService _service;

        public OperationsModel(IPiggyRepository repository, IOperationService service)
            => (_repository, _service) = (repository, service);

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
                            Amount = operation.Amount,
                            AccountId = operation.AccountId,
                            AccountTitle = operation.AccountTitle,
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
    }
}