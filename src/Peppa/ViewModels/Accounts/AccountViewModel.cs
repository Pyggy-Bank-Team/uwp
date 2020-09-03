using piggy_bank_uwp.Entities;
using piggy_bank_uwp.Enums;

namespace piggy_bank_uwp.ViewModels.Accounts
{
    public class AccountViewModel : BaseViewModel
    {
        public AccountViewModel()
            => IsNew = true;

        internal AccountViewModel(Account model)
        {
            IsNew = false;
            Title = model.Title;
            Balance = model.Balance;
            Currency = model.Currency;
            IsArchived = model.IsArchived;
            Type = (AccountType)model.Type;
            Id = model.Id;
        }
        
        public string Title { get; set; }

        public decimal Balance { get; set; }

        public string Currency { get; set; }

        public bool IsArchived { get; set; }

        public AccountType Type { get; set; }

        public bool IsNew { get; set; }

        public string CurrentBalance => $"{Balance} {Currency}";

        public long Id { get; }
    }
}
