using System;
using System.Threading.Tasks;
using Peppa.Context.Entities;

namespace Peppa.Interface.Models
{
    public interface IAccountsModel : IDisposable
    {
        Task<Account[]> GetAccounts();
        Task CreatedAccount();
        Task DeleteAccount();
        Task UpdateAccount();
    }
}