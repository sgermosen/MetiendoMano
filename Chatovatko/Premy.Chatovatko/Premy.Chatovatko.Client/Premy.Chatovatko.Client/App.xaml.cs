using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Premy.Chatovatko.Client.Views;
using System.Reflection;
using Premy.Chatovatko.Libs.Logging;
using Premy.Chatovatko.Client.Libs.UserData;
using Premy.Chatovatko.Client.Libs.Database;
using Premy.Chatovatko.Client.Libs.ClientCommunication;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels;
using Premy.Chatovatko.Libs.Cryptography;
using Premy.Chatovatko.Client.Libs.Sync;
using Premy.Chatovatko.Client.Helpers;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Premy.Chatovatko.Client
{
    public partial class App : Application, ILoggable
    {
        public Logger logger;
        public IClientDatabaseConfig config;
        public DBInitializator initializator;
        public SettingsLoader settingsLoader;
        public SettingsCapsula settings = null;
        public Connection connection = null;
        public Synchronizer synchronizer = null;

        private Page mainPageBackup = null;
        public Action<byte[], String> saveFile;

        public App(Action<byte[], String> saveFile)
        {
            InitializeComponent();
            this.saveFile = saveFile;

            logger = new Logger (new DebugLoggerOutput());
            config = new ClientDatabaseConfig();
            initializator = new DBInitializator(config, logger);
            initializator.DBEnsureCreated();

            settingsLoader = new SettingsLoader(config, logger);

            if (settingsLoader.Exists())
            {
                Log("Settings exists and will be loaded.");
                settings = settingsLoader.GetSettingsCapsula();
                connection = new Connection(logger, settings);
                Init();
            }
            else
            {
                Log("Settings doesn't exist.");
                MainPage = new CertificateSelection(this, logger);
            }

        }

        protected Connection GetConnection()
        {
            return connection;
        }

        private void Init()
        {
            synchronizer = new Synchronizer(GetConnection, Reconnect, logger, settings);
            synchronizer.Run();
            MainPage = (new MainPage(this, settings));
            mainPageBackup = MainPage;
        }

        public void ShowLoading(string message)
        {
            mainPageBackup = MainPage;
            MainPage = new Loading(message);
        }

        public void HideLoading()
        {
            MainPage = mainPageBackup;
        }

        private void Reconnect()
        {
            try
            {
                connection.Disconnect();
            }
            catch
            {

            }
            try
            { 
                connection = new Connection(logger, settings);
                connection.Connect();
            }
            catch(Exception ex)
            {
                logger.LogException(this, ex);
            }
        }

        
        public void AfterCertificateSelected(X509Certificate2 cert)
        {
            MainPage = new ServerSelection(this, cert);
        }

        public async void AfterServerSelected(X509Certificate2 clientCert, String address, String password, String userName)
        {
            MainPage = new Loading("Server informations are downloading...");
            try
            {
                InfoConnection infoConnection = new InfoConnection(address, logger);
                ServerInfo info = null;

                await Task.Run(() =>
                {
                    info = infoConnection.DownloadInfo();
                });

                MainPage = new ServerVerification(this, info, clientCert, address, password, userName);
            }
            catch (Exception ex)
            {
                MainPage = new ServerSelection(this, clientCert, address, password, userName, ex.Message);
            }
        }
        
        public async void AfterServerConfirmed(X509Certificate2 clientCert, ServerInfo info, String address, String password, String userName)
        {
            MainPage = new Loading("Client is being registred...");
            try
            { 
                await Task.Run(() =>
                {
                    ConnectionUtils.Register(out connection, out settings, logger, Log, address, clientCert, config, userName, settingsLoader, info);
                });
                Init();
            }
            catch(Exception ex)
            {
                logger.LogException(this, ex);
                MainPage = new ServerSelection(this, clientCert, address, password, userName, ex.Message);
            }
        }



        public void AddUpdatable(IUpdatable updatable)
        {
            synchronizer.Updatable.Add(new WeakReference<IUpdatable>(updatable));
        }
        
        protected override void OnStart()
        {
            if(synchronizer != null)
            {
                synchronizer.SetDelay(1000);
            }
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {
            if (synchronizer != null)
            {
                synchronizer.SetDelay(1000);
            }
        }

        public string GetLogSource()
        {
            return "Log";
        }

        private void Log(String message)
        {
            logger.Log(this, message);
        }
    }
}
