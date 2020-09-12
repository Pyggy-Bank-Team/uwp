using Peppa.Enums;
using Peppa.Context.Entities;
using piggy_bank_uwp.Enums;
using Peppa.Workers;
using System.Globalization;

namespace Peppa.ViewModels.Accounts
{
    public class AccountViewModel : BaseViewModel
    {
        public AccountViewModel()
        {
            IsNew = true;
            var baseCurrency = SettingsWorker.Current.GetValue(Constants.BaseCurrency);
            Currency = baseCurrency == null ? RegionInfo.CurrentRegion.ISOCurrencySymbol : (string)baseCurrency;
        }

        internal AccountViewModel(Account model)
        {
            IsNew = false;
            Title = model.Title;
            Balance = model.Balance;
            Currency = model.Currency;
            IsArchived = model.IsArchived;
            IsDeleted = model.IsDeleted;
            Type = model.Type;
            Id = model.Id;
            IsSynchronized = model.IsSynchronized;
        }

        public Account MakeAccountEntity()
            => new Account
            {
                Id = Id,
                Title = Title,
                Balance = Balance,
                Currency = Currency,
                Type = Type,
                IsArchived = IsArchived,
                IsDeleted = IsDeleted,
                IsSynchronized = IsSynchronized
            };
        
        public string Title { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; }

        public bool IsArchived { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsSynchronized { get; set; }

        public AccountType Type { get; set; }

        public bool IsNew { get; set; }
        
        public bool NeedUpdate { get; set; }

        public ActionType Action { get; set; }

        public string CurrentBalance => $"{Balance} {Currency}";

        public int Id { get; }
    }
}
