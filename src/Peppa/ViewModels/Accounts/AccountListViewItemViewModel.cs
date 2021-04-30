using Peppa.Enums;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models.Accounts;

namespace Peppa.ViewModels.Accounts
{
    public class AccountListViewItemViewModel
    {
        private readonly ILocalizationService _localizationService;

        public AccountListViewItemViewModel(IAccountModel model, ILocalizationService localizationService)
        {
            _localizationService = localizationService;
            Model = model;
            Title = Model.Title;
            TypeTitle = GetTypeTitle(model.Type, localizationService);
            BalanceWithCurrencySymbol = $"{Model.Balance.ToString("N2")} {Model.Currency}";
            ArchiveTitle = Model.IsArchived ? _localizationService.GetTranslateByKey(Localization.InArchive) : "";
        }

        private static string GetTypeTitle(AccountType type, ILocalizationService service)
        {
            switch (type)
            {
                case AccountType.Cash:
                    return service.GetTranslateByKey(Localization.Cash);
                case AccountType.Card:
                    return service.GetTranslateByKey(Localization.Card);
                default:
                    return "Null";
            }
        }
        
        public string Title { get; set; }
        public string TypeTitle { get; set; }
        public string ArchiveTitle { get; set; }
        public string BalanceWithCurrencySymbol { get; set; }
        public IAccountModel Model { get; }
    }
}
