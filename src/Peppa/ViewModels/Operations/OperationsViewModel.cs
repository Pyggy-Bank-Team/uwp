using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Peppa.Interface.Models;
using Peppa.ViewModels.Interface;

namespace Peppa.ViewModels.Operations
{
    public class OperationsViewModel : BaseViewModel, IInitialization
    {
        private readonly IOperationsModel _model;
        private OperationViewModel _selectedOperaton;

        public OperationsViewModel(IOperationsModel model)
        {
            _model = model;
            Operations = new ObservableCollection<OperationViewModel>();
            IsProgressShow = false;
            
            _model.PropertyChanged += OnModelPropertyChanged;
        }

        public async Task Initialization()
        {
            
        }

        public async void OnOperationClick(object sender, ItemClickEventArgs e)
        {
            
        }

        public async void OnAddOperationClick(object sender, RoutedEventArgs e)
        {
            
        }

        public async void OnNextButtonClick(object sender, RoutedEventArgs e)
        {
            
        }
        
        public async void OnPreviousButtonClick(object sender, RoutedEventArgs e)
        {
            
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
            => RaisePropertyChanged(e.PropertyName);
        
        public ObservableCollection<OperationViewModel> Operations { get; }

        public OperationViewModel SelectedOperation
        {
            get => _selectedOperaton;
            set
            {
                if (_selectedOperaton != value)
                {
                    _selectedOperaton = value;
                    RaisePropertyChanged(nameof(SelectedOperation));
                }
            }
        }

        public int CurrentPageNumber
        {
            get => _model.CurrentPageNumber;
            set
            {
                if (_model.CurrentPageNumber != value)
                {
                    _model.CurrentPageNumber = value;
                    RaisePropertyChanged(nameof(CurrentPageNumber));
                }
            }
        }

        public int TotalPages
        {
            get => _model.TotalPages;
            set
            {
                if (_model.TotalPages != value)
                {
                    _model.TotalPages = value;
                    RaisePropertyChanged(nameof(TotalPages));
                }
            }
        }
        
        public bool IsProgressShow { get; set; }
        
        public bool CanPreviousButtonClick { get; set; }
        public bool CanNextButtonClick { get; set; }
    }
}
