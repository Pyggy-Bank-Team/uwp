using Peppa.Extensions;
using Windows.UI.Xaml.Controls;

namespace piggy_bank_uwp.Dialogs
{
    public sealed partial class ColorPickerDialog : ContentDialog
    {
        public ColorPickerDialog()
        {
            this.InitializeComponent();
        }

        private void OnSaveButton(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            HexColor = NewColor.Color.ToColor();
        }

        public string HexColor { get; private set; }
    }
}
