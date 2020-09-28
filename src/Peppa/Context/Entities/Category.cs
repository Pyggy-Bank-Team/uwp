using Peppa.Enums;

namespace Peppa.Context.Entities
{
    public class Category
    {
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string HexColor { get; set; }

        public CategoryType Type { get; set; }
        
        public bool IsArchived { get; set; }
        
        public bool IsDeleted { get; set; }
        
        public bool IsSynchronized { get; set; }
    }
}