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

        public OperationsViewModel(IOperationsModel model, IToastService toastService, ILocalizationService localizationService)
        {
            _model = model;
            _toastService = toastService;
            _localizationService = localizationService;
            Operations = new ObservableCollection<OperationListViewItemViewModel>();
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

            Operations.Clear();

            foreach (var operation in _model.Operations)
                Operations.Add(new OperationListViewItemViewModel(operation, _localizationService));


            IsProgressShow = false;

            RaisePropertyChanged(nameof(IsProgressShow));
            RaisePropertyChanged(nameof(CanNextButtonClick));
            RaisePropertyChanged(nameof(CanPreviousButtonClick));
        }

        public async void OnOperationClick(object sender, ItemClickEventArgs e)
        {
            if (!(e.ClickedItem is OperationListViewItemViewModel selectedOperation))
                return;

            var editOperationDialog = new OperationDialog(new OperationDialogViewModel(selectedOperation.Model, selectedOperation.ViewType, _toastService, _localizationService))
            {
                PrimaryButtonText = _localizationService.GetTranslateByKey(Localization.Save),
                CloseButtonText = _localizationService.GetTranslateByKey(Localization.Cancel)
            };
            await editOperationDialog.ShowAsync();

            switch (editOperationDialog.Result)
            {
                case DialogResult.Save:
                    await _model.UpdateOperation(selectedOperation.Model, GetCancellationToken());
                    break;
                case DialogResult.Delete:
                    await _model.DeleteOperation(selectedOperation.Model, GetCancellationToken());
                    break;
            }
        }

        public async void OnAddOperationClick(object sender, RoutedEventArgs e)
        {
            var newOperation = new OperationListViewItemViewModel(_model.CreateNewOperation(), _localizationService);
            var editOperationDialog = new OperationDialog(new OperationDialogViewModel(newOperation.Model, newOperation.ViewType, _toastService, _localizationService))
            {
                PrimaryButtonText = _localizationService.GetTranslateByKey(Localization.Save),
                CloseButtonText = _localizationService.GetTranslateByKey(Localization.Cancel)
            };
            await editOperationDialog.ShowAsync();

            if (editOperationDialog.Result == DialogResult.Save)
                await _model.SaveOperation(newOperation.Model, GetCancellationToken());
        }

        public async void OnNextButtonClick(object sender, RoutedEventArgs e)
        {
            IsProgressShow = true;
            RaisePropertyChanged(nameof(IsProgressShow));

            if (_model.CurrentPageNumber > _model.TotalPages)
            {
                IsProgressShow = false;
                RaisePropertyChanged(nameof(IsProgressShow));
                RaisePropertyChanged(nameof(CanNextButtonClick));
                RaisePropertyChanged(nameof(CanPreviousButtonClick));
                return;
            }

            _model.CurrentPageNumber++;

            try
            {
                await _model.UpdateOperations(GetCancellationToken());
            }
            catch
            {
                _toastService.ShowNotification("SoS", _localizationService.GetTranslateByKey(Localization.OopsError));
            }

            Operations.Clear();

            foreach (var operation in _model.Operations)
                Operations.Add(new OperationListViewItemViewModel(operation, _localizationService));

            IsProgressShow = false;
            RaisePropertyChanged(nameof(IsProgressShow));
            RaisePropertyChanged(nameof(CanNextButtonClick));
            RaisePropertyChanged(nameof(CanPreviousButtonClick));
        }

        public async void OnPreviousButtonClick(object sender, RoutedEventArgs e)
        {
            IsProgressShow = true;
            RaisePropertyChanged(nameof(IsProgressShow));

            if (_model.CurrentPageNumber <= 1)
            {
                IsProgressShow = false;
                RaisePropertyChanged(nameof(IsProgressShow));
                RaisePropertyChanged(nameof(CanNextButtonClick));
                RaisePropertyChanged(nameof(CanPreviousButtonClick));
                return;
            }

            _model.CurrentPageNumber--;

            try
            {
                await _model.UpdateOperations(GetCancellationToken());
            }
            catch
            {
                _toastService.ShowNotification("SoS", _localizationService.GetTranslateByKey(Localization.OopsError));
            }

            Operations.Clear();

            foreach (var operation in _model.Operations)
                Operations.Add(new OperationListViewItemViewModel(operation, _localizationService));

            IsProgressShow = false;
            RaisePropertyChanged(nameof(IsProgressShow));
            RaisePropertyChanged(nameof(CanPreviousButtonClick));
            RaisePropertyChanged(nameof(CanNextButtonClick));
        }

        private void OnModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RaisePropertyChanged(e.PropertyName);
        }

        public ObservableCollection<OperationListViewItemViewModel> Operations { get; }

        public bool IsProgressShow { get; private set; }
        public bool CanPreviousButtonClick => _model.CurrentPageNumber != 1;
        public bool CanNextButtonClick => _model.TotalPages > _model.CurrentPageNumber;
    }
}