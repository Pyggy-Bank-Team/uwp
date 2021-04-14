using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Peppa.Dto;
using Peppa.Enums;

namespace Peppa.Interface.Models
{
    public interface IOperationModel : INotifyPropertyChanged
    {
        decimal Amount { get; set; }
        OperationType Type { get; set; }
        string Comment { get; set; }
        DateTime OperationDate { get; set; }
        CategoryType CategoryType { get; }
        string CategoryHexColor { get; }
        string CategoryTitle { get;  }
        string AccountTitle { get; }
        string ToAccountTitle { get; }
        string Symbol { get; }
        List<Account> Accounts { get; }
        List<Category> Categories { get; set; }
        Task Save();
        Task Update();
        Task Delete();
        Task UpdateAccounts();
        Task UpdateCategories();
    }
}