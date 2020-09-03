using Microsoft.Toolkit.Uwp.Notifications;

namespace Peppa.Services
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
                                Text = Localize.GetTranslateByKey(Localize.HeaderReminderNotifi)
                            },
                            new AdaptiveText
                            {
                                Text =  Localize.GetTranslateByKey(Localize.DescriptionRemiderNotifi)
                            }
                        }
                    }
                }
            };
        } 
    }
}
