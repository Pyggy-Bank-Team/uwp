using System.Collections.Generic;
using System.ComponentModel;

namespace Peppa.Interface.ViewModels
{
    public interface ISettingsViewModel : IInitialization, INotifyPropertyChanged
    {
        void OnLogoutClick(object sender, Windows.UI.Xaml.RoutedEventArgs e);
        string Email { get; set; }
        string Currency { get; set; }
        string UserName { get; set; }
        List<string> Languages { get; }
        string Language { get; set; }
        string Version { get; set; }
        bool IsProgressShow { get; set; }
        bool IsDarkModeEnabled { get; set; }
        bool IsChangedSettings { get; set; }
    }
}