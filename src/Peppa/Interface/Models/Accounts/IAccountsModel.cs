using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;

namespace Peppa.Interface.Models.Accounts
{
    public interface IAccountsModel : IDisposable
    {
        #region Old
        Task<Account[]> GetAccounts(CancellationToken token);
        Task CreatedAccount(Account account, CancellationToken token);
        Task DeleteAccount(int id, CancellationToken token);
        Task UpdateAccount(Account account, CancellationToken token);
        #endregion
        Task<IAccountModel> CreateNewAccount(CancellationToken token);
        Task UpdateAccounts(CancellationToken token);
        Task SaveAccount(IAccountModel newAccount, CancellationToken token);
        Task UpdateAccount(IAccountModel account, CancellationToken token);
        Task DeleteAccount(IAccountModel account, CancellationToken token);
        List<IAccountModel> Accounts { get; }
        double TotalAmount { get; }
    }
}