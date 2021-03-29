using Microsoft.Toolkit.Uwp.Notifications;

namespace Peppa.Services.Windows
{
    public static class ToastService
    {
        public static ToastContent GenerateToastContent()
        {
            return new ToastContent
            {
                Scenario = ToastScenario.Reminder,
                Visual = new ToastVisual
                {
                    BindingGeneric = new ToastBindingGeneric
                    {
                        Children =
                        {
                            new AdaptiveText
                            {
                                Text = Localization.GetTranslateByKey(Localization.HeaderReminderNotifi)
                            },
                            new AdaptiveText
                            {
                                Text =  Localization.GetTranslateByKey(Localization.DescriptionRemiderNotifi)
                            }
                        }
                    }
                }
            };
        } 
    }
}
