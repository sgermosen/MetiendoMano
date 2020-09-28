using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookUICloneXF.Model;
using FFImageLoading.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FacebookUICloneXF
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : ContentPage
    {
        private List<PostItem> ListPosts = new List<PostItem>();
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(Navigation);
            Random random = new Random();
            ListOfPost.ItemsSource = ListPosts = new List<PostItem>()
            {
                new PostItem()
                {
                    PersonName = "John Doe",
                    Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.",
                    PersonProfilePicture = "person1",
                    Date = random.Next(2,15)+" hrs",

                    PostLikes = random.Next(4,50)
                },
                new PostItem()
                {
                    PersonName = "Fig Nelson",
                    Description = "Morbi hendrerit purus sit amet ante fermentum dictum.",
                    PersonProfilePicture = "person2",
                    Date = random.Next(2,15)+" hrs",
                    PostLikes = random.Next(4,50)
                },
                new PostItem()
                {
                    PersonName = "Jackson Pot",
                    Description = "Suspendisse sodales leo at mi suscipit, ac pellentesque est maximus.",
                    PersonProfilePicture = "person3",
                    Date = random.Next(2,15)+" hrs",
                    PostLikes = random.Next(4,50)
                },
                new PostItem()
                {
                    PersonName = "Eric Widget",
                    Description = "Praesent convallis mi vitae nisi tempor, nec tincidunt urna vehicula.",
                    PersonProfilePicture = "person4",
                    Date =random.Next(2,15)+" hrs",
                    PostLikes = random.Next(4,50)
                },
                new PostItem()
                {
                    PersonName = "Fergus Douchebag",
                    Description = "Aenean vel sem sed massa scelerisque molestie sed non elit.",
                    PersonProfilePicture = "person5",
                    Date = random.Next(2,15)+" hrs",
                    PostLikes = random.Next(4,50)
                }
            };
            ListOfPost.HeightRequest = ListOfPost.RowHeight * ListPosts.Count;

        }

        private double oldY = 0;
        private void MyScrollView_Scrolled(object sender, ScrolledEventArgs e)
        {
            if (e.ScrollY > oldY)
            {
                HideBars();
                Debug.WriteLine("DOWNNNNNNNN");

            }
            else if (e.ScrollY < oldY)
            {
                ShowBars();
                Debug.WriteLine("UPPPP");

            }

            oldY = e.ScrollY;

            //Debug.WriteLine("------X="+e.ScrollX+"-Y="+e.ScrollY);

        }

        private void ShowBars()
        {
            FullTopBar.TranslateTo(0, 0, 250u);

        }

        private void HideBars()
        {
            FullTopBar.TranslateTo(0, -40, 250u);

        }

        private void ListOfPost_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            ((ListView)sender).SelectedItem = null;
        }

        private async void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            StackLayout stackLayout = ((StackLayout)sender);
            StackLayout x = ((StackLayout)(stackLayout.Parent.Parent.Parent));  ;
            Label likecount = (Label)((StackLayout)x.Children[2]).Children[1];
            Label likecountreal = (Label)((StackLayout)x.Children[2]).Children[2];

            Label test = ((Label)stackLayout.Children[0]);
            CachedImage cachedImage = ((CachedImage)stackLayout.Children[1]);
            Label label = ((Label)stackLayout.Children[2]);

            if (test.Text.Equals("false"))
            {
                label.TextColor = Color.FromHex("3578e5");
                likecount.Text = "You and " + likecountreal.Text + " others";
                cachedImage.Source = ImageSource.FromFile("bluelikeicon");
                await label.ScaleTo(1.5, 150, Easing.CubicOut);
                await cachedImage.ScaleTo(1.5, 150, Easing.CubicOut);

                test.Text = "true";
                label.ScaleTo(1, 150, Easing.CubicIn);
                cachedImage.ScaleTo(1, 150, Easing.CubicIn);

            }
            else
            {
                await label.ScaleTo(1.5, 50, Easing.CubicOut);
                await cachedImage.ScaleTo(1.5, 50, Easing.CubicOut);
                likecount.Text = likecountreal.Text;

                label.TextColor = Color.Black;
                cachedImage.Source = ImageSource.FromFile("likeicon");
                test.Text = "false";
                label.ScaleTo(1, 150, Easing.CubicIn);
                cachedImage.ScaleTo(1, 150, Easing.CubicIn);
            }

        }

        private void CommentSection_OnTapped(object sender, EventArgs e)
        {

            if (CommentSection.TranslationY > 500)
            {

                StackLayout stackLayout = ((StackLayout)sender);
                StackLayout x = ((StackLayout)(stackLayout.Parent.Parent.Parent)); ;
                Label likecount = (Label)((StackLayout)x.Children[2]).Children[1];
                Label likecountreal = (Label)((StackLayout)x.Children[2]).Children[2];
                Label likecountcommentsection = (Label)((StackLayout)CommentSection.Children[0]).Children[1];
                likecountcommentsection.Text = likecount.Text;
                CommentSection.TranslateTo(0, 0, 250);

            }
            else
            {

               
                CommentSection.TranslateTo(0, 1000, 250);

            }

        }


        private async void DoAnimaton_OnTapped(object sender, EventArgs e)
        {
            //this method just for some animation
            Grid grid = ((Grid) sender);
            await grid.ScaleTo(1.4, 100, Easing.CubicOut);
            grid.ScaleTo(1, 50, Easing.CubicIn);
        }
    }

    public class MainPageViewModel : BindableObject
    {
        private readonly INavigation navigation;
        private string _menuIconone;
        private string _menuIconfive;
        private string _menuIcontwo;
        private string _menuIconthree;
        private string _menuIconfour;
        public Command TopBarMenuCommand { get; set; }

        public string MenuIconone
        {
            get { return _menuIconone; }
            set
            {
                _menuIconone = value;
                OnPropertyChanged();
            }
        }


        public string MenuIcontwo
        {
            get { return _menuIcontwo; }
            set
            {
                _menuIcontwo = value;
                OnPropertyChanged();
            }
        }


        public string MenuIconthree
        {
            get { return _menuIconthree; }
            set
            {
                _menuIconthree = value;
                OnPropertyChanged();
            }
        }


        public string MenuIconfour
        {
            get { return _menuIconfour; }
            set
            {
                _menuIconfour = value;
                OnPropertyChanged();
            }
        }


        public string MenuIconfive
        {
            get { return _menuIconfive; }
            set
            {
                _menuIconfive = value;
                OnPropertyChanged();
            }
        }

        public MainPageViewModel(INavigation navigation)
        {
            this.navigation = navigation;
            InitMenuIcons();

          
            TopBarMenuCommand=new Command(TopBarMenu);
        }

        private void InitMenuIcons()
        {
            MenuIconone = "bluehomeicon";
            MenuIcontwo = "groupicon";
            MenuIconthree = "friendicon";
            MenuIconfour = "notificationicon";
            MenuIconfive = "menuicon";
            menuIconsList=new List<string>(){MenuIconone,MenuIcontwo,MenuIconthree,MenuIconfour,MenuIconfive};
        }

        private List<string> menuIconsList;
        private void TopBarMenu(object obj)
        {
            int choix = int.Parse(obj.ToString());
            if (!menuIconsList[choix].Contains("blue"))
            {
                ResetAllIconsValues();
                menuIconsList[choix] = "blue" + menuIconsList[choix];

            }
      

            RefreshAllIconsValues();

        }

        private void RefreshAllIconsValues()
        {
            MenuIconone = menuIconsList[0];
            MenuIcontwo = menuIconsList[1];
            MenuIconthree = menuIconsList[2];
            MenuIconfour = menuIconsList[3];
            MenuIconfive = menuIconsList[4];
        }
        private void ResetAllIconsValues()
        {
            menuIconsList[0]= MenuIconone = "homeicon";
            menuIconsList[1]= MenuIcontwo = "groupicon";
            menuIconsList[2]=  MenuIconthree = "friendicon";
            menuIconsList[3]=MenuIconfour = "notificationicon";
            menuIconsList[4]= MenuIconfive = "menuicon";
           
        }
    }
}
