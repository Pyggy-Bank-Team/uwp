using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Peppa.Dto;
using Peppa.Enums;
using Peppa.Interface.Models;

namespace Peppa.ViewModels.Operations
{
    public class OperationDialogViewModel : BaseViewModel
    {
        private Account _selectedFromAccount;
        private Account _selectedToAccount;
        private Category _selectedCategory;
        private bool _isExpense;
        private bool _isIncome;
        private bool _isTransfer;
        private string _str;

        public OperationDialogViewModel(IOperationModel model, OperationViewType viewType)
        {
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

            model.PropertyChanged += OnModelPropertyChanged;


            Temp = new List<string>();
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
            => RaisePropertyChanged(e.PropertyName);

        public async void OnLoaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            var token = GetCancellationToken();
            
            await Model.UpdateAccounts(Model.IsNew, token);
            await Model.UpdateCategories(token);

            _selectedFromAccount = Accounts.Last();
            RaisePropertyChanged(nameof(SelectedFromAccount));


            Temp = new List<string>
            {
                "Red",
                "Green",
                "Yellow"
            };


            RaisePropertyChanged(nameof(Temp));

            _str = "Red";
            RaisePropertyChanged(nameof(Selected));
        }

        public List<string> Temp { get; set; }

        public string Selected
        {
            get => _str;
            set
            {
                if (_str == value)
                    return;
                _str = value;
                RaisePropertyChanged(nameof(Selected));
            }
        }
        
        public List<Account> Accounts => Model.Accounts;
        
        public Account SelectedFromAccount
        {
            get => _selectedFromAccount;
            set
            {
                if (_selectedFromAccount == value)
                    return;
                
                _selectedFromAccount = value;
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
                RaisePropertyChanged(nameof(SelectedToAccount));
            }
        }

        public List<Category> Categories => Model.Categories;

        public Category SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory == value)
                    return;

                _selectedCategory = value;
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

        public DateTime? OperationDate
        {
            get => Model.OperationDate;
            set
            {
                if (Model.OperationDate == value || value == null)
                    return;
                Model.OperationDate = value.Value;
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
                RaisePropertyChanged(nameof(IsExpense));
                RaisePropertyChanged(nameof(IsBudget));
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
                RaisePropertyChanged(nameof(IsIncome));
                RaisePropertyChanged(nameof(IsBudget));
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
                RaisePropertyChanged(nameof(IsTransfer));
                RaisePropertyChanged(nameof(IsBudget));
            }
        }

        public bool IsBudget => IsExpense || IsIncome;
        
        public IOperationModel Model { get; }
        public bool IsNew { get; }
    }
}