using System.Text;
using Peppa.Enums;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models;

namespace Peppa.ViewModels.Operations
{
    public class OperationViewModel : BaseViewModel
    {
        private readonly IOperationModel _model;
        private readonly ILocalizationService _localizationService;

        public OperationViewModel(IOperationModel model, ILocalizationService localizationService)
        {
            _model = model;
            _localizationService = localizationService;
            ViewType = GetOperationViewType(model.CategoryType);
            TypeTitle = GetTypeTitle(model.CategoryType, localizationService);
            CategoryHexColor = model.CategoryHexColor;
            CategoryTitle = model.CategoryTitle;
            AccountTitle = model.AccountTitle;
            ToTitle = model.ToAccountTitle;
            AmountTitle = GetAmountTitle(model.CategoryType, model.Amount, model.Symbol);
            OperationDate = model.OperationDate.ToShortDateString();
            Comment = model.Comment;
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
        
        private static string GetAmountTitle(CategoryType categoryType, decimal amount, string symbol)
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append(categoryType == CategoryType.Expense ? "-" : "+");

            stringBuilder.Append(amount);
            stringBuilder.Append(" ");
            stringBuilder.Append(symbol);

            return stringBuilder.ToString();
        }
        
        public ActionType Action { get; set; }
        public OperationViewType ViewType { get; }
        public string TypeTitle { get;  }
        public string CategoryHexColor { get; }
        public string CategoryTitle { get;  }
        public bool IsBudget => ViewType != OperationViewType.Transfer;
        public bool IsTransfer => ViewType == OperationViewType.Transfer;
        public string AccountTitle { get; }
        public string ToTitle { get; }
        public string AmountTitle { get; }
        public string OperationDate { get; }
        public string Comment { get; set; }
        public IOperationModel Model { get; }
        public bool CanDelete { get; set; }//If not equals a new operation
    }
}
