using System.Threading;
using System.Threading.Tasks;

namespace Peppa.Interface.Models.Categories
{
    public interface ICategoryModel
    {
        Task Save(CancellationToken token);
        Task Update(CancellationToken token);
        Task Delete(CancellationToken token);
    }
}