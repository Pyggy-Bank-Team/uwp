using Peppa.Context.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Peppa.Interface.Models
{
    public interface IOperationModel
    {
        Task<Account[]> GetAccounts(CancellationToken token);
        Task<Category[]> GetCategories(CancellationToken token);
    }
}
