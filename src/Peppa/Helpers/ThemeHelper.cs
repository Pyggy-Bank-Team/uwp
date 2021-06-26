using System;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Peppa.Helpers
{
    public static class ThemeHelper
    {
        public static ApplicationTheme GetRequestedTheme()
        {
            var settings = ApplicationData.Current.LocalSettings;

            if (settings.Values.ContainsKey(Constants.RequestedTheme))
            {
                var savedTheme = settings.Values[Constants.RequestedTheme];
                return Enum.Parse<ApplicationTheme>((string)savedTheme);
            }

            return ApplicationTheme.Dark;
        }
    }
}
