using System.Threading.Tasks;

namespace piggy_bank_uwp.ViewModels.Interface
{
    public interface IBaseViewModel
    {
        Task Initialization();

        void Finalization();
    }
}
