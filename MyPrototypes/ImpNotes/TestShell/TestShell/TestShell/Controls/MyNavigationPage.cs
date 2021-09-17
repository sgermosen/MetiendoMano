using System;
using System.Collections.Generic;
using System.Text;

namespace TestShell.Controls
{
    using System;
    using Xamarin.Forms;

        public class MyNavigationPage : NavigationPage
    {
        public MyNavigationPage(Page root) : base(root)
        {
            Init();
        }

        public MyNavigationPage()
        {
            Init();
        }

        void Init()
        {

            BarBackgroundColor = Color.FromHex("#03A9F4");
            BarTextColor = Color.White;
        }
    }
}
