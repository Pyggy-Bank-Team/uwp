using System.Threading;
using System.Threading.Tasks;

namespace Peppa.Interface.Models.Settings
{
    public interface ISettingsModel
    {
        Task UpdateUser(CancellationToken token);
        Task ChangeEmail(CancellationToken token);
        Task ChangeCurrency(CancellationToken token);
        void ChangeLanguage();
        void LogOut();
        bool DarkModeIsEnabled { get; set; }
        string Language { get; set; }
        string Email { get; set; }
        string Currency { get; set; }
        string Login { get; }
    }
}