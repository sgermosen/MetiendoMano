using ExpensesPredictor.Mobile.ViewModels.Abstract;
using Xamarin.Forms;

namespace ExpensesPredictor.Mobile.Views.Abstract
{
    public class ViewBase: ContentPage
    {
        public ViewModelBase ViewModel => BindingContext as ViewModelBase;

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel?.OnPushed();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            ViewModel.OnPopped();
        }
    }
}
