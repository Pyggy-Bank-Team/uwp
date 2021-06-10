using Peppa.Enums;
using Peppa.Interface.InternalServices;
using Peppa.Interface.Models.Categories;

namespace Peppa.ViewModels.Categories
{
    public class CategoryListViewItemViewModel
    {
        private readonly ILocalizationService _localizationService;

        public CategoryListViewItemViewModel(ICategoryModel model, ILocalizationService localizationService)
        {
            Model = model;
            _localizationService = localizationService;
            Title = model.Title;
            HexColor = model.HexColor;
            TypeTitle = GetTypeTitle(Model.Type);
            ArchiveTitle = Model.IsArchived ? _localizationService.GetTranslateByKey(Localization.InArchive) : "";
        }

        private string GetTypeTitle(CategoryType type)
        {
            switch (type)
            {
                case CategoryType.Income:
                    return _localizationService.GetTranslateByKey(Localization.Income);
                case CategoryType.Expense:
                    return _localizationService.GetTranslateByKey(Localization.Expense);
                default:
                    return "Null";
            }
        }
        
        public string Title { get; set; }
        public string HexColor { get; set; }
        public string TypeTitle { get; set; }
        public string ArchiveTitle { get; set; }
        public ICategoryModel Model { get; }
    }
}
