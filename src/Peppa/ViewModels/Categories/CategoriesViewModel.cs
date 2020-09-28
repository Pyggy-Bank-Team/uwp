using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Peppa.Interface.Models;
using Peppa.ViewModels.Categoies;
using Peppa.ViewModels.Interface;

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
            var categories = await _model.GetCategories(GetToken());
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

        public ObservableCollection<CategoryViewModel> List { get; private set; }
        
        public CategoryViewModel SelectedItem { get; set; }
    }
}