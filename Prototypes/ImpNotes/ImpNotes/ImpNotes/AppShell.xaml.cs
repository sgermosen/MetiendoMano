using ImpNotes.Expenses.Views;
using ImpNotes.Notes.Views;
using Xamarin.Forms;

namespace ImpNotes
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("concepts", typeof(ConceptsPage));
            // (App.Current.MainPage as Xamarin.Forms.Shell).GoToAsync($"app:///myapp/concepts?id={productId},true");
        }
    }
}
