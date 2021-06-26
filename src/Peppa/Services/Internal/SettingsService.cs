using Windows.Storage;
using Peppa.Interface.InternalServices;

namespace Peppa.Services.Internal
{
    public class SettingsService : ISettingsService
    {
        private readonly ApplicationDataContainer _settings;
        private static readonly object Lock = new object();

        public SettingsService()
        {
            _settings = ApplicationData.Current.LocalSettings;
        }

        public void AddOrUpdateValue(string key, string value)
        {
            lock (Lock)
            {
                if (_settings.Values.ContainsKey(key))
                {
                    _settings.Values[key] = default(string);
                    _settings.Values[key] = value;
                }
                else
                    _settings.Values.Add(key, value);
            }
        }

        public bool TryGetValue(string key, out string value)
        {
            value = null;

            lock (Lock)
            {
                if (_settings.Values.ContainsKey(key))
                    value = (string)_settings.Values[key];
            }

            return value != null;
        }

        public void RemoveValue(string key)
        {
            lock (Lock)
            {
                if (_settings.Values.ContainsKey(key))
                    _settings.Values.Remove(key);
            }
        }

        public bool HaveValue(string key)
        {
            lock (Lock)
            {
                return _settings.Values.ContainsKey(key);
            }
        }
    }
}