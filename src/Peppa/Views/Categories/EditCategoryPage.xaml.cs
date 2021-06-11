using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using System;
using Peppa.Dialogs;
using Peppa.Enums;
using Peppa.Extensions;
using Peppa.Services.Windows;
using Peppa.ViewModels.Categories;

namespace Peppa.Views.Categories
{

    public sealed partial class EditCategoryPage : Page
    {
        private CategoryDialogViewModel _categoryDialog;

        public EditCategoryPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _categoryDialog = e.Parameter as CategoryDialogViewModel;
            Types.ItemsSource = new[] { CategoryType.Income, CategoryType.Expense };

            if (!_categoryDialog.IsNew)
            {
                UpdateColor();
            }
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            _categoryDialog.Result = DialogResult.Delete;
            GoBack();
        }

        private async void OnSaveClick(object sender, RoutedEventArgs e)
        {
            if (SelectedColor.BorderBrush == null)
            {
                await DialogService
                    .GetInformationDialog(Localization.GetTranslateByKey(Localization.WarringCategoryContent))
                    .ShowAsync();

                return;
            }

            _categoryDialog.Result = DialogResult.Save;
            GoBack();
        }

        private void OnColorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItemBrush = (e.AddedItems[0] as Ellipse).Fill as SolidColorBrush;
            _categoryDialog.Color = selectedItemBrush.Color.ToHexColor();
            UpdateColor();
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
        {
            _categoryDialog.Result = DialogResult.Cancel;
            GoBack();
        }

        private void OnTitleSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Types.Width = e.NewSize.Width;
        }

        private void OnColorsGridViewSizeChanged(object sender, SizeChangedEventArgs e)
        {
            //ChooseColorButton.Width = e.NewSize.Width;
            SelectedColor.Width = e.NewSize.Width;
        }

        private async void OnChooseColorButtonClicked(object sender, RoutedEventArgs e)
        {
            var colorPicker = new ColorPickerDialog();
            await colorPicker.ShowAsync();

            _categoryDialog.Color = colorPicker.SelectedColor;
            UpdateColor();
        }

        private void GoBack()
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }

        private void UpdateColor()
        {
            SelectedColor.BorderBrush = null;

            foreach (Ellipse item in ColorsGridView.Items)
            {
                if (item.Tag.ToString().ToUpper() == _categoryDialog.Color.ToUpper())
                    SelectedColor.BorderBrush = new SolidColorBrush(_categoryDialog.Color.ToColor());
            }

            if (SelectedColor.BorderBrush == null)
            {
                SelectedColor.BorderBrush = new SolidColorBrush(_categoryDialog.Color.ToColor());
            }
        }
    }
}
