using System.Threading.Tasks;

namespace Peppa.ViewModels.Interface
{
    public interface IBaseViewModel
    {
        Task Initialization();

        void Finalization();
    }
}
