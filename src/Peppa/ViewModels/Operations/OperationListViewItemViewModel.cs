using System.Text;
using Peppa.Enums;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models;
using Peppa.Interface.Models.Operations;

namespace Peppa.ViewModels.Operations
{
    public class OperationListViewItemViewModel : BaseViewModel
    {
        private readonly ILocalizationService _localizationService;

        public OperationListViewItemViewModel(IOperationModel model, ILocalizationService localizationService)
        {
            Model = model;
            _localizationService = localizationService;
            ViewType = GetOperationViewType(model.CategoryType);
            TypeTitle = GetTypeTitle(model.CategoryType, localizationService);
            CategoryHexColor = model.CategoryHexColor;
            AmountTitle = GetAmountTitle(model.CategoryType, model.Amount, model.Symbol);
            OperationDate = model.OperationDate.ToString("d MMMM yyyy");
            Comment = model.Comment;
            OperationTitle = GetTitle(model.CategoryType, model.AccountTitle, model.CategoryTitle, model.ToAccountTitle);
        }

        private string GetTypeTitle(CategoryType categoryType, ILocalizationService service)
        {
            switch (categoryType)
            {
                case CategoryType.Income:
                    return service.GetTranslateByKey(Localization.Income);
                case CategoryType.Expense:
                    return service.GetTranslateByKey(Localization.Expense);
                case CategoryType.Undefined:
                    return service.GetTranslateByKey(Localization.Transfer);
                default:
                    return "null";
            }
        }
        
        private static OperationViewType GetOperationViewType(CategoryType categoryType)
        {
            switch (categoryType)
            {
                case CategoryType.Income:
                    return OperationViewType.Income;
                case CategoryType.Expense:
                    return OperationViewType.Expense;
               default:
                   return OperationViewType.Transfer;
            }
        }
        
        private static string GetAmountTitle(CategoryType categoryType, double amount, string symbol)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(categoryType == CategoryType.Expense ? "-" : "+");

            stringBuilder.Append(amount);
            stringBuilder.Append(" ");
            stringBuilder.Append(symbol);

            return stringBuilder.ToString();
        }

        private static string GetTitle(CategoryType categoryType, string accountTitle, string categoryTitle, string toTitle)
        {
            switch (categoryType)
            {
                case CategoryType.Income:
                    return $"{categoryTitle} > {accountTitle}";
                case CategoryType.Expense:
                    return $"{accountTitle}  > {categoryTitle}";
                case CategoryType.Undefined:
                    return $"{accountTitle}  > {toTitle}";
                default:
                    return "null";
            }
        }
        
        public OperationViewType ViewType { get; }
        public string TypeTitle { get;  }
        public string CategoryHexColor { get; }
        public string AmountTitle { get; }
        public string OperationDate { get; }
        public string Comment { get; }
        public string OperationTitle { get; }
        public IOperationModel Model { get; }
    }
}
