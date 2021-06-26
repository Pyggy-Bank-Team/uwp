using System.Collections.Generic;

namespace Peppa.Interface.InternalServices
{
    public interface ILocalizationService
    {
        string GetTranslateByKey(string key);
        Dictionary<string, string> Languages { get; }
    }
}