using Peppa.Enums;

namespace Peppa.Dto
{
    public class Category
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string HexColor { get; set; }
        public CategoryType Type { get; set; }
    }
}