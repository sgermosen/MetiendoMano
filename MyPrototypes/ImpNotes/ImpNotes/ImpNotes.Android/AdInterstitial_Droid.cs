using Android.Gms.Ads;
using Xamarin.Forms;
using ImpNotes.Droid;
using ImpNotes.Interface;
using ImpNotes.Notes.Views;

[assembly: Dependency(typeof(AdInterstitial_Droid))]
namespace ImpNotes.Droid
{
    public class AdInterstitial_Droid : IAdInterstitial
    {
        InterstitialAd interstitialAd;

        public AdInterstitial_Droid()
        {
            interstitialAd = new InterstitialAd(Android.App.Application.Context);
            interstitialAd.RewardedVideoAdOpened += (s, e) => {
                NotesListPage.ShouldShowAdd = false;
            };
            // TODO: change this id to your admob id  
            interstitialAd.AdUnitId = Ads.AdConstant.InterstitialUnitId;
            //  LoadAd();
        }

        void LoadAd()
        {
            var requestbuilder = new AdRequest.Builder();
            interstitialAd.LoadAd(requestbuilder.Build());
        }

        public void ShowAd()
        {
            if (interstitialAd.IsLoaded)
                interstitialAd.Show();

            LoadAd();
        }
    }
}