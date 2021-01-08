using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Responses;
using piggy_bank_uwp.Contracts;

namespace Peppa.Interface.Services
{
    public interface IOperationService : IAuthorization
    {
        Task<PageResult<OperationResponse>> GetOperations(CancellationToken token);
        Task<BudgetOperationResponse> GetBudgetOperation(int id, CancellationToken token);
        Task<TransferOperationResponse> GetTransferOperation(int id, CancellationToken token);
    }
}