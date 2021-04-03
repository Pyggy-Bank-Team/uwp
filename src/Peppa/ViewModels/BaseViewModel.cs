using System;
using System.ComponentModel;
using System.Reflection;
using System.Threading;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Peppa.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected BaseViewModel()
        {
            Frame = Window.Current.Content as Frame;
        }
        
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
        
        internal CancellationToken GetCancellationToken(int minutes = 1)
            => new CancellationTokenSource(TimeSpan.FromMinutes(minutes)).Token;

        public Frame Frame { get; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
