using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Peppa.Enums;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models.Categories;
using Peppa.Interface.WindowsService;
using Peppa.ViewModels.Interface;

namespace Peppa.ViewModels.Categories
{
    public class CategoriesViewModel : BaseViewModel, IInitialization
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

            IsProgressShow = false;
            RaisePropertyChanged(nameof(IsProgressShow));
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
        
        public CategoryViewModel SelectedItem { get; set; }
        
        public bool IsProgressShow { get; set; }
    }
}