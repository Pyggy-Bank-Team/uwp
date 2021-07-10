using Peppa.Enums;
using Peppa.Extensions;
using Peppa.Interface.Models.Categories;
using System.Collections.Generic;
using Windows.UI.Xaml.Media;

namespace Peppa.ViewModels.Categories
{
    public class CategoryDialogViewModel : BaseViewModel
    {
        private bool _isIncome;
        private bool _isExpense;
        private SolidColorBrush _color;

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

            IsNew = Model.IsNew;

            #region Init colors

            _color = Model.HexColor.ToSolidColorBrush();

            Colors = new List<SolidColorBrush>(Constants.Colors.Length);

            foreach (var color in Constants.Colors)
            {
                if (Model.HexColor.ToUpperInvariant() == color.ToUpperInvariant())
                    Colors.Add(_color);
                else
                    Colors.Add(color.ToSolidColorBrush());
            }

            #endregion
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

        public SolidColorBrush Color
        {
            get => _color;
            set
            {
                if (_color == value)
                    return;

                _color = value;
                Model.HexColor = value.ToHexColor();
                RaisePropertyChanged(nameof(Color));
            }
        }

        public List<SolidColorBrush> Colors { get; set; }

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
        
        public bool IsNew { get; }

        public ICategoryModel Model { get; }
    }
}
