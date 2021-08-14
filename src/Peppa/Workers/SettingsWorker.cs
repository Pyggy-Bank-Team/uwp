using Windows.Storage;

namespace Peppa.Workers
{
    public sealed class SettingsWorker
    {
        private ApplicationDataContainer _localSettings;

        private SettingsWorker()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
        }

        public bool HaveValue(string key)
        {
            lock (_localSettings)
            {
                return _localSettings.Values.ContainsKey(key);
            }
        }

        public static SettingsWorker Current = new SettingsWorker();
    }
}