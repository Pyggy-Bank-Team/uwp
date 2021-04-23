using System;
using System.Text;
using Peppa.Context.Entities;
using Peppa.Enums;

namespace Peppa.ViewModels.Operations
{
    public class ListItemViewModel
    {
        public ListItemViewModel()
        {
            IsNew = true;
            Date = DateTime.UtcNow;
        }

        public ListItemViewModel(Operation operation)
        {
            Id = operation.Id;
            Type = operation.Type;
            CategoryHexColor = operation.CategoryHexColor;
            CategoryTitle = operation.CategoryTitle;
            CategoryType = operation.CategoryType ?? CategoryType.Undefined;
            AccountTitle = operation.AccountTitle;
            ToTitle = Type == OperationType.Budget ? operation.AccountTitle : operation.ToTitle;
            Amount = GetAmountValue(Type, CategoryType, operation.Amount, operation.Symbol);
            EntityAmount = operation.Amount;
            Date = operation.CreatedOn;
            Comment = operation.Comment;
            IsNew = false;
        }

        private static string GetAmountValue(OperationType type, CategoryType? categoryType, double amount, string symbol)
        {
            var stringBuilder = new StringBuilder();

            switch (type)
            {
                case OperationType.Transfer:
                    stringBuilder.Append("+");
                    break;
                default:
                    switch (categoryType)
                    {
                        case CategoryType.Income:
                            stringBuilder.Append("+");
                            break;
                        default:
                            stringBuilder.Append("-");
                            break;
                    }

                    break;
            }

            stringBuilder.Append(amount);
            stringBuilder.Append(" ");
            stringBuilder.Append(symbol);

            return stringBuilder.ToString();
        }
        
        public static implicit operator Operation(ListItemViewModel viewModel)
            => new Operation
            {
                Amount = viewModel.EntityAmount,
                Comment = viewModel.Comment,
                Id = viewModel.Id,
                AccountId = viewModel.Budget?.AccountId ?? viewModel.Transfer.FromId,
                CategoryId = viewModel.Budget?.CategoryId,
                ToId = viewModel.Transfer?.ToId,
                CreatedOn = viewModel.Date
            };

        public OperationType Type { get; set; }

        public CategoryType CategoryType { get; set; }

        public string CategoryHexColor { get; set; }

        public string CategoryTitle { get; set; }

        public string Amount { get; set; }
        
        public double EntityAmount { get; set;}

        public string AccountTitle { get; set; }

        public string CurrencySymbol { get; set; }

        public string Comment { get; set; }

        public string ToTitle { get; set; }

        public bool IsBudget => Type == OperationType.Budget;

        public int Id { get; }

        public DateTime Date { get; set; }

        public bool IsNew { get; set; }
        
        public DialogResult Action { get; set; }

        public BudgetOperationViewModel Budget { get; set; }
        public TransferOperationViewModel Transfer { get; set;}
    }
}