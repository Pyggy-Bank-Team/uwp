using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using piggy_bank_uwp.Interface;
using piggy_bank_uwp.ViewModels.Interface;
using piggy_bank_uwp.Workers;

namespace piggy_bank_uwp.ViewModels.Accounts
{
    public class AccountsViewModel : BaseViewModel, IBaseViewModel
    {
        private readonly IAccountService _service;

        public AccountsViewModel(IAccountService service)
        {
            _service = service;
            List = new ObservableCollection<AccountViewModel>();
        }

        public async void Initialization()
        {
            var accounts = await _service.GetAccounts();
            if (accounts != null)
            {
                List = new ObservableCollection<AccountViewModel>(accounts.Select(a => new AccountViewModel(a)));
                RaisePropertiesChanged();
            }
        }

        public void Finalization()
        {
            throw new NotImplementedException();
        }

        internal void UpdateData()
        {
        }

        public void RaiseBalance()
        {
            RaisePropertyChanged(nameof(List));
            RaisePropertyChanged(nameof(TotalBalance));
        }

        public string TotalBalance
        {
            get
            {
                if (List.Count > 0)
                {
                    var sum = List.Where(l => !l.IsArchived).Sum(l => l.Balance);
                    return $"{sum} {List.FirstOrDefault()?.Currency}";
                }

                return string.Empty;
            }
        }

        public ObservableCollection<AccountViewModel> List { get; private set; }
    }
}