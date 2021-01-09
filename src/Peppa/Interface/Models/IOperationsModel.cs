using System;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using Peppa.Dto;

namespace piggy_bank_uwp.Interface.Models
{
    public interface IOperationsModel : IDisposable
    {
        Task<PageResult<Operation>> GetOperations(int page, CancellationToken token);
        Task<Account[]> GetAccounts(CancellationToken token);
        Task<Category[]> GetCategories(CancellationToken token);
        Task<Operation> GetBudgetOperation(int operationId, CancellationToken token);
        Task<Operation> GetTransaferOperation(int  operationId, CancellationToken token);
    }
}
