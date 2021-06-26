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

        public Task CreateAccount(Account newAccount, CancellationToken token)
            => Add(newAccount, token);

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

        public Task<Account[]> GetAccounts(CancellationToken token, bool all = true)
            => all
            ? _context.Accounts.Where(a => !a.IsDeleted).ToArrayAsync(token)
            : _context.Accounts.Where(a => !a.IsDeleted && !a.IsArchived).ToArrayAsync(token);


        public Task<Category[]> GetCategories(CancellationToken token, bool all = true)
            => all
            ? _context.Categories.Where(c => !c.IsDeleted).ToArrayAsync(token)
            : _context.Categories.Where(c => !c.IsDeleted && !c.IsArchived).ToArrayAsync(token);

        public async Task<bool> HaveCategories(int id, CancellationToken token)
        {
            var existAccount = await _context.Categories.FirstOrDefaultAsync(a => a.Id == id, token);
            return existAccount != null;
        }

        public Task CreateCategory(Category newCategory, CancellationToken token)
            => Add(newCategory, token);

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

        public Task<Operation[]> GetOperations(CancellationToken token)
            => _context.Operations.Where(o => !o.IsDeleted).ToArrayAsync(token);

        public async Task<bool> HaveOperation(int id, CancellationToken token)
        {
            var existAccount = await _context.Operations.FirstOrDefaultAsync(a => a.Id == id, token);
            return existAccount != null;
        }

        public Task CreateOperation(Operation newOperation, CancellationToken token)
            => Add(newOperation, token);

        public async Task UpdateOperation(Operation operation, CancellationToken token)
        {
            if (operation == null)
                return;

            var existOperation = await _context.Operations.FirstOrDefaultAsync(a => a.Id == operation.Id, token);
            if (existOperation == null)
                return;

            existOperation.Id = operation.Id;
            existOperation.CategoryId = operation.CategoryId;
            existOperation.CategoryType = operation.CategoryType;
            existOperation.CategoryHexColor = operation.CategoryHexColor;
            existOperation.Amount = operation.Amount;
            existOperation.AccountTitle = operation.AccountTitle;
            existOperation.Comment = operation.Comment;
            existOperation.Type = operation.Type;
            existOperation.CreatedOn = operation.CreatedOn;
            existOperation.ToTitle = operation.ToTitle;
            existOperation.IsDeleted = operation.IsDeleted;

            _context.Operations.Update(existOperation);
            await _context.SaveChangesAsync(token);
        }

        public async Task AddOrUpdateOperation(Operation operation, CancellationToken token)
        {
            if (operation == null)
                return;

            var existOperation = await _context.Operations.FirstOrDefaultAsync(a => a.Id == operation.Id, token);

            if (existOperation == null)
                await Add(operation, token);
            else
            {
                existOperation.CategoryId = operation.CategoryId ?? existOperation.CategoryId;
                existOperation.AccountId = operation.AccountId ?? existOperation.AccountId;
                existOperation.ToId = operation.ToId ?? existOperation.ToId;
                existOperation.Amount = operation.Amount == default ? existOperation.Amount : operation.Amount;
                existOperation.Comment = operation.Comment ?? existOperation.Comment;
                existOperation.Type = operation.Type;
                existOperation.CreatedOn = operation.CreatedOn;

                _context.Operations.Update(existOperation);
                await _context.SaveChangesAsync(token);
            }
        }

        public Task<User> GetUser(CancellationToken token)
            => _context.Users.FirstAsync(token);

        public Task CreateUser(User newUser, CancellationToken token)
            => _context.Users.AddAsync(newUser, token);

        public async Task UpdateUser(User updatedUser, CancellationToken token)
        {
            var existedUser = await _context.Users.FirstAsync(token);

            existedUser.Email = updatedUser.Email;
            existedUser.CurrencyBase = updatedUser.CurrencyBase;
            existedUser.UserName = updatedUser.UserName;

            _context.Users.Update(existedUser);
            await _context.SaveChangesAsync(token);
        }

        public Task<Operation> GetOperation(int id, CancellationToken token)
            => _context.Operations.FirstOrDefaultAsync(o => o.Id == id, token);       

        public void Dispose()
            => _context?.Dispose();

        private async Task Add<T>(T entity, CancellationToken token) where T : class
        {
            if (entity == null)
                return;

            await _context.AddAsync(entity, token);
            await _context.SaveChangesAsync(token);
        }
    }
}