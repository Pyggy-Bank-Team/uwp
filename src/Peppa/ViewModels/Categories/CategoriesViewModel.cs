using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models.Categories;
using Peppa.Interface.ViewModels;
using Peppa.Interface.WindowsService;
using Peppa.ViewModels.Interface;

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

            IsProgressShow = false;
            RaisePropertyChanged(nameof(IsProgressShow));
        }

        public void OnAddCategoryClick(object sender, RoutedEventArgs e)
        {
            
        }

        public void OnCategoryItemClick(object sender, ItemClickEventArgs e)
        {
            
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
    }
}