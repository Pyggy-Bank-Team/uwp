using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.Services.Store;
using System;
using Peppa.Utilities;
using Peppa.ViewModels.Interface;

namespace Peppa.ViewModels.Donate
{
    public class DonateViewModel : IBaseViewModel
    {
        private StoreContext _storeContext;
        private bool _isLoaded;
        public DonateViewModel()
        {
            Items = new List<DonateItemViewModel>();
            _storeContext = StoreContext.GetDefault();
        }

        public async Task Initialization()
        {
            if (_isLoaded)
                return;

            string[] productKinds = { "UnmanagedConsumable" };
            var queryResult = await _storeContext.GetAssociatedStoreProductsAsync(productKinds);

            foreach (KeyValuePair<string, StoreProduct> item in queryResult.Products)
            {
                DonateItemViewModel donateItem = new DonateItemViewModel
                {
                    Title = DonateUtility.GetValidurchaseName(item.Value.InAppOfferToken),
                    StoreId = item.Key,
                    Price = item.Value.Price.FormattedPrice
                };
                Items.Add(donateItem);
            }

            _isLoaded = true;
        }

        public void Finalization()
        {
        }

        public async Task<string> BuyItem(DonateItemViewModel item)
        {
            try
            {
                var result = await _storeContext.RequestPurchaseAsync(item?.StoreId);
                switch (result.Status)
                {
                    case StorePurchaseStatus.Succeeded:
                        return Localization.GetTranslateByKey(Localization.PurchaseStatusOk);
                    case StorePurchaseStatus.NotPurchased:
                        return String.Empty;
                    default:
                        return Localization.GetTranslateByKey(Localization.PurchaseStatusBad);
                }
            }
            catch
            {
                return Localization.GetTranslateByKey(Localization.PurchaseStatusBad);
            }
        }

        public List<DonateItemViewModel> Items { get; }
    }
}
