using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Contracts;
using Peppa.Contracts.Requests;
using Peppa.Contracts.Responses;
using Peppa.Contracts.Results;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Services;

namespace Peppa.Services.PiggyService
{
    public class OperationService : PiggyServiceBase, IOperationService
    {
        public OperationService(IHttpClientFactory httpClientFactory, ISettingsService settingsService)
            : base(httpClientFactory, settingsService)
        {
            
        }

        public Task<PageResult<OperationResponse>> GetOperations(int page, CancellationToken token)
            => Get<PageResult<OperationResponse>>($"operations?page={page}", token);

        public Task<BudgetOperationResponse> GetBudgetOperation(int id, CancellationToken token)
            => Get<BudgetOperationResponse>($"operations/budget/{id}", token);

        public Task<TransferOperationResponse> GetTransferOperation(int id, CancellationToken token)
             => Get<TransferOperationResponse>($"operations/transfer/{id}", token);

        public Task<bool> CreateBudgetOperation(CreateBudgetOperationRequest request, CancellationToken token)
            => Post("operations/budget", request, token);

        public Task<bool> CreateTransferOperation(CreateTransferOperationRequest request, CancellationToken token)
            => Post("operations/transfer", request, token);

        public Task<bool> UpdateBudgetOperation(int id, UpdateBudgetOperationRequest request, CancellationToken token)
            => Put($"operations/budget/{id}", request, token);

        public Task<bool> UpdateTransferOperation(int id, UpdateTransferOperationRequest request, CancellationToken token)
             => Put($"operations/transfer/{id}", request, token);
        
        public Task<bool> DeleteTransferOperation(int id, CancellationToken token)
            => Delete($"operations/transfer/{id}", token);
        
        public Task<bool> DeleteBudgetOperation(int id, CancellationToken token)
            => Delete($"operations/budget/{id}", token);
    }
}