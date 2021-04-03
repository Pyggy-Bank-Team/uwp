using Windows.UI.Notifications;
using Microsoft.Toolkit.Uwp.Notifications;
using Peppa.Interface.WindowsService;

namespace Peppa.Services.Windows
{
    public class ToastService : IToastService
    {
        private readonly ToastNotifier _notifier;

        public ToastService()
        {
            _notifier = ToastNotificationManager.CreateToastNotifier();
        }

        public void ShowNotification(string header, string description)
        {
            var content = new ToastContent
            {
                Scenario = ToastScenario.Default,
                Visual = new ToastVisual
                {
                    BindingGeneric = new ToastBindingGeneric
                    {
                        Children =
                        {
                            new AdaptiveText {Text = header},
                            new AdaptiveText {Text = description}
                        }
                    }
                }
            };
            
            _notifier.Show(new ToastNotification(content.GetXml()));
        }
    }
}