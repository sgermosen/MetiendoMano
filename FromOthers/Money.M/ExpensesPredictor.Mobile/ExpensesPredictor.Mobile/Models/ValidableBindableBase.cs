using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ExpensesPredictor.Mobile.Models
{
    public abstract class ValidableBindableBase: BindableBase
    {
        public abstract bool IsValid { get; }

        protected override void SetProperty<T>(ref T value, T newvalue,[CallerMemberName] string propertyName = null)
        {
            value = newvalue;
            OnPropertyChanged(propertyName);

            if (propertyName!="IsValid")
                OnPropertyChanged("IsValid");
        }
    }
}
