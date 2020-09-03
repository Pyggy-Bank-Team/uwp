using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Peppa.Contracts.Requests;
using Peppa.Interface;
using Peppa.ViewModels.Interface;

namespace Peppa.ViewModels.Accounts
{
    public class AccountsViewModel : BaseViewModel, IBaseViewModel
    {
        private readonly IAccountService _service;

        public AccountsViewModel(IAccountService service)
        {
            _service = service;
            List = new ObservableCollection<AccountViewModel>();
        }

        public async Task Initialization()
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

        internal async Task UpdateData()
        {
            var request = new AccountRequest
            {
                Balance = SelectedItem.Balance,
                Currency = SelectedItem.Currency,
                IsArchived = SelectedItem.IsArchived,
                Title = SelectedItem.Title,
                Type = (long)SelectedItem.Type
            };

            await _service.CreateAccount(request);
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

        public AccountViewModel SelectedItem { get; set; }
    }
}