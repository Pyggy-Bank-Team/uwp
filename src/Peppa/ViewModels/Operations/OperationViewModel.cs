using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Peppa.Enums;
using Peppa.Interface.Models;
using Peppa.ViewModels.Accounts;
using Peppa.ViewModels.Categories;
using piggy_bank_uwp.Context.Entities;
using piggy_bank_uwp.Enums;

namespace Peppa.ViewModels.Operations
{
    public class OperationViewModel : BaseViewModel
    {
        private readonly IOperationModel _model;

        public OperationViewModel(IOperationModel model)
        {
            IsNew = true;
            Type = OperationType.Budget;
            CategoryType = Enums.CategoryType.Expense;
            _model = model;
        }

        public OperationViewModel(Operation operation, IOperationModel model)
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
            _model = model;
        }

        public async Task<CategoryItemViewModel[]> GetCategories()
        {
            if (CategoryType == null)
                return null;

            var categories = await _model.GetCategories(GetToken());
            return categories.Where(c => c.Type == CategoryType).Select(c => new CategoryItemViewModel
            {
                Title = c.Title,
                HexColor = c.HexColor
            }).ToArray();
        }

        public async Task<AccountItemViewModel[]> GetAccounts()
        {
            var accounts = await _model.GetAccounts(GetToken());
            return accounts.Select(a => new AccountItemViewModel
            {
                Title = a.Title,
                BalanceWithCurrencySymbol = $"{a.Balance} {a.Currency}"
            }).ToArray();
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
