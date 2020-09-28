using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpensesPredictor.Mobile.Infrastructure.Commands
{
    public class RelayCommand<T>:ICommand where T:class 
    {
        private readonly Action<T> _execute;
        private readonly Func<T, bool> _canExecute;

        public RelayCommand(Action<T> execute,Func<T,bool> canExecute=null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter as T);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this,new EventArgs());
        }

        public void Execute(object parameter)
        {
            if(!CanExecute(parameter as T))return;
            _execute(parameter as T);
        }

        public event EventHandler CanExecuteChanged;
    }
}
