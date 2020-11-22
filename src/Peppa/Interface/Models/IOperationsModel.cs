using System;
using piggy_bank_uwp.Context.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace piggy_bank_uwp.Interface.Models
{
    public interface IOperationsModel : IDisposable
    {
        Task<Operation[]> GetOperations(CancellationToken token);
    }
}
