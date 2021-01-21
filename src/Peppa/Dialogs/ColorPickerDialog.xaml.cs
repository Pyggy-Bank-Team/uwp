using Windows.UI.Xaml.Controls;
using Peppa.Extensions;

namespace Peppa.Dialogs
{
    public sealed partial class ColorPickerDialog : ContentDialog
    {
        public ColorPickerDialog()
        {
            this.InitializeComponent();
        }

        private void OnSaveButton(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            SelectedColor = NewColor.Color.ToHexColor();
        }

        public string SelectedColor { get; private set; }
    }
}
