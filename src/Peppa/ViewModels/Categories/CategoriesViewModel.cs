using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Peppa.Dialogs;
using Peppa.Enums;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models.Categories;
using Peppa.Interface.ViewModels;
using Peppa.Interface.WindowsService;

namespace Peppa.ViewModels.Categories
{
    public class CategoriesViewModel : BaseViewModel, IInitialization, ICategoriesViewModel
    {
        private readonly ICategoriesModel _model;
        private readonly IToastService _toastService;
        private readonly ILocalizationService _localizationService;

        public CategoriesViewModel(ICategoriesModel model, IToastService toastService, ILocalizationService localizationService)
        {
            _model = model;
            _toastService = toastService;
            _localizationService = localizationService;
            List = new ObservableCollection<CategoryListViewItemViewModel>();
        }

        public async Task Initialization()
        {
            IsProgressShow = true;
            RaisePropertyChanged(nameof(IsProgressShow));

            await UpdateCategories();

            DummyText = null;

            if (List.Count == 0)
                DummyText = _localizationService.GetTranslateByKey(Localization.NoCategories);

            IsProgressShow = false;
            RaisePropertyChanged(nameof(IsProgressShow));
            RaisePropertyChanged(nameof(DummyText));
        }

        public async void OnAddCategoryClick(object sender, RoutedEventArgs e)
        {
            var newCategory = new CategoryDialogViewModel(_model.CreateNewCategory());
            var categoryDialog = new CategoryDialog(newCategory)
            {
                PrimaryButtonText = _localizationService.GetTranslateByKey(Localization.Save),
                CloseButtonText = _localizationService.GetTranslateByKey(Localization.Cancel)
            };

            await categoryDialog.ShowAsync();

            if (categoryDialog.Result == DialogResult.Save)
            {
                await _model.SaveCategory(newCategory.Model, GetCancellationToken());
                await UpdateCategories();

                DummyText = null;

                if (List.Count == 0)
                    DummyText = _localizationService.GetTranslateByKey(Localization.NoCategories);

                RaisePropertyChanged(nameof(DummyText));
            }
        }

        public async void OnCategoryItemClick(object sender, ItemClickEventArgs e)
        {
            if (!(e.ClickedItem is CategoryListViewItemViewModel selectedCategory))
                return;

            var editOperationDialog = new CategoryDialog(new CategoryDialogViewModel(selectedCategory.Model))
            {
                PrimaryButtonText = _localizationService.GetTranslateByKey(Localization.Save),
                CloseButtonText = _localizationService.GetTranslateByKey(Localization.Cancel)
            };

            await editOperationDialog.ShowAsync();

            switch (editOperationDialog.Result)
            {
                case DialogResult.Save:
                    await _model.UpdateCategory(selectedCategory.Model, GetCancellationToken());
                    await UpdateCategories();
                    break;
                case DialogResult.Delete:
                    await _model.DeleteCategory(selectedCategory.Model, GetCancellationToken());
                    await UpdateCategories();
                    break;
            }

            DummyText = null;

            if (List.Count == 0)
                DummyText = _localizationService.GetTranslateByKey(Localization.NoCategories);

            RaisePropertyChanged(nameof(DummyText));
        }

        private async Task UpdateCategories()
        {
            try
            {
                await _model.UpdateCategories(GetCancellationToken());
            }
            catch
            {
                _toastService.ShowNotification("SoS", _localizationService.GetTranslateByKey(Localization.OopsError));
            }

            List.Clear();

            foreach (var account in _model.Categories)
                List.Add(new CategoryListViewItemViewModel(account, _localizationService));
        }

        public ObservableCollection<CategoryListViewItemViewModel> List { get; }

        public bool IsProgressShow { get; set; }

        public string DummyText { get; private set; }
    }
}