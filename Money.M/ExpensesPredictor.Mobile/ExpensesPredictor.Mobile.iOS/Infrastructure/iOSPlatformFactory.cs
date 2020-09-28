using System;
using System.IO;
using ExpensesPredictor.Mobile.iOS.Infrastructure;
using ExpensesPredictor.Mobile.Infrastructure.NoSQL.Abstract;
using MarcelloDB;
using MarcelloDB.Platform;
using Xamarin.Forms.Internals;

[assembly: Xamarin.Forms.Dependency(typeof(iOSPlatformFactory))]
namespace ExpensesPredictor.Mobile.iOS.Infrastructure
{
    [Preserve]
    public class iOSPlatformFactory : IPlatformFactory
    {

        public static Lazy<Session> SessionLazy = new Lazy<Session>(CreateSession);
        public IPlatform GetPlatform()
        {
            return new MarcelloDB.netfx.Platform();
        }

        public Session GetSession()
        {
            return SessionLazy.Value;
        }

        public static Session CreateSession()
        {
            var platform = new MarcelloDB.netfx.Platform();
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, "DbFiles");
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);

            return new Session(platform, path);
        }
    }
}