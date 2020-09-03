using System.Threading.Tasks;

namespace Peppa.Interface.Models
{
    public interface IAccountsModel
    {
        Task GetAccounts();
        Task CreatedAccount();
        Task DeleteAccount();
        Task UpdateAccount();
    }
}