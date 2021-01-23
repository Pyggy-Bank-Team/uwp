using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;

namespace Peppa.Interface.Services
{
    public interface IOperationService : IAuthorization
    {
        Task<PageResult<OperationResponse>> GetOperations(int page, CancellationToken token);
        Task<BudgetOperationResponse> GetBudgetOperation(int id, CancellationToken token);
        Task<TransferOperationResponse> GetTransferOperation(int id, CancellationToken token);
        Task<bool> CreateBudgetOperation(CreateBudgetOperationRequest request, CancellationToken token);
        Task<bool> CreateTransferOperation(CreateTransferOperationRequest request, CancellationToken token);
    }
}