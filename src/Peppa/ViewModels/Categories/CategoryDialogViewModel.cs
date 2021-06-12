using Windows.UI;
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

            #region Init colors

            Colors = new List<SolidColorBrush>
            {
                "#ffb900".ToSolidColorBrush(),
                "#ff8c00".ToSolidColorBrush(),
                "#f7630c".ToSolidColorBrush(),
                "#ca5010".ToSolidColorBrush(),
                "#da3b01".ToSolidColorBrush(),
                "#ef6950".ToSolidColorBrush(),
                "#e74856".ToSolidColorBrush(),
                "#e81123".ToSolidColorBrush(),
                "#ea005e".ToSolidColorBrush(),
                "#c30052".ToSolidColorBrush(),
                "#e3008c".ToSolidColorBrush(),
                "#bf0077".ToSolidColorBrush(),
                "#0078d7".ToSolidColorBrush(),
                "#0063b1".ToSolidColorBrush(),
                "#8e8cd8".ToSolidColorBrush(),
                "#6b69d6".ToSolidColorBrush(),
                "#8764b8".ToSolidColorBrush(),
                "#881798".ToSolidColorBrush(),
                "#0099bc".ToSolidColorBrush(),
                "#2d7d9a".ToSolidColorBrush(),
                "#038387".ToSolidColorBrush(),
                "#00b294".ToSolidColorBrush(),
                "#018574".ToSolidColorBrush(),
                "#00cc6a".ToSolidColorBrush(),
                "#10893e".ToSolidColorBrush(),
                "#107c10".ToSolidColorBrush(),
                "#498205".ToSolidColorBrush(),
                "#486860".ToSolidColorBrush(),
                "#515c6b".ToSolidColorBrush(),
                "#7e735f".ToSolidColorBrush(),
                "#847545".ToSolidColorBrush(),
                "#525e54".ToSolidColorBrush()
            };

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
            get => Model.HexColor.ToSolidColorBrush();
            set
            {
                if (Model.HexColor.ToSolidColorBrush() == value)
                    return;

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

        public ICategoryModel Model { get; }
    }
}
