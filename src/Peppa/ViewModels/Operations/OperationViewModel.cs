﻿using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peppa.Context.Entities;
using Peppa.Enums;
using Peppa.Interface.Models;
using piggy_bank_uwp.Context.Entities;
using piggy_bank_uwp.Enums;

namespace Peppa.ViewModels.Operations
{
    public class OperationViewModel : BaseViewModel
    {
        private readonly IAccountsModel _accountsMode;
        private readonly ICategoriesModel _categoriesModel;

        public OperationViewModel()
        {
            IsNew = true;
            Type = OperationType.Budget;
            CategoryType = Enums.CategoryType.Expense;
        }

        public OperationViewModel(Operation operation)
        {
            IsNew = false;
            CategoryId = operation.CategoryId;
            CategoryType = operation.CategoryType;
            CategoryHexColor = operation.CategoryHexColor;
            CategoryTitle = operation.CategoryTitle;
            Amount = operation.Amount;
            AccountId = operation.AccountId;
            AccountTitle = operation.AccountTitle;
            CurrencySymbol = operation.Symbol;
            Comment = operation.Comment;
            Type = operation.Type;
            PlanDate = operation.PlanDate;
            FromTitle = operation.FromTitle;
            ToTitle = operation.ToTitle;
            IsDeleted = operation.IsDeleted;
            Id = operation.Id;
            CreatedOn = operation.CreatedOn;
        }

        public async Task<Category[]> GetCategories()
        {
            if (CategoryType == null)
                return null;

            var categories =  await _categoriesModel.GetCategories(GetToken());
            return categories.Where(c => c.Type == CategoryType).ToArray();
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
