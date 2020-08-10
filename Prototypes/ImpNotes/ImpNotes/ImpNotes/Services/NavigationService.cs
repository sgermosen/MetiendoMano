using ImpNotes.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace ImpNotes.Services
{
    public sealed class NavigationService : INavigationService
    {
        private readonly IDictionary<string, Type> pages_
            = new Dictionary<string, Type>();

        /// <summary>
        /// The name of the virtual "root" page at the top of the navigation history.
        /// </summary>
        public const string RootPage = "(Root)";

        /// <summary>
        /// A moniker for an "unknown" page when navigation happens without
        /// using the <see cref="NavigationService"/>.
        /// </summary>
        public const string UnknownPage = "(Unknown)";

        private static Frame AppFrame => ((Window.Current.Content as Frame)?.Content as Shell)?.AppFrame;

        public void Configure(string page, Type type)
        {
            lock (pages_)
            {
                if (pages_.ContainsKey(page))
                    throw new ArgumentException("The specified page is already registered.");

                if (pages_.Values.Any(v => v == type))
                    throw new ArgumentException("The specified view has already been registered under another name.");

                pages_.Add(page, type);
            }
        }

        #region INavigationService Implementation

        /// <summary>
        /// Gets the name of the currently displayed page.
        /// </summary>
        public string CurrentPage
        {
            get {
                var frame = AppFrame;
                if (frame.BackStackDepth == 0)
                    return RootPage;

                if (frame.Content == null)
                    return UnknownPage;

                var type = frame.Content.GetType();

                lock (pages_)
                {
                    if (pages_.Values.All(v => v != type))
                        return UnknownPage;

                    var item = pages_.Single(i => i.Value == type);

                    return item.Key;
                }
            }
        }

        /// <summary>
        /// Navigates to the specified page.
        /// </summary>
        /// <param name="page"></param>
        public void NavigateTo(string page)
        {
            NavigateTo(page, null);
        }

        /// <summary>
        /// Navigates to the specified page and
        /// supply additional page-specific parameters.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="parameter"></param>
        public void NavigateTo(string page, object parameter)
        {
            lock (pages_)
            {
                if (!pages_.ContainsKey(page))
                    throw new ArgumentException("Unable to find a page registered with the specified name.");

                System.Diagnostics.Debug.Assert(AppFrame != null);
                AppFrame.Navigate(pages_[page], parameter);
            }
        }

        /// <summary>
        /// Navigates to the previous page in the navigation history.
        /// </summary>
        public void GoBack()
        {
            System.Diagnostics.Debug.Assert(AppFrame != null);
            if (AppFrame.CanGoBack)
                AppFrame.GoBack();
        }

        #endregion
    }
}
