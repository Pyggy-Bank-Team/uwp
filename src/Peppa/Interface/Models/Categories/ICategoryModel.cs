using System.Threading;
using System.Threading.Tasks;
using Peppa.Enums;

namespace Peppa.Interface.Models.Categories
{
    public interface ICategoryModel
    {
        Task Save(CancellationToken token);
        Task Update(CancellationToken token);
        Task Delete(CancellationToken token);
        string HexColor { get; set; }
        string Title { get; set; }
        CategoryType Type { get; set; }
        bool IsArchived { get; set; }
        bool IsNew { get; }
    }
}