using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Peppa.Dialogs;
using Peppa.Enums;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models.Accounts;
using Peppa.Interface.ViewModels;
using Peppa.Interface.WindowsService;

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

            await UpdateAccounts();

            DummyText = null;

            if (List.Count == 0)
                DummyText = _localizationService.GetTranslateByKey(Localization.NoAccounts);

            IsProgressShow = false;
            RaisePropertyChanged(nameof(IsProgressShow));
            RaisePropertyChanged(nameof(DummyText));
        }

        public async void OnAddAccountClick(object sender, RoutedEventArgs e)
        {
            var newAccount = new AccountDialogViewModel(_model.CreateNewAccount());
            var editOperationDialog = new AccountDialog(newAccount)
            {
                PrimaryButtonText = _localizationService.GetTranslateByKey(Localization.Save),
                CloseButtonText = _localizationService.GetTranslateByKey(Localization.Cancel)
            };

            await editOperationDialog.ShowAsync();

            if (editOperationDialog.Result == DialogResult.Save)
            {
                await _model.SaveAccount(newAccount.Model, GetCancellationToken());
                await UpdateAccounts();

                DummyText = null;

                if (List.Count == 0)
                    DummyText = _localizationService.GetTranslateByKey(Localization.NoAccounts);

                RaisePropertyChanged(nameof(DummyText));
            }
        }

        public async void OnAccountItemClick(object sender, ItemClickEventArgs e)
        {
            if (!(e.ClickedItem is AccountListViewItemViewModel selectedAccount))
                return;

            var editOperationDialog = new AccountDialog(new AccountDialogViewModel(selectedAccount.Model))
            {
                PrimaryButtonText = _localizationService.GetTranslateByKey(Localization.Save),
                CloseButtonText = _localizationService.GetTranslateByKey(Localization.Cancel)
            };

            await editOperationDialog.ShowAsync();

            switch (editOperationDialog.Result)
            {
                case DialogResult.Save:
                    await _model.UpdateAccount(selectedAccount.Model, GetCancellationToken());
                    await UpdateAccounts();
                    break;
                case DialogResult.Delete:
                    await _model.DeleteAccount(selectedAccount.Model, GetCancellationToken());
                    await UpdateAccounts();
                    break;
            }

            DummyText = null;

            if (List.Count == 0)
                DummyText = _localizationService.GetTranslateByKey(Localization.NoAccounts);

            RaisePropertyChanged(nameof(DummyText));
        }

        private async Task UpdateAccounts()
        {
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

            TotalBalanceTitle = $"{_model.TotalAmount} {_model.CurrencyBase}";
            RaisePropertyChanged(nameof(TotalBalanceTitle));
        }

        public string TotalBalanceTitle { get; set; }

        public ObservableCollection<AccountListViewItemViewModel> List { get; private set; }

        public bool IsProgressShow { get; set; }

        public string DummyText { get; private set; }
    }
}