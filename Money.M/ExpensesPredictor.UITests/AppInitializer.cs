using System;
using System.IO;
using System.Linq;
using Xamarin.UITest;
using Xamarin.UITest.Configuration;
using Xamarin.UITest.Queries;

namespace ExpensesPredictor.UITests
{
    public class AppInitializer
    {
        public static IApp StartApp(Platform platform)
        {
            if (platform == Platform.Android)
            {
                return ConfigureApp
                    .Android
                    .InstalledApp("expenses.predictor")
                    .EnableLocalScreenshots()
                    .StartApp(AppDataMode.Clear);
            }

            return ConfigureApp
                .iOS
                .InstalledApp("ExpensesPredictor")
                .EnableLocalScreenshots()
                .StartApp(AppDataMode.Clear);
        }
    }
}

