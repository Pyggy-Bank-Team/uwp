using System;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;

namespace Peppa.Interface.Models
{
    public interface IAccountsModel : IDisposable
    {
        Task<Account[]> GetAccounts(CancellationToken token);
        Task CreatedAccount(Account account, CancellationToken token);
        Task DeleteAccount(int id, CancellationToken token);
        Task UpdateAccount(Account account, CancellationToken token);
    }
}