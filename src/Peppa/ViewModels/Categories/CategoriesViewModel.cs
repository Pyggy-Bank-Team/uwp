using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Peppa.Interface.Models;
using Peppa.ViewModels.Categoies;
using Peppa.ViewModels.Interface;
using piggy_bank_uwp.Enums;

namespace Peppa.ViewModels.Categories
{
    public class CategoriesViewModel : BaseViewModel, IBaseViewModel
    {
        private readonly ICategoriesModel _model;

        public CategoriesViewModel(ICategoriesModel model)
        {
            _model = model;
            List = new ObservableCollection<CategoryViewModel>();
        }
        
        public async Task Initialization()
        {
            var categories = await _model.GetCategories(GetCancellationToken());
            if (categories != null)
            {
                List = new ObservableCollection<CategoryViewModel>(categories.Select(c => new CategoryViewModel(c)).OrderBy(c => c.IsArchived));
                RaisePropertiesChanged();
            }
        }

        public void Finalization()
        {
            throw new System.NotImplementedException();
        }

        //TODO Split on two separated methods
        internal async Task UpdateData()
        {
            switch (SelectedItem?.Action)
            {
                case ActionType.Save when SelectedItem?.IsNew == true || SelectedItem?.IsSynchronized == false:
                    await _model.CreateCategory(SelectedItem.MakeCategoryEntity(), GetCancellationToken());
                    break;
                case ActionType.Save when SelectedItem?.IsNew == false:
                    await _model.UpdateCategory(SelectedItem.MakeCategoryEntity(), GetCancellationToken());
                    break;
                case ActionType.Delete:
                    await _model.DeleteCategory(SelectedItem.Id, GetCancellationToken());
                    break;
            }
        }

        public ObservableCollection<CategoryViewModel> List { get; private set; }
        
        public CategoryViewModel SelectedItem { get; set; }
    }
}