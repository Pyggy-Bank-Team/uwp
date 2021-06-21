using System.Collections.Generic;
using Windows.ApplicationModel.Resources;
using Peppa.Interface.InternalServices;

namespace Peppa.Services.Internal
{
    public class LocalizationService : ILocalizationService
    {
        public LocalizationService()
            => Languages = new Dictionary<string, string>
            {
                {"en-US", "English"},
                {"ru-RU", "Русский"}
            };
        
        public string GetTranslateByKey(string key)
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
        
        public Dictionary<string, string> Languages { get; private set; }
    }
}