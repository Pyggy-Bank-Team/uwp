using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Peppa.ViewModels.Operations;
using System.ComponentModel;

namespace Peppa.Interface.ViewModels
{
    public interface IOperationsViewModel : INotifyPropertyChanged
    {
        Task Initialization();
        void OnOperationClick(object sender, ItemClickEventArgs e);
        void OnAddOperationClick(object sender, RoutedEventArgs e);
        void OnNextButtonClick(object sender, RoutedEventArgs e);
        void OnPreviousButtonClick(object sender, RoutedEventArgs e);
        ObservableCollection<OperationListViewItemViewModel> Operations { get; }
        bool IsProgressShow { get; }
        bool CanPreviousButtonClick { get; }
        bool CanNextButtonClick { get; }
        string DummyText { get; }
    }
}