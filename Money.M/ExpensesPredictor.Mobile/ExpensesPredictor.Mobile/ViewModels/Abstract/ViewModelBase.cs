using ExpensesPredictor.Mobile.Models;

namespace ExpensesPredictor.Mobile.ViewModels.Abstract
{
    public abstract class ViewModelBase: BindableBase
    {
        private bool _isLoading;

        public bool IsLoading
        {
            get { return _isLoading; }
            set { SetProperty(ref _isLoading,value); }
        }

        public virtual void OnPushed()
        {
        }

        public virtual void OnPopped()
        {
        }
    }
}
