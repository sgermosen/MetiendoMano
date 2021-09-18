package com.ldt.cinematicket.ui.main.book;

import android.content.Context;
import android.content.res.Configuration;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.WindowManager;
import android.webkit.WebChromeClient;
import android.webkit.WebSettings;
import android.webkit.WebView;
import android.webkit.WebViewClient;

import com.ldt.cinematicket.R;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.PresentStyle;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;

import java.lang.reflect.Method;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import butterknife.BindView;
import butterknife.ButterKnife;

public class WebViewFragment extends SupportFragment {
    private static final String TAG ="WebViewFragment";

    @BindView(R.id.web)
    WebView mWebView;
    private String mURL;

    public static WebViewFragment newInstance(String url) {
     WebViewFragment webViewFragment = new WebViewFragment();
        webViewFragment.mURL = url;
        return webViewFragment;

    }

    @Nullable
    @Override
    protected View onCreateView(LayoutInflater inflater, ViewGroup container) {
        return inflater.inflate(R.layout.web_view,container,false);
    }

    @Override
    public void onConfigurationChanged(Configuration newConfig) {
        super.onConfigurationChanged(newConfig);

        if(newConfig.orientation==Configuration.ORIENTATION_PORTRAIT){
            mWebView.getLayoutParams().height = (int) getResources().getDimension(R.dimen.web_size_portrait);
            getActivity().getWindow().addFlags(WindowManager.LayoutParams.FLAG_FORCE_NOT_FULLSCREEN);
            getActivity().getWindow().clearFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN);
            getActivity().findViewById(R.id.container).requestLayout();
        }
        else {
             View decorView = getActivity().getWindow().getDecorView();
            getActivity().getWindow().addFlags(WindowManager.LayoutParams.FLAG_FULLSCREEN);
            getActivity().getWindow().clearFlags(WindowManager.LayoutParams.FLAG_FORCE_NOT_FULLSCREEN);
            getActivity().findViewById(R.id.container).requestLayout();
            mWebView.getLayoutParams().height = ViewGroup.LayoutParams.MATCH_PARENT;
        }
        mWebView.requestLayout();


    }
    public static String getIDYoutube(Context context,String url) {
    /*

    http://www.youtube.com/watch?v=dQw4w9WgXcQ&a=GxdCwVVULXctT2lYDEPllDR0LRTutYfW
    http://www.youtube.com/watch?v=dQw4w9WgXcQ
    http://youtu.be/dQw4w9WgXcQ
    http://www.youtube.com/embed/dQw4w9WgXcQ
    http://www.youtube.com/v/dQw4w9WgXcQ
    http://www.youtube.com/e/dQw4w9WgXcQ
    http://www.youtube.com/watch?v=dQw4w9WgXcQ
    http://www.youtube.com/watch?feature=player_embedded&v=dQw4w9WgXcQ
    http://www.youtube-nocookie.com/v/6L3ZvIMwZFM?version=3&hl=en_US&rel=0

    */
        String pattern = context.getString(R.string.youtube_pattern_id);

        Pattern compiledPattern = Pattern.compile(pattern);
        Matcher matcher = compiledPattern.matcher(url); //url is youtube url for which you want to extract the id.
        if (matcher.find()) {
            Log.d(TAG, "getIDYoutube: "+matcher.group());
            return matcher.group();
        }
        return null;
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        ButterKnife.bind(this,view);
        mWebView.setWebViewClient(new WebViewClient() {
            @Override
            public boolean shouldOverrideUrlLoading(WebView view, String url) {
                return false;
            }
        });

        WebSettings webSettings = mWebView.getSettings();

        webSettings.setLayoutAlgorithm(WebSettings.LayoutAlgorithm.SINGLE_COLUMN);
        webSettings.setPluginState(WebSettings.PluginState.ON);
        webSettings.setJavaScriptEnabled(true);
        mWebView.setScrollBarStyle(View.SCROLLBARS_INSIDE_OVERLAY);
        mWebView.reload();
        WebChromeClient webChromeClient = new WebChromeClient();
        mWebView.setWebChromeClient(webChromeClient);

        mIsPaused = true;
        mWebView.loadUrl("https://www.youtube.com/embed/" + getIDYoutube(getContext(),mURL)+getString(R.string.autoplay_youtube_pattern));
      //  mWebView.loadData(getHTML(getIDYoutube(mURL)),"text/html", "UTF-8");
}
    public String getHTML(String videoId) {
        String html = "<iframe class=\"youtube-player\" " + "style=\"border: 0; width: 100%; height: 96%;"
                + "padding:0px; margin:0px\" " + "id=\"ytplayer\" type=\"text/html\" "
                + "src=\"http://www.youtube.com/embed/" + videoId
                + "?&theme=dark&autohide=2&modestbranding=1&showinfo=0&autoplay=1\fs=0\" frameborder=\"0\" "
                + "allowfullscreen autobuffer " + "controls onclick=\"this.play()\">\n" + "</iframe>\n";
      //  LogShowHide.LogShowHideMethod("video-id from html url= ", "" + html);
        return html;
    }
    private boolean mIsPaused = false;

    @Override
    public void onResume() {
        super.onResume();
        if (mIsPaused)
        {
            // resume flash and javascript etc
            callHiddenWebViewMethod(mWebView, "onResume");
            mWebView.resumeTimers();
            mIsPaused = false;
        }
    }

    @Override
    public void onPause() {
        super.onPause();

        if (!mIsPaused)
        {
            // pause flash and javascript etc
            callHiddenWebViewMethod(mWebView, "onPause");
            mWebView.pauseTimers();
            mIsPaused = true;
        }
    }
    private void callHiddenWebViewMethod(final WebView wv, final String name)
    {
        try
        {
            final Method method = WebView.class.getMethod(name);
            method.invoke(mWebView);
        } catch (final Exception e)
        {}
    }

    @Override
    public int getPresentTransition() {
        return PresentStyle.SCALEXY;
    }
}
