using System;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using piggy_bank_uwp.Context.Entities;

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
        Task<Operation[]> GetOperations(CancellationToken token);
        Task<bool> HaveOperation(int id, CancellationToken token);
        Task CreateOperation(Operation newOperation, CancellationToken token);
        Task UpdateOperation(Operation operation, CancellationToken token);
    }
}