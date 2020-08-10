using System;
using System.Collections.Generic;
using System.Text;

namespace TestShell.Views
{
    using System;
    using Xamarin.Forms;

    public class WebsiteView : BaseView
    {
        public WebsiteView(string site, string title)
        {
            Title = title;
            var webView = new WebView();
            webView.Source = new UrlWebViewSource
            {
                Url = site
            };
            Content = webView;
        }
    }
}
