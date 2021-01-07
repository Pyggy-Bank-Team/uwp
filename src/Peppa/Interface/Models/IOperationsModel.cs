using System;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using Peppa.Interface;

namespace piggy_bank_uwp.Interface.Models
{
    public interface IOperationsModel : IDisposable
    {
        Task<Operation[]> GetOperations(CancellationToken token);

        Task<Account[]> GetAccounts(CancellationToken token);
        Task<Category[]> GetCategories(CancellationToken token);

        IPiggyRepository Repository { get; }
    }
}
