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
            _dataContext = (CategoriesViewModel)App.ServiceProvider.GetService(typeof(CategoriesViewModel));
            DataContext = _dataContext;

            await _dataContext.Initialization();
        }

        private void OnAddedCategoryClick(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(EditCategoryPage), new CategoryViewModel());
        }

        private void OnCategoryItemClick(object sender, ItemClickEventArgs e)
        {
            Frame.Navigate(typeof(EditCategoryPage), e.ClickedItem);
        }
    }
}
