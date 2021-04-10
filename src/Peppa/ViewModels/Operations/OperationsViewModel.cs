using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Peppa.Dialogs;
using Peppa.Enums;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models;
using Peppa.Interface.ViewModels;
using Peppa.Interface.WindowsService;
using Peppa.ViewModels.Interface;

namespace Peppa.ViewModels.Operations
{
    public class OperationsViewModel : BaseViewModel, IInitialization, IOperationsViewModel
    {
        private readonly IOperationsModel _model;
        private readonly IToastService _toastService;
        private readonly ILocalizationService _localizationService;
        private OperationViewModel _selectedOperation;

        public OperationsViewModel(IOperationsModel model, IToastService toastService, ILocalizationService localizationService)
        {
            _model = model;
            _toastService = toastService;
            _localizationService = localizationService;
            Operations = new ObservableCollection<OperationViewModel>();
            IsProgressShow = false;

            _model.PropertyChanged += OnModelPropertyChanged;
        }

        public async Task Initialization()
        {
            IsProgressShow = true;
            RaisePropertyChanged(nameof(IsProgressShow));

            try
            {
                await _model.UpdateOperations(GetCancellationToken());
            }
            catch
            {
                _toastService.ShowNotification("SoS", _localizationService.GetTranslateByKey(Localization.OopsError));
            }

            IsProgressShow = false;
            RaisePropertyChanged(nameof(IsProgressShow));
        }

        public async void OnOperationClick(object sender, ItemClickEventArgs e)
        {
            if (!(e.ClickedItem is OperationViewModel selectedOperation))
                return;
            
            var editOperationDialog = new OperationDialog(selectedOperation);
            await editOperationDialog.ShowAsync();

            switch (selectedOperation.Action)
            {
                case ActionType.Save:
                    await _model.UpdateOperation(selectedOperation.Model, GetCancellationToken());
                    break;
                case ActionType.Delete:
                    await _model.DeleteOperation(selectedOperation.Model, GetCancellationToken());
                    break;
            }
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
        {
            if (e.PropertyName == nameof(_model.Operations))
            {
                Operations.Clear();

                foreach (var operation in _model.Operations)
                    Operations.Add(new OperationViewModel(operation, _localizationService));

                RaisePropertyChanged(nameof(Operations));
            }
            else
                RaisePropertyChanged(e.PropertyName);
        }

        public ObservableCollection<OperationViewModel> Operations { get; }

        public OperationViewModel SelectedOperation
        {
            get => _selectedOperation;
            set
            {
                if (_selectedOperation != value)
                {
                    _selectedOperation = value;
                    RaisePropertyChanged(nameof(SelectedOperation));
                }
            }
        }

        public bool IsProgressShow { get; set; }
        public bool CanPreviousButtonClick => _model.CurrentPageNumber == 1;
        public bool CanNextButtonClick => _model.CurrentPageNumber == _model.TotalPages;
    }
}