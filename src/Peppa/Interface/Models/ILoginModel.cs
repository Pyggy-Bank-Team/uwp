using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Peppa.Dto;
using Peppa.Enums;

namespace Peppa.Interface.Models
{
    public interface ILoginModel 
    {
        string UserName { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string Email { get; set; }
        List<Currency> Currencies { get; set; }
        Currency Currency { get; set; }
        Task<SigninResultEnum> Signin(CancellationToken token);
        Task Signup(CancellationToken token);
        Task UpdateCurrencies(CancellationToken token);
    }
}