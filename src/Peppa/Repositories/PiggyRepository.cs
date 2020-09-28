using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Peppa.Context;
using Peppa.Interface;
using Peppa.Context.Entities;

namespace Peppa.Repositories
{
    public class PiggyRepository : IPiggyRepository
    {
        private readonly PiggyContext _context;

        public PiggyRepository()
            => _context = new PiggyContext();

        public async Task CreateAccount(Account newAccount, CancellationToken token)
        {
            if (newAccount == null)
                return;

            await _context.AddAsync(newAccount, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task UpdateAccount(Account account, CancellationToken token)
        {
            if (account == null)
                return;

            var existAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == account.Id, token);
            if (existAccount == null)
                return;

            existAccount.Balance = account.Balance;
            existAccount.Currency = account.Currency;
            existAccount.Title = account.Title;
            existAccount.Type = account.Type;
            existAccount.IsArchived = account.IsArchived;
            existAccount.IsDeleted = account.IsDeleted;
            existAccount.IsSynchronized = account.IsSynchronized;

            _context.Accounts.Update(existAccount);
            await _context.SaveChangesAsync(token);
        }

        public async Task DeleteAccount(int id, CancellationToken token)
        {
            var existAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id, token);
            if (existAccount == null)
                return;

            _context.Accounts.Remove(existAccount);
            await _context.SaveChangesAsync(token);
        }

        public async Task<bool> HaveAccount(int id, CancellationToken token)
        {
            var existAccount = await _context.Accounts.FirstOrDefaultAsync(a => a.Id == id, token);
            return existAccount != null;
        }

        public Task<Account[]> GetAccounts(CancellationToken token)
            => _context.Accounts.Where(a => !a.IsDeleted).ToArrayAsync(token);

        public Task<Category[]> GetCategories(CancellationToken token)
            => _context.Categories.Where(c => !c.IsDeleted).ToArrayAsync(token);

        public async Task<bool> HaveCategories(int id, CancellationToken token)
        {
            var existAccount = await _context.Categories.FirstOrDefaultAsync(a => a.Id == id, token);
            return existAccount != null;
        }

        public async Task CreateCategory(Category newCategory, CancellationToken token)
        {
            if (newCategory == null)
                return;

            await _context.AddAsync(newCategory, token);
            await _context.SaveChangesAsync(token);
        }

        public async Task UpdateCategory(Category category, CancellationToken token)
        {
            if (category == null)
                return;

            var existCategory = await _context.Categories.FirstOrDefaultAsync(a => a.Id == category.Id, token);
            if (existCategory == null)
                return;
            
            existCategory.HexColor = category.HexColor;
            existCategory.Title = category.Title;
            existCategory.Type = category.Type;
            existCategory.IsArchived = category.IsArchived;
            existCategory.IsDeleted = category.IsDeleted;
            existCategory.IsSynchronized = category.IsSynchronized;

            _context.Categories.Update(existCategory);
            await _context.SaveChangesAsync(token);
        }

        public void Dispose()
            => _context?.Dispose();
    }
}