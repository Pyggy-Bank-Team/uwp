using Windows.UI.Xaml.Controls;

namespace Peppa.Services.Windows
{
    public static class DialogService
    {
        public static ContentDialog GetInformationDialog(string content)
        {
            return new ContentDialog
            {
                Content = content,
                PrimaryButtonText = Localization.GetTranslateByKey(Localization.Ok)
            };
        }

        public static ContentDialog GetPurchaseStatusDialog(string status)
        {
            return new ContentDialog
            {
                Content = status,
                PrimaryButtonText = Localization.GetTranslateByKey(Localization.Ok)
            };
        }
    }
}
