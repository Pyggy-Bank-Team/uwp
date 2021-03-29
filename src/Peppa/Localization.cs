using Windows.ApplicationModel.Resources;

namespace Peppa
{
    public static class Localization
    {
        public const string Accounts = "Accounts";
        public const string Costs = "Costs";
        public const string Categories = "Categories";
        public const string Diagrama = "Diagrama";
        public const string Synchronization = "Synchronization";
        public const string Donate = "Donate";
        public const string Settings = "Settings";
        public const string HeaderReminderNotifi = "HeaderReminderNotifi";
        public const string DescriptionRemiderNotifi = "DescriptionRemiderNotifi";
        public const string WarringCategoriesContent = "WarringCategoriesContent";
        public const string WarringCostContent = "WarringCostContent";
        public const string WarringCategoryContent = "WarringCategoryContent";
        public const string WarringBalanceCostContent = "WarringBalanceCostContent";
        public const string Ok = "Ok";
        public const string PurchaseStatusOk = "PurchaseStatusOk";
        public const string PurchaseStatusBad = "PurchaseStatusBad";
        public const string NotValidUserNameOrPassword = "NotValidUserNameOrPassword";
        public const string PasswordAndConfirmPasswordNotEquals = "PasswordAndConfirmPasswordNotEquals";
        public const string CurrencyNotSelected = "CurrencyNotSelected";

        public static string GetTranslateByKey(string key)
        {
            try
            {
                if (string.IsNullOrEmpty(key))
                    return string.Empty;

                var translate = ResourceLoader.GetForViewIndependentUse().GetString(key);

                return string.IsNullOrEmpty(translate) ? key : translate;
            }
            catch
            {
                return key;
            }
        }
    }
}
