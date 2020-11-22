using System;
using Peppa.Enums;
using piggy_bank_uwp.Context.Entities;
using piggy_bank_uwp.Enums;

namespace Peppa.ViewModels.Operations
{
    public class OperationViewModel : BaseViewModel
    {
        public OperationViewModel()
        {
            IsNew = true;
        }

        public OperationViewModel(Operation operation)
        {
            IsNew = false;
            CategoryId = operation.CategoryId;
            CategoryType = operation.CategoryType;
            CategoryHexColor = operation.CategoryHexColor;
            Amount = operation.Amount;
            AccountId = operation.AccountId;
            AccountTitle = operation.AccountTitle;
            Comment = operation.Comment;
            Type = operation.Type;
            PlanDate = operation.PlanDate;
            FromTitle = operation.FromTitle;
            ToTitle = operation.ToTitle;
            IsDeleted = operation.IsDeleted;
            Id = operation.Id;
            CreatedOn = operation.CreatedOn;
        }
        
        public bool IsNew { get; set; }
        
        public long? CategoryId { get; set; }
    
        public CategoryType? CategoryType { get; set; }

        public string CategoryHexColor { get; set; }

        public decimal? Amount { get; set; }

        public long? AccountId { get; set; }

        public string AccountTitle { get; set; }

        public string Comment { get; set; }

        public OperationType Type { get; set; }

        public DateTime? PlanDate { get; set; }

        public string FromTitle { get; set; }
  
        public string ToTitle { get; set; }

        public bool IsDeleted { get; set; }
        
        public int Id { get; }
        
        public DateTime CreatedOn { get; set; }
    }
}
