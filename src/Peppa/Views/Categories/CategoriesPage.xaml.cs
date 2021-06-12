using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Microsoft.Extensions.DependencyInjection;
using Peppa.Interface.ViewModels;

namespace Peppa.Views.Categories
{
    public sealed partial class CategoriesPage : Page
    {
        private ICategoriesViewModel _dataContext;

        public CategoriesPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _dataContext = App.ServiceProvider.GetService<ICategoriesViewModel>();
            await _dataContext.Initialization();
        }
    }
}
