using Peppa.Interface.ViewModels;
using QRCoder;
using System;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace Peppa.ViewModels.Settings
{
    public class BotDialogViewModel : BaseViewModel, IInitialization
    {
        private readonly string _externalId;
        public BotDialogViewModel(string externalId)
            => _externalId = externalId;

        public async Task Initialization()
        {
            IsProgressShow = true;
            RaisePropertyChanged(nameof(IsProgressShow));

            var encoding = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(_externalId));

#if DEBUG
            var url = $"https://t.me/PiggyBankDevBot?start={encoding}";

#else
            var url = $"https://t.me/PiggyBankProBot?start={encoding}";
#endif
            var qrGenerator = new QRCodeGenerator();
            var qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);

            var qrCodeBmp = new BitmapByteQRCode(qrCodeData);
            //1st color - background, 2nd color - foreground
            var qrCodeImageBmp = qrCodeBmp.GetGraphic(20, "#000000", "#ffffff");
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                using (DataWriter writer = new DataWriter(stream.GetOutputStreamAt(0)))
                {
                    writer.WriteBytes(qrCodeImageBmp);
                    await writer.StoreAsync();
                }

                var image = new BitmapImage();
                await image.SetSourceAsync(stream);

                Qr = image;
            }

            IsProgressShow = false;
            RaisePropertyChanged(nameof(IsProgressShow));
            RaisePropertyChanged(nameof(Qr));
        }

        public bool IsProgressShow { get; private set; }
        public BitmapImage Qr { get; private set; }
    }
}
