using System.Collections.Generic;

namespace Peppa.Helpers
{
    public static class CurrencyHelper
    {
        private static readonly Dictionary<string, string> AvailableCurrencies = new Dictionary<string, string>
        {
            {"RUB", "₽"},
            {"BYN", "Br"},
            {"UAH", "₴"},
            {"KZT", "₸"},
            {"USD", "$"},
            {"EUR", "€"}
        };

        public static string GetSymbol(string currency)
        {
            if (string.IsNullOrWhiteSpace(currency))
                return null;

            if (AvailableCurrencies.ContainsKey(currency))
                return AvailableCurrencies[currency];

            return currency;
        }
    }
}