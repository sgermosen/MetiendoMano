using System;
using System.IO;
using ExpensesPredictor.Mobile.Droid.Infrastructure;
using ExpensesPredictor.Mobile.Infrastructure.NoSQL.Abstract;
using MarcelloDB;
using MarcelloDB.Platform;
using Xamarin.Forms.Internals;

[assembly: Xamarin.Forms.Dependency(typeof(AndroidPlatformFactory))]
namespace ExpensesPredictor.Mobile.Droid.Infrastructure
{
    [Preserve]
    public class AndroidPlatformFactory : Java.Lang.Object, IPlatformFactory
    {
        public static Lazy<Session> SessionLazy = new Lazy<Session>(CreateSession);
        public static Lazy<IPlatform> PlatformLazy = new Lazy<IPlatform>(CreatePlatform);
        public static IPlatform CreatePlatform()
        {
            return new MarcelloDB.netfx.Platform();
        }

        public IPlatform GetPlatform()
        {
            return PlatformLazy.Value;
        }

        public Session GetSession()
        {
            return SessionLazy.Value;
            //return CreateSession();
        }

        public static Session CreateSession()
        {
            var platform = new MarcelloDB.netfx.Platform();
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder

            var path = Path.Combine(documentsPath, "dbfiles");

            var directory = new DirectoryInfo(path);
            if (!directory.Exists) directory.Create();

            return new Session(platform, path);
        }
    }
}