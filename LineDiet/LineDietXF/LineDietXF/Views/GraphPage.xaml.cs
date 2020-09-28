using LineDietXF.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LineDietXF.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GraphPage : ContentPage
    {
        GraphPageViewModel ViewModel { get { return this.BindingContext as GraphPageViewModel; } }

        public GraphPage()
        {
            // NOTE:: The Binding Context is not set yet at this point, so do not access the VM from the constructor
            InitializeComponent();
        }

        void WeightEntryListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            WeightEntryListView.SelectedItem = null; // clear selection (so it doesn't stay highlighted)
        }

        /// <summary>
        /// When the size of the window changes we want to update the UI. This is specifically to show/hide the listing of 
        /// weight entries so that the graph is full-screen when in landscape.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="height"></param>
        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            bool orientationChanged = false;

            // see if the device is landscape, and if so hide the listing and just show the graph
            if (width > height) // landscape
            {
                if (RootGrid.RowDefinitions.Count == 2)
                {
                    RootGrid.RowDefinitions.RemoveAt(1);
                    BottomAreaGrid.IsVisible = false;
                    Grid.SetRow(BottomAreaGrid, 0);
                    Grid.SetRowSpan(LoadingIndicator, 1);

                    orientationChanged = true;
                }
            }
            else // portrait
            {
                if (RootGrid.RowDefinitions.Count == 1)
                {
                    RootGrid.RowDefinitions.Add(new RowDefinition() { Height = GridLength.Star });
                    Grid.SetRow(BottomAreaGrid, 1);
                    BottomAreaGrid.IsVisible = true;
                    Grid.SetRowSpan(LoadingIndicator, 2);

                    orientationChanged = true;
                }
            }

            if (orientationChanged)
            {
                // we have to tell the graph to refresh or else the graph will appear squished
                if (MainGraph.Model != null)
                    MainGraph.Model.InvalidatePlot(false);
            }
        }
    }
}