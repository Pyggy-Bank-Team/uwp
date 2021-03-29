using Windows.ApplicationModel.Resources;
using Peppa.Interface.InternalServices;

namespace Peppa.Services.Internal
{
    public class LocalizationService : ILocalizationService
    {
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
    }
}