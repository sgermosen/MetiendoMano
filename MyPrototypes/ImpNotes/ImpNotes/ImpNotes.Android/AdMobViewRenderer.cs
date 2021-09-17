using Android.Content;
using Android.Gms.Ads;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ImpNotes.Ads;
using ImpNotes.Droid;

[assembly: ExportRenderer(typeof(AdMobView), typeof(AdMobViewRenderer))]
namespace ImpNotes.Droid
{
    public class AdMobViewRenderer : ViewRenderer<AdMobView, AdView>
    {
        public AdMobViewRenderer(Context context) : base(context) { }


        //protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
        //{
        //    base.OnElementChanged(e);
        //    if (e.NewElement != null && Control == null)
        //        SetNativeControl(CreateAdView());
        //}


        private int GetSmartBannerDpHeight()
        {
            var dpHeight = Resources.DisplayMetrics.HeightPixels / Resources.DisplayMetrics.Density;

            if (dpHeight <= 400)
            {
                return 40;
            }
            if (dpHeight <= 720)
            {
                return 62;
            }
            return 102;
        }


        protected override void OnElementChanged(ElementChangedEventArgs<AdMobView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
            {
                var adView = new AdView(Context)
                {
                    AdSize = AdSize.SmartBanner,
                    AdUnitId = Element.AdUnitId
                };

                var requestbuilder = new AdRequest.Builder();

                adView.LoadAd(requestbuilder.Build());
                e.NewElement.HeightRequest = GetSmartBannerDpHeight();

                SetNativeControl(adView);
            }
        }

        //private AdView CreateAdView()
        //{
        //    var adView = new AdView(Context)
        //    {
        //        AdSize = AdSize.SmartBanner,
        //        AdUnitId = Element.AdUnitId
        //    };

        //    adView.LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.MatchParent, LayoutParams.MatchParent);

        //    adView.LoadAd(new AdRequest.Builder().Build());

        //    return adView;
        //}
    }
}