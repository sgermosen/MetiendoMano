using Plugin.SharedTransitions;
using Xamarin.Forms;

namespace ArtNews.Views
{
    public partial class CustomNavigationPage : SharedTransitionNavigationPage
    {
        public CustomNavigationPage(Page root) : base(root)
        {
            InitializeComponent();
        }
    }
}