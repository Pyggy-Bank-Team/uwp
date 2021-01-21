using System;
using System.Text;
using Peppa.Context.Entities;
using Peppa.Enums;

namespace Peppa.ViewModels.Operations
{
    public class OperationViewModel : BaseViewModel
    {
        public OperationViewModel(Operation operation)
        {
            IsNew = false;
            CategoryId = operation.CategoryId;
            CategoryType = operation.CategoryType;
            CategoryHexColor = operation.CategoryHexColor;
            CategoryTitle = operation.CategoryTitle;
            Amount = operation.Amount;
            AccountTitle = operation.AccountTitle;
            AccountId = operation.AccountId;
            CurrencySymbol = operation.Symbol;
            Comment = operation.Comment;
            Type = operation.Type;
            ToTitle = operation.ToTitle;
            IsDeleted = operation.IsDeleted;
            Id = operation.Id;
            CreatedOn = operation.CreatedOn;
            ViewType = GetViewType(Type, CategoryType ?? Enums.CategoryType.Undefined);
        }
        
        private static OperationViewType GetViewType(OperationType operationType, CategoryType categoryType)
        {
            if (operationType == OperationType.Transfer)
                return OperationViewType.Transfer;

            if (categoryType == Enums.CategoryType.Expense)
                return OperationViewType.Expense;
            else
                return OperationViewType.Income;
        }

        public bool IsNew { get; set; }

        public long? CategoryId { get; set; }

        public CategoryType? CategoryType { get; set; }

        public string CategoryHexColor { get; set; }

        public string CategoryTitle { get; set; }

        public decimal? Amount { get; set; }

        public long? AccountId { get; set; }

        public string AccountTitle { get; set; }

        public string CurrencySymbol { get; set; }

        public string Comment { get; set; }

        public OperationType Type { get; set; }

        public OperationViewType ViewType { get; set; }

        public DateTime? PlanDate { get; set; }

        public string FromTitle { get; set; }

        public string ToTitle { get; set; }

        public bool IsDeleted { get; set; }

        public string OperationValue
        {
            get
            {
                var stringBuilder = new StringBuilder();

                switch (Type)
                {
                    case OperationType.Transfer:
                        stringBuilder.Append("+");
                        break;
                    default:
                        switch (CategoryType)
                        {
                            case Enums.CategoryType.Income:
                                stringBuilder.Append("+");
                                break;
                            default:
                                stringBuilder.Append("-");
                                break;
                        }
                        break;
                }

                stringBuilder.Append(Amount);
                stringBuilder.Append(" ");
                stringBuilder.Append(CurrencySymbol);

                return stringBuilder.ToString();
            }
        }

        public string OperationTitle
        {
            get
            {
                var stringBuilder = new StringBuilder();

                switch (CategoryType)
                {
                    case Enums.CategoryType.Income:
                        stringBuilder.Append($"{CategoryTitle} > {AccountTitle}");
                        break;
                    default:
                        stringBuilder.Append($"{AccountTitle} > {CategoryTitle}");
                        break;
                }

                return stringBuilder.ToString();
            }
        }

        public int Id { get; }

        public DateTime CreatedOn { get; set; }
    }
}
