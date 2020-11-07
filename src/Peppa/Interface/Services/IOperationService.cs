using System.Threading.Tasks;
using piggy_bank_uwp.Contracts.Responses;

namespace Peppa.Interface.Services
{
    public interface IOperationService
    {
        Task<OperationResponse[]> GetOperations();
    }
}