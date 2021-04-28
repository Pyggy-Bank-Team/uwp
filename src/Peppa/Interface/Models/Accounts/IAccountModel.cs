using System.Threading;
using System.Threading.Tasks;

namespace Peppa.Interface.Models.Accounts
{
    public interface IAccountModel
    {
        Task Save(CancellationToken token);
        Task Update(CancellationToken token);
        Task Delete(CancellationToken token);
        double Balance { get; set; }
    }
}