using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Peppa.Enums;
using Peppa.Interface.Models;
using Peppa.Interface.Models.Categories;
using Peppa.ViewModels.Interface;

namespace Peppa.ViewModels.Categories
{
    public class CategoriesViewModel : BaseViewModel, IInitialization
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
                case DialogResult.Save when SelectedItem?.IsNew == true || SelectedItem?.IsSynchronized == false:
                    await _model.CreateCategory(SelectedItem.MakeCategoryEntity(), GetCancellationToken());
                    break;
                case DialogResult.Save when SelectedItem?.IsNew == false:
                    await _model.UpdateCategory(SelectedItem.MakeCategoryEntity(), GetCancellationToken());
                    break;
                case DialogResult.Delete:
                    await _model.DeleteCategory(SelectedItem.Id, GetCancellationToken());
                    break;
            }
        }

        public ObservableCollection<CategoryViewModel> List { get; private set; }
        
        public CategoryViewModel SelectedItem { get; set; }
    }
}