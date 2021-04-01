using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Peppa.Enums;
using Peppa.ViewModels.Interface;
using Peppa.Interface.Models;

namespace Peppa.ViewModels.Accounts
{
    public class AccountsViewModel : BaseViewModel, IInitialization
    {
        private readonly IAccountsModel _model;

        public AccountsViewModel(IAccountsModel model)
        {
            _model = model;
            List = new ObservableCollection<AccountViewModel>();
        }

        public async Task Initialization()
        {
            var accounts = await _model.GetAccounts(GetCancellationToken());
            if (accounts != null)
            {
                List = new ObservableCollection<AccountViewModel>(accounts.Select(a => new AccountViewModel(a)).OrderBy(a => a.IsArchived));
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
                case ActionType.Save when SelectedItem?.IsNew == true || SelectedItem?.IsSynchronized == false:
                    await _model.CreatedAccount(SelectedItem.MakeAccountEntity(), GetCancellationToken());
                    break;
                case ActionType.Save when SelectedItem?.IsNew == false:
                    await _model.UpdateAccount(SelectedItem.MakeAccountEntity(), GetCancellationToken());
                    break;
                case ActionType.Delete:
                    await _model.DeleteAccount(SelectedItem.Id, GetCancellationToken());
                    break;
            }
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