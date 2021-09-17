using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace SunmiDemo.ViewModel
{
    public class MainViewModel
    {
        public ICommand PrintCommand { get; private set; }
        public ICommand LFCommand { get; private set; }

        public MainViewModel()
        {
            PrintCommand = new Command(async () => await PrintItem());
            LFCommand = new Command(async () => await PrinterCommand());
        }

        async Task PrinterCommand()
        {
            Xamarin.Forms.DependencyService.Register<INativePages>();
            DependencyService.Get<INativePages>().StartActivityInAndroid("LF");
        }

        async Task PrintItem()
        {
            // await Application.Current.MainPage.Navigation.PushModalAsync(new cpgPrint());
            Xamarin.Forms.DependencyService.Register<INativePages>();
            DependencyService.Get<INativePages>().StartActivityInAndroid("Hello Sunmi,\nI am printing some text\n");
        }

    }
}
