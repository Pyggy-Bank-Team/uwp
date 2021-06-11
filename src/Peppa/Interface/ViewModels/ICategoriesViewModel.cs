using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Peppa.ViewModels.Categories;

namespace Peppa.Interface.ViewModels
{
   
    public interface ICategoriesViewModel : INotifyPropertyChanged
    {
        Task Initialization();
        ObservableCollection<CategoryListViewItemViewModel> List { get; }
        bool IsProgressShow { get; set; }
        void OnAddCategoryClick(object sender, RoutedEventArgs e);
        void OnCategoryItemClick(object sender, ItemClickEventArgs e);
    }
}