using Peppa.Context.Entities;
using Peppa.Enums;

namespace Peppa.ViewModels.Categories
{
    public class CategoryViewModel : BaseViewModel
    {
        public CategoryViewModel()
        {
            IsNew = true;
        }

        public CategoryViewModel(Category model)
        {
            IsNew = false;
            Id = model.Id;
            Title = model.Title;
            HexColor = model.HexColor;
            Type = model.Type;
            IsArchived = model.IsArchived;
            IsDeleted = model.IsDeleted;
            IsSynchronized = model.IsSynchronized;
        }
        
        //TODO Add implicit operation
        public Category MakeCategoryEntity()
            => new Category
            {
                Id = Id,
                Title = Title,
                HexColor = HexColor,
                Type = Type,
                IsArchived = IsArchived,
                IsDeleted = IsDeleted,
                IsSynchronized = IsSynchronized
            };
        
        public int Id { get; }
        
        public string Title { get; set; }
        
        public string HexColor { get; set; }

        public CategoryType Type { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public bool IsSynchronized { get; set; }
        
        public DialogResult Action { get; set; }
        
        public bool IsNew { get; set; }
        
        public bool IsArchived { get; set; }
    }
}
