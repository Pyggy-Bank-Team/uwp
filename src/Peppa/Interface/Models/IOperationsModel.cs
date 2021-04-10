using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Models;

namespace Peppa.Interface.Models
{
    public interface IOperationsModel : IDisposable, INotifyPropertyChanged
    {
        Task UpdateOperations(CancellationToken token);
        Task SaveOperation(OperationModel newOperation, CancellationToken token);
        Task UpdateSelectedOperation(OperationModel operation, CancellationToken token);
        Task DeleteOperation(OperationModel operation, CancellationToken token);
        List<OperationModel> Operations { get; }
        int CurrentPageNumber { get; set; }
        int TotalPages { get; set; }
    }
}