using Windows.UI;
using Peppa.Enums;
using Peppa.Extensions;
using Peppa.Interface.Models.Categories;

namespace Peppa.ViewModels.Categories
{
    public class CategoryDialogViewModel : BaseViewModel
    {
        private bool _isIncome;
        private bool _isExpense;

        public CategoryDialogViewModel(ICategoryModel model)
        {
            Model = model;
            switch (Model.Type)
            {
                case CategoryType.Income:
                    _isIncome = true;
                    break;
                case CategoryType.Expense:
                    _isExpense = true;
                    break;
            }
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

        public Color Color
        {
            get => Model.HexColor.ToColor();
            set
            {
                if (Model.HexColor.ToColor() == value)
                    return;

                Model.HexColor = value.ToHexColor();
                RaisePropertyChanged(nameof(Color));
            }
        }

        public bool IsIncome
        {
            get => _isIncome;
            set
            {
                if (_isIncome == value)
                    return;

                _isIncome = value;
                Model.Type = CategoryType.Income;
                RaisePropertyChanged(nameof(IsIncome));
                RaisePropertyChanged(nameof(IsExpense));
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
                Model.Type = CategoryType.Expense;
                RaisePropertyChanged(nameof(IsExpense));
                RaisePropertyChanged(nameof(IsIncome));
            }
        }
        
        public DialogResult Result { get; set; }

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

        public ICategoryModel Model { get; }
    }
}
