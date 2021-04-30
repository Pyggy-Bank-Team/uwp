using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;

namespace Peppa.Interface.Models.Accounts
{
    public interface IAccountsModel : IDisposable
    {
        IAccountModel CreateNewAccount();
        Task UpdateAccounts(CancellationToken token);
        Task SaveAccount(IAccountModel newAccount, CancellationToken token);
        Task UpdateAccount(IAccountModel account, CancellationToken token);
        Task DeleteAccount(IAccountModel account, CancellationToken token);
        List<IAccountModel> Accounts { get; }
        double TotalAmount { get; }
        string CurrencyBase { get;}
    }
}