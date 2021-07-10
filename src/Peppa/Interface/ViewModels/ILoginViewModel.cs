using Windows.UI.Xaml;
using Peppa.Dto;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Peppa.Interface.ViewModels
{
    public interface ILoginViewModel : INotifyPropertyChanged
    {
        string UserName { get; set; }
        string Password { get; set; }
        string ConfirmPassword { get; set; }
        string Email { get; set; }
        string Error { get; }
        ObservableCollection<Currency> Currencies { get; }
        Currency SelectedCurrency { get; set; }
        bool IsLoginProgressShow { get; set; }
        bool IsLoginPanelShow { get; set; }
        bool IsRegistrationPanelShow { get; set; }
        void OnLoginButtonClick(object sender, RoutedEventArgs e);
        void OnRegistrationButtonClick(object sender, RoutedEventArgs e);
        void OnRegistrationLinkButtonClick(object sender, RoutedEventArgs e);
        void OnLoginLinkClick(object sender, RoutedEventArgs e);
    }
}