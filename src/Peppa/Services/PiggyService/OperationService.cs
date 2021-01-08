using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts.Responses;
using Peppa.Interface.Services;
using piggy_bank_uwp.Contracts;

namespace Peppa.Services.PiggyService
{
    public class OperationService : PiggyServiceBase, IOperationService
    {
        public OperationService(IHttpClientFactory httpClientFactory)
            : base(httpClientFactory) { }

        public Task<PageResult<OperationResponse>> GetOperations(CancellationToken token)
            => Get<PageResult<OperationResponse>>("operations", token);

        public Task<BudgetOperationResponse> GetBudgetOperation(int id, CancellationToken token)
            => Get<BudgetOperationResponse>($"operations/budget/{id}", token);

        public Task<TransferOperationResponse> GetTransferOperation(int id, CancellationToken token)
             => Get<TransferOperationResponse>($"operations/transfer/{id}", token);
    }
}