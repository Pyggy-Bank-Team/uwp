namespace Peppa.Interface.InternalServices
{
    public interface ISettingsService
    {
        void AddOrUpdateValue(string key, string value);
        bool TryGetValue(string key, out string value);
        string GetValue(string key);
        void RemoveValue(string key);
        bool HaveValue(string key);
    }
}