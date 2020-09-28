using System;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;

namespace Peppa.Interface
{
    public interface IPiggyRepository : IDisposable
    {
        Task CreateAccount(Account newAccount, CancellationToken token);
        Task UpdateAccount(Account account, CancellationToken token);
        Task DeleteAccount(int id,CancellationToken token);
        Task<bool> HaveAccount(int id, CancellationToken token);
        Task<Account[]> GetAccounts(CancellationToken token);
        Task<Category[]> GetCategories(CancellationToken token);
        Task<bool> HaveCategories(int id, CancellationToken token);
        Task CreateCategory(Category newCategory, CancellationToken token);
        Task UpdateCategory(Category category, CancellationToken token);
    }
}