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

            existAccount.IsDeleted = true;
            _context.Accounts.Update(existAccount);
            await _context.SaveChangesAsync(token);
        }

        public Task<Account[]> GetAccounts()
            => _context.Accounts.ToArrayAsync();

        public void Dispose()
            => _context?.Dispose();
    }
}