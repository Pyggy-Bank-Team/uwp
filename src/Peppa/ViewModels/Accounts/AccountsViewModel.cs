using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Peppa.ViewModels.Interface;
using Peppa.Interface.Models;
using piggy_bank_uwp.Enums;

namespace Peppa.ViewModels.Accounts
{
    public class AccountsViewModel : BaseViewModel, IBaseViewModel
    {
        private readonly IAccountsModel _model;

        public AccountsViewModel(IAccountsModel model)
        {
            _model = model;
            List = new ObservableCollection<AccountViewModel>();
        }

        public async Task Initialization()
        {
            var accounts = await _model.GetAccounts(GetToken());
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

        //TODO Split on two separated methods
        internal async Task UpdateData()
        {
            switch (SelectedItem?.Action)
            {
                case ActionType.Save when SelectedItem?.IsNew == true:
                    await _model.CreatedAccount(SelectedItem.MakeAccountEntity(), GetToken());
                    break;
                case ActionType.Save when SelectedItem?.IsNew == false:
                    await _model.UpdateAccount(SelectedItem.MakeAccountEntity(), GetToken());
                    break;
                case ActionType.Delete:
                    await _model.DeleteAccount(SelectedItem.Id, GetToken());
                    break;
            }
        }

        public void RaiseBalance()
        {
            RaisePropertyChanged(nameof(List));
            RaisePropertyChanged(nameof(TotalBalance));
        }

        private CancellationToken GetToken()
            => new CancellationTokenSource(TimeSpan.FromMinutes(1)).Token;

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