using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Peppa.Interface.Models
{
    public interface IOperationsModel : IDisposable, INotifyPropertyChanged
    {
        Task UpdateOperations(CancellationToken token);
        Task SaveOperation(IOperationModel newOperation, CancellationToken token);
        Task UpdateOperation(IOperationModel operation, CancellationToken token);
        Task DeleteOperation(IOperationModel operation, CancellationToken token);
        List<IOperationModel> Operations { get; }
        int CurrentPageNumber { get; set; }
        int TotalPages { get; set; }
    }
}