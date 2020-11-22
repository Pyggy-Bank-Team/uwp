using System.Threading;
using System.Threading.Tasks;
using piggy_bank_uwp.Contracts;
using piggy_bank_uwp.Contracts.Responses;

namespace Peppa.Interface.Services
{
    public interface IOperationService : IAuthorization
    {
        Task<PageResult<OperationResponse>> GetOperations(CancellationToken token);
    }
}