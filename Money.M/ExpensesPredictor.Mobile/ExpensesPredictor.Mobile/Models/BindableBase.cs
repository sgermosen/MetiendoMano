using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace ExpensesPredictor.Mobile.Models
{
    public class BindableBase:INotifyPropertyChanged
    {
        protected virtual void SetProperty<T>(ref T value,T newvalue,[CallerMemberName] string propertyName = null)
        {
            value = newvalue;
            OnPropertyChanged(propertyName);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}