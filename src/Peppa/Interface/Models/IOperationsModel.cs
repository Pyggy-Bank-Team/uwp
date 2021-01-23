using System;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using Peppa.Dto;

namespace Peppa.Interface.Models
{
    public interface IOperationsModel : IDisposable
    {
        Task<PageResult<Operation>> GetOperations(int page, CancellationToken token);
        Task<Account[]> GetAccounts(bool all, CancellationToken token);
        Task<Category[]> GetCategories(bool all, CancellationToken token);
        Task<Operation> GetBudgetOperation(int operationId, CancellationToken token);
        Task<Operation> GetTransferOperation(int  operationId, CancellationToken token);
        Task CreateBudgetOperation(Operation operation, CancellationToken token);
        Task CreateTransferOperation(Operation operation, CancellationToken token);
    }
}
