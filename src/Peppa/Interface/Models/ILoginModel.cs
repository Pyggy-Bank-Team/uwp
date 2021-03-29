using System.Threading;
using System.Threading.Tasks;
using Peppa.Dto;

namespace Peppa.Interface.Models
{
    public interface ILoginModel
    {
        string UserName { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string Error { get; set; }
        Currency[] Currencies { get; set; }
        Currency SelectedCurrency { get; set; }
        Task Signin();
        Task Signup();
        Task GetCurrencies(CancellationToken token);
    }
}