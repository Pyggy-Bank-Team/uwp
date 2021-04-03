using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Dto;

namespace Peppa.Interface.Models
{
    public interface ILoginModel : INotifyPropertyChanged
    {
        string UserName { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string Email { get; set; }
        string Error { get; set; }
        List<Currency> Currencies { get; set; }
        Currency SelectedCurrency { get; set; }
        Task Signin(CancellationToken token);
        Task Signup(CancellationToken token);
        Task UpdateCurrencies(CancellationToken token);
    }
}