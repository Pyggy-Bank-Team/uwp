﻿using System;
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
            IsBudget = Type == OperationType.Budget;
            CategoryHexColor = operation.CategoryHexColor;
            CategoryTitle = operation.CategoryTitle;
            CategoryType = operation.CategoryType ?? CategoryType.Undefined;
            AccountTitle = operation.AccountTitle;
            ToTitle = Type == OperationType.Budget ? operation.AccountTitle : operation.ToTitle;
            Amount = GetAmountValue(Type, CategoryType, operation.Amount);
            Date = operation.CreatedOn;
            Comment = operation.Comment;
            IsNew = false;
        }

        private static string GetAmountValue(OperationType type, CategoryType? categoryType, decimal amount)
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
            //stringBuilder.Append(CurrencySymbol);

            return stringBuilder.ToString();
        }

        public OperationType Type { get; set; }

        public CategoryType CategoryType { get; set; }

        public string CategoryHexColor { get; set; }

        public string CategoryTitle { get; set; }

        public string Amount { get; set; }

        public string AccountTitle { get; set; }

        public string CurrencySymbol { get; set; }

        public string Comment { get; set; }

        public string ToTitle { get; set; }

        public bool IsBudget { get; }

        public int Id { get; }

        public DateTime Date { get; set; }

        public bool IsNew { get; set; }
    }
}