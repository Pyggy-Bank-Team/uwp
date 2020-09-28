using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;

namespace Peppa.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        internal virtual void RaisePropertyChanged(string propertyName)
        {
            App.RunUIAsync(() =>
            {
                if (String.IsNullOrEmpty(propertyName))
                    return;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            });
        }

        internal virtual void RaisePropertiesChanged()
        {
            var properties = GetType().GetRuntimeProperties();

            foreach (var property in properties)
            {
                RaisePropertyChanged(property.Name);
            }
        }
        
        internal CancellationToken GetToken(int minutes = 1)
            => new CancellationTokenSource(TimeSpan.FromMinutes(minutes)).Token;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
