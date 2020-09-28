using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LineDietXF.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoadingIndicator : Grid
    {
        public LoadingIndicator()
        {
            InitializeComponent();
        }
    }
}