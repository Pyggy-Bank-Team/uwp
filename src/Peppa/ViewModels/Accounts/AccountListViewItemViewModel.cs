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
        }
        
        public int Id { get; set; }

        public string Title { get; set; }
        
        public string TypeTitle { get; set; }
        
        public string ArchiveTitle { get; set; }

        public string BalanceWithCurrencySymbol { get; set; }
        public IAccountModel Model { get; }
    }
}
