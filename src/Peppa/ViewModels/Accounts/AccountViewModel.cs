using piggy_bank_uwp.Entities;
using piggy_bank_uwp.Enums;
using piggy_bank_uwp.ViewModels.Interface;

namespace piggy_bank_uwp.ViewModels.Accounts
{
    public class AccountViewModel : BaseViewModel, IUpdateable
    {
        private readonly Account _model;
        public AccountViewModel()
        {
            _model  = new Account();
            IsNew = true;
        }

        internal AccountViewModel(Account model)
        {
            _model = model;
            IsNew = false;
        }

        public void Update()
        {
            RaisePropertiesChanged();
        }

        public string Title
        {
            get => _model.Title;
            set
            {
                if (_model.Title != value)
                {
                    _model.Title = value;
                }
            }
        }

        public decimal Balance
        {
            get => _model.Balance;
            set
            {
                if (_model.Balance != value)
                {
                    _model.Balance = value;
                }
            }
        }

        public bool IsArchived
            => _model.IsArchived;

        public AccountType Type
            => (AccountType)_model.Type;

        public bool IsNew { get; set; }

        public string CurrentBalance => $"{Balance} {Currency}";

        public string Currency => _model.Currency;

        public long Id => _model.Id;
    }
}
