using System.Collections.Generic;
using Peppa.Enums;
using Peppa.Interface.Models.Accounts;

namespace Peppa.ViewModels.Accounts
{
    public class AccountDialogViewModel : BaseViewModel
    {
        private bool _isCard;
        private bool _isCash;

        public AccountDialogViewModel(IAccountModel model)
        {
            Model = model;
            IsNew = Model.IsNew;
            Currency = Model.Currency;
            Currencies = new List<string> {Model.Currency};
        }

        public string Title
        {
            get => Model.Title;
            set
            {
                if (Model.Title == value)
                    return;

                Model.Title = value;
                RaisePropertyChanged(nameof(Title));
            }
        }

        public bool IsCard
        {
            get => _isCard;
            set
            {
                if (_isCard == value)
                    return;

                _isCard = value;
                Model.Type = AccountType.Card;
                RaisePropertyChanged(nameof(IsCard));
                RaisePropertyChanged(nameof(IsCash));
            }
        }

        public bool IsCash
        {
            get => _isCash;
            set
            {
                if (_isCash == value)
                    return;

                _isCash = value;
                Model.Type = AccountType.Cash;
                RaisePropertyChanged(nameof(IsCash));
                RaisePropertyChanged(nameof(IsCard));
            }
        }

        public double Balance
        {
            get => Model.Balance;
            set
            {
                if (Model.Balance == value)
                    return;

                Model.Balance = value;
                RaisePropertyChanged(nameof(Balance));
            }
        }

        public string Currency { get; set; }

        public List<string> Currencies { get; set; }

        public bool IsArchived
        {
            get => Model.IsArchived;
            set
            {
                if (Model.IsArchived == value)
                    return;

                Model.IsArchived = value;
                RaisePropertyChanged(nameof(IsArchived));
            }
        }

        public bool IsNew { get; }

        public DialogResult Result { get; set; }

        public IAccountModel Model { get; }
    }
}