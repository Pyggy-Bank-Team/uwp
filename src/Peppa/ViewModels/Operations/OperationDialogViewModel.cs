using System;
using System.Collections.Generic;
using System.Linq;
using Peppa.Dto;
using Peppa.Enums;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models.Operations;
using Peppa.Interface.WindowsService;

namespace Peppa.ViewModels.Operations
{
    public class OperationDialogViewModel : BaseViewModel
    {
        private readonly IToastService _toastService;
        private readonly ILocalizationService _localizationService;
        private Account _selectedFromAccount;
        private Account _selectedToAccount;
        private Category _selectedCategory;
        private bool _isExpense;
        private bool _isIncome;
        private bool _isTransfer;

        public OperationDialogViewModel(IOperationModel model, OperationViewType viewType, IToastService toastService, ILocalizationService localizationService)
        {
            _toastService = toastService;
            _localizationService = localizationService;
            Model = model;
            IsNew = model.IsNew;
            switch (viewType)
            {
                case OperationViewType.Income:
                    IsIncome = true;
                    break;
                case OperationViewType.Expense:
                    IsExpense = true;
                    break;
                case OperationViewType.Transfer:
                    IsTransfer = true;
                    break;
            }

            Accounts = new List<Account>();
            Categories = new List<Category>();
        }

        public async void OnLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var token = GetCancellationToken();

            try
            {
                await Model.UpdateData(token);
                await Model.UpdateAccounts(!Model.IsNew, token);
                await Model.UpdateCategories(!Model.IsNew, token);
            }
            catch
            {
                _toastService.ShowNotification("SoS", _localizationService.GetTranslateByKey(Localization.OopsError));
            }
            
            Accounts = Model.Accounts;

            if (IsExpense)
                Categories = Model.Categories.Where(c => c.Type == CategoryType.Expense).ToList();
            
            if (IsIncome)
                Categories = Model.Categories.Where(c => c.Type == CategoryType.Income).ToList();

            RaisePropertyChanged(nameof(Accounts));
            RaisePropertyChanged(nameof(Categories));

            _selectedFromAccount = Accounts.FirstOrDefault(a => a.Id == Model.AccountId);
            _selectedCategory = Categories.FirstOrDefault(c => c.Id == Model.CategoryId);
            _selectedToAccount = Accounts.FirstOrDefault(a => a.Id == Model.ToAccountId);

            RaisePropertyChanged(nameof(SelectedFromAccount));
            RaisePropertyChanged(nameof(SelectedCategory));
            RaisePropertyChanged(nameof(SelectedToAccount));
        }

        private async void UpdateCategories()
        {
            try
            {
                await Model.UpdateCategories(!Model.IsNew, GetCancellationToken());
            }
            catch
            {
                _toastService.ShowNotification("SoS", _localizationService.GetTranslateByKey(Localization.OopsError));
            }

            if (IsExpense)
                Categories = Model.Categories.Where(c => c.Type == CategoryType.Expense).ToList();

            if (IsIncome)
                Categories = Model.Categories.Where(c => c.Type == CategoryType.Income).ToList();

            RaisePropertyChanged(nameof(Categories));
        }

        public List<Account> Accounts { get; private set; }

        public Account SelectedFromAccount
        {
            get => _selectedFromAccount;
            set
            {
                if (_selectedFromAccount == value)
                    return;

                _selectedFromAccount = value;
                Model.AccountId = _selectedFromAccount.Id;
                RaisePropertyChanged(nameof(SelectedFromAccount));
            }
        }

        public Account SelectedToAccount
        {
            get => _selectedToAccount;
            set
            {
                if (_selectedToAccount == value)
                    return;

                _selectedToAccount = value;
                Model.ToAccountId = _selectedToAccount.Id;
                RaisePropertyChanged(nameof(SelectedToAccount));
            }
        }

        public List<Category> Categories { get; private set; }

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory == value)
                    return;

                _selectedCategory = value;
                Model.CategoryId = _selectedCategory.Id;
                RaisePropertyChanged(nameof(SelectedCategory));
            }
        }

        public DialogResult Result { get; set; }

        public double Amount
        {
            get => Model.Amount;
            set
            {
                if (Model.Amount == value)
                    return;

                Model.Amount = value;
                RaisePropertyChanged(nameof(Amount));
            }
        }

        public string Comment
        {
            get => Model.Comment;
            set
            {
                if (Model.Comment == value)
                    return;
                Model.Comment = value;
                RaisePropertyChanged(nameof(Comment));
            }
        }

        public DateTime OperationDate
        {
            get => Model.OperationDate;
            set
            {
                if (Model.OperationDate == value || value == null)
                    return;

                Model.OperationDate = value;
                RaisePropertyChanged(nameof(OperationDate));
            }
        }

        public bool IsExpense
        {
            get => _isExpense;
            set
            {
                if (_isExpense == value)
                    return;

                _isExpense = value;
                Model.Type = OperationType.Budget;
                RaisePropertyChanged(nameof(IsExpense));
                RaisePropertyChanged(nameof(IsBudget));
                UpdateCategories();
            }
        }

        public bool IsIncome
        {
            get => _isIncome;
            set
            {
                if (IsIncome == value)
                    return;

                _isIncome = value;
                Model.Type = OperationType.Budget;
                RaisePropertyChanged(nameof(IsIncome));
                RaisePropertyChanged(nameof(IsBudget));
                UpdateCategories();
            }
        }

        public bool IsTransfer
        {
            get => _isTransfer;
            set
            {
                if (_isTransfer == value)
                    return;

                _isTransfer = value;
                Model.Type = OperationType.Transfer;
                RaisePropertyChanged(nameof(IsTransfer));
                RaisePropertyChanged(nameof(IsBudget));
            }
        }

        public bool IsBudget => IsExpense || IsIncome;

        public IOperationModel Model { get; }
        public bool IsNew { get; }
    }
}