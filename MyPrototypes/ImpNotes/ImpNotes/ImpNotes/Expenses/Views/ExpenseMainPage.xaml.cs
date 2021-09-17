using System;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ImpNotes.Expenses.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpenseMainPage : ContentPage
    {
        public ExpenseMainPage()
        {
            InitializeComponent();

            if (Device.RuntimePlatform == Device.iOS)
                this.Padding = new Thickness(0, 70, 0, 0);

            BindingContext = new ViewModels.ExpenseMainViewModel();
        }

        async void Incomes_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync((new ConceptsPage()));
        }

        async void Expenses_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync((new ConceptsPage()));
        }

        async void Accounts_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync((new ConceptsPage()));
        }

        async void Concepts_Click(object sender, EventArgs e)
        {
            await Navigation.PushAsync((new ConceptsPage()));
        }
    }
}