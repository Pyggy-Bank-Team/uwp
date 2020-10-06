using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Peppa.ViewModels.Categoies;
using Peppa.ViewModels.Categories;

namespace Peppa.Views.Categories
{
    public sealed partial class CategoriesPage : Page
    {
        private CategoriesViewModel _dataContext;

        public CategoriesPage()
        {
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            UpdateProgressRing.Visibility = Visibility.Visible;

            _dataContext = (CategoriesViewModel)App.ServiceProvider.GetService(typeof(CategoriesViewModel));
            DataContext = _dataContext;

            var selectedItem = _dataContext.SelectedItem;
            if (selectedItem != null)
                await _dataContext.UpdateData();

            await _dataContext.Initialization();
            _dataContext.SelectedItem = null;

            UpdateProgressRing.Visibility = Visibility.Collapsed;
        }

        private void OnAddedCategoryClick(object sender, RoutedEventArgs e)
        {
            var newCategory = new CategoryViewModel();
            _dataContext.SelectedItem = newCategory;
            Frame.Navigate(typeof(EditCategoryPage), newCategory);
        }

        private void OnCategoryItemClick(object sender, ItemClickEventArgs e)
        {
            _dataContext.SelectedItem = e.ClickedItem as CategoryViewModel;
            Frame.Navigate(typeof(EditCategoryPage), e.ClickedItem);
        }
    }
}
