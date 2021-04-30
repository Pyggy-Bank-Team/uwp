using System.Threading;
using System.Threading.Tasks;
using Peppa.Enums;

namespace Peppa.Interface.Models.Accounts
{
    public interface IAccountModel
    {
        long Id { get; }
        string Title { get; set; }
        string Currency { get; set; }
        double Balance { get; set; }
        AccountType Type { get; set; }
        bool IsArchived { get; set; }
        bool IsSynchronized { get; set; }
        bool IsNew { get; }
        Task Save(CancellationToken token);
        Task Update(CancellationToken token);
        Task Delete(CancellationToken token);
    }
}