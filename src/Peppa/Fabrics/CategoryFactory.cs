using System.Collections.Generic;
using Peppa.Models;
using Peppa.Models.Categories;
using Peppa.Utilities;

namespace Peppa.Fabrics
{
    public static class CategoryFactory
    {
        public static IEnumerable<CategoryModel> GetCategories()
        {
            CategoryModel category = new CategoryModel { Title = "Foods", HexColor = "#FF107C10", Id = SystemUtility.GetGuid() };
            yield return category;

            category = new CategoryModel { Title = "Extra costs", HexColor = "#FFE81123", Id = SystemUtility.GetGuid() };
            yield return category;

            category = new CategoryModel { Title = "Home", HexColor = "#FFFFB900", Id = SystemUtility.GetGuid() };
            yield return category;

            category = new CategoryModel { Title = "Work", HexColor = "#FF0078D7", Id = SystemUtility.GetGuid() };
            yield return category;
        }
    }
}
