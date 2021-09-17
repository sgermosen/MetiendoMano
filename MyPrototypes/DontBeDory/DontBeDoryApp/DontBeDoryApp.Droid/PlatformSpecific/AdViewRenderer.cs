using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Widget;
using Android.Gms.Ads;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(DontBeDoryApp.Controls.AdControlView), typeof(DontBeDoryApp.Droid.PlatformSpecific.AdViewRenderer))]

namespace DontBeDoryApp.Droid.PlatformSpecific
{
    public class AdViewRenderer : ViewRenderer<Controls.AdControlView, AdView>
    {
        string adUnitId = string.Empty;
        //Note you may want to adjust this, see further down.
        AdSize adSize = AdSize.SmartBanner;
        AdView adView;
        AdView CreateNativeAdControl()
        {
            if (adView != null)
                return adView;

            // This is a string in the Resources/values/strings.xml that I added or you can modify it here. This comes from admob and contains a / in it
          //  adUnitId = Forms.Context.Resources.GetString(Resource.String.banner_ad_unit_id);
            adUnitId = "ca-app-pub-8521044456540023/5772463173";
            adView = new AdView(Forms.Context);
            adView.AdSize = adSize;
            adView.AdUnitId = adUnitId;

            var adParams = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);

            adView.LayoutParameters = adParams;

            adView.LoadAd(new AdRequest
                    .Builder()
                .Build());
            return adView;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.AdControlView> e)
        {
            base.OnElementChanged(e);
            if (Control == null)
            {
                CreateNativeAdControl();
                SetNativeControl(adView);
            }
        }
    }

}