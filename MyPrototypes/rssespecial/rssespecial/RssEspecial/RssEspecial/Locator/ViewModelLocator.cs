using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;
using RssEspecial.ViewModel;

namespace RssEspecial.Locator
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<HomePageViewModel>();
            SimpleIoc.Default.Register<DetailPageViewModel>();
        }
        public HomePageViewModel HomePageViewModel => ServiceLocator.Current.GetInstance<HomePageViewModel>();
        public DetailPageViewModel DetailPageViewModel => ServiceLocator.Current.GetInstance<DetailPageViewModel>();
    }
}
