using ChatClient.Services;
using ChatClient.ViewModels;
using ChatClient.Views;
using Prism;
using Prism.Ioc;
using Xamarin.Essentials.Implementation;
using Xamarin.Essentials.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ChatClient
{
    public partial class App
    {
        /*
        * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
        * This imposes a limitation in which the App class must have a default constructor.
        * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
        */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync($"{nameof(NavigationPage)}/{nameof(ConnectPage)}");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // register services
            containerRegistry.Register<TaskManager>();
            containerRegistry.Register<AudioPlayer>();
            containerRegistry.RegisterSingleton<ClientChatHub>();
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            // register pages and viewModels
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<ConnectPage, ConnectPageViewModel>();
            containerRegistry.RegisterForNavigation<UserListPage, UserListPageViewModel>();
            containerRegistry.RegisterForNavigation<ChatPage, ChatPageViewModel>();
        }
    }
}
