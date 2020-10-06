using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;
using System;
using Peppa.Services;
using Peppa.ViewModels.Categoies;
using Peppa.Enums;
using piggy_bank_uwp.Dialogs;
using piggy_bank_uwp.Enums;
using Peppa.Extensions;

namespace Peppa.Views.Categories
{

    public sealed partial class EditCategoryPage : Page
    {
        //private CategoryViewModel _category;
        private CategoryViewModel _category;

        public EditCategoryPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _category = e.Parameter as CategoryViewModel;
            Types.ItemsSource = Enum.GetValues(typeof(CategoryType));

            if(!_category.IsNew)
            {
                foreach (Ellipse item in ColorsGridView.Items)
                {
                    if (item.Tag.ToString().ToUpper() == _category.HexColor.ToUpper())
                        ColorsGridView.SelectedItem = item;
                }
            }
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            _category.Action = ActionType.Delete;
            GoBack();
        }

        private async void OnSaveClick(object sender, RoutedEventArgs e)
        {
            if(ColorsGridView.SelectedItem == null)
            {
                await DialogService
                    .GetInformationDialog(Localize.GetTranslateByKey(Localize.WarringCategoryContent))
                    .ShowAsync();

                return;
            }

            _category.Action = ActionType.Save;
            GoBack();
        }

        private void OnColorSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItemBrush = (e.AddedItems[0] as Ellipse).Fill as SolidColorBrush;
            var temp = selectedItemBrush.Color.ToString();
            _category.HexColor = selectedItemBrush.ToColor();
        }

        private void OnCloseClick(object sender, RoutedEventArgs e)
            => GoBack();

        private void OnTitleSizeChanged(object sender, SizeChangedEventArgs e)
        {
            Types.Width = e.NewSize.Width;
        }

        private void OnColorsGridViewSizeChanged(object sender, SizeChangedEventArgs e)
        {
            ChooseColorButton.Width = e.NewSize.Width;
        }

        private async void OnChooseColorButtonClicked(object sender, RoutedEventArgs e)
        {
            var dialog = new ColorPickerDialog();
            await dialog.ShowAsync();
        }

        private void GoBack()
        {
            if (Frame.CanGoBack)
                Frame.GoBack();
        }
    }
}
