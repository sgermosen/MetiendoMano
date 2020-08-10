using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ImpNotes.Services;
using ImpNotes.Views;

namespace ImpNotes
{
    public partial class App : Application
    {
      //  public static NavigationPage Navigator { get; internal set; }
        public static NavigationPage Navigator { get; internal set; }

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
          
           MainPage = new AppShell();
         
            //  MainPage = new NavigationPage(new AppShell()) ;

            //var sentences = new List<string>();

            /////  MainPage = new TextToImagePage(new Model.NotesModel() { Notes = text, TextColor = "Blue" });
           //MainPage = new NavigationPage(new MainPage());
        }



        public IEnumerable<string> SplitText(string text, int length)
        {
            for (int i = 0; i < text.Length; i += length)
            {
                yield return text.Substring(i, Math.Min(length, text.Length - i));
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        
    }

    public static class Ext
    {
        public static string[] GetWords(
            this string input,
            int count = -1,
            string[] wordDelimiter = null,
            StringSplitOptions options = StringSplitOptions.None)
        {
            if (string.IsNullOrEmpty(input)) return new string[] { };

            if (count < 0)
                return input.Split(wordDelimiter, options);

            string[] words = input.Split(wordDelimiter, count + 1, options);
            if (words.Length <= count)
                return words;   // not so many words found

            // remove last "word" since that contains the rest of the string
            Array.Resize(ref words, words.Length - 1);

            return words;
        }

        public static IEnumerable<List<T>> SplitList<T>(this List<T> locations, int nSize = 30)
        {
            for (int i = 0; i < locations.Count; i += nSize)
            {
                yield return locations.GetRange(i, Math.Min(nSize, locations.Count - i));
            }
        }
    }
}
