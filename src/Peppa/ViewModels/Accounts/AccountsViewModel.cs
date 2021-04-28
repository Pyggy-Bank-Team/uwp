using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Peppa.Enums;
using Peppa.Interface.InternalServices;
using Peppa.ViewModels.Interface;
using Peppa.Interface.Models.Accounts;
using Peppa.Interface.WindowsService;
using System;

namespace Peppa.ViewModels.Accounts
{
    public class AccountsViewModel : BaseViewModel, IInitialization
    {
        private readonly IAccountsModel _model;
        private readonly IToastService _toastService;
        private readonly ILocalizationService _localizationService;

        public AccountsViewModel(IAccountsModel model, IToastService toastService, ILocalizationService localizationService)
        {
            _model = model;
            _toastService = toastService;
            _localizationService = localizationService;
            List = new ObservableCollection<AccountListViewItemViewModel>();
        }

        public async Task Initialization()
        {
            IsProgressShow = true;
            RaisePropertyChanged(nameof(IsProgressShow));

            try
            {
                await _model.UpdateAccounts(GetCancellationToken());
            }
            catch
            {
                _toastService.ShowNotification("SoS", _localizationService.GetTranslateByKey(Localization.OopsError));
            }
            
            List.Clear();

            foreach (var account in _model.Accounts)
                List.Add(new AccountListViewItemViewModel(account, _localizationService));

            IsProgressShow = false;
            
            RaisePropertyChanged(nameof(List));
            RaisePropertyChanged(nameof(IsProgressShow));

            TotalBalanceTitle = $"{_model.TotalAmount} {_model.CurrencyBase}";
            RaisePropertyChanged(nameof(TotalBalanceTitle));
        }

        //TODO Split on two separated methods
        internal async Task UpdateData()
        {
            switch (SelectedItem?.Action)
            {
                case DialogResult.Save when SelectedItem?.IsNew == true || SelectedItem?.IsSynchronized == false:
                    await _model.CreatedAccount(SelectedItem.MakeAccountEntity(), GetCancellationToken());
                    break;
                case DialogResult.Save when SelectedItem?.IsNew == false:
                    await _model.UpdateAccount(SelectedItem.MakeAccountEntity(), GetCancellationToken());
                    break;
                case DialogResult.Delete:
                    await _model.DeleteAccount(SelectedItem.Id, GetCancellationToken());
                    break;
            }
        }

        public void OnAddOperationClick(object sender, RoutedEventArgs e)
        {
            
        }

        public void OnAccountItemClick(object sender, ItemClickEventArgs e)
        {
            
        }

        public string TotalBalanceTitle { get; set; }

        public ObservableCollection<AccountListViewItemViewModel> List { get; private set; }

        public AccountViewModel SelectedItem { get; set; }
        
        public bool IsProgressShow { get; set; }
    }
}