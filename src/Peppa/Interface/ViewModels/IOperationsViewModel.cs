using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Peppa.ViewModels.Operations;

namespace Peppa.Interface.ViewModels
{
    public interface IOperationsViewModel
    {
        Task Initialization();
        void OnOperationClick(object sender, ItemClickEventArgs e);
        void OnAddOperationClick(object sender, RoutedEventArgs e);
        void OnNextButtonClick(object sender, RoutedEventArgs e);
        void OnPreviousButtonClick(object sender, RoutedEventArgs e);
        ObservableCollection<OperationViewModel> Operations { get; }
        OperationViewModel SelectedOperation { get; set; }
        bool IsProgressShow { get; set; }
        bool CanPreviousButtonClick { get; }
        bool CanNextButtonClick { get; }
    }
}