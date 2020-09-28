using Premy.Chatovatko.Client.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Premy.Chatovatko.Client.Helpers
{
    public class LoadingLock : IDisposable
    {
        private readonly Page page;

        public LoadingLock(Page page, String message)
        {
            this.page = page;
            page.Navigation.PushModalAsync(new Loading(message));
        }

        public void Dispose()
        {
            page.Navigation.PopModalAsync();
        }
    }
}
