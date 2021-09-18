package com.ldt.cinematicket.util;

import android.app.Activity;
import android.content.Context;
import android.content.res.Resources;
import android.graphics.Point;
import android.os.Build;
import android.support.annotation.RequiresApi;
import android.util.DisplayMetrics;
import android.util.TypedValue;
import android.view.Display;
import android.view.WindowManager;

public class Tool {
    @RequiresApi(api = Build.VERSION_CODES.JELLY_BEAN_MR1)
    public static int getNavigationHeight(Activity activity)
    {

        int navigationBarHeight = 0;
        int resourceId = activity.getResources().getIdentifier("navigation_bar_height", "dimen", "android");
        if (resourceId > 0) {
            navigationBarHeight = activity.getResources().getDimensionPixelSize(resourceId);
        }
        if(!hasSoftKeys(activity.getWindowManager())) return 0;
        return  navigationBarHeight;
    }
    public static boolean hasSoftKeys(WindowManager windowManager){
        Display d = windowManager.getDefaultDisplay();

        DisplayMetrics realDisplayMetrics = new DisplayMetrics();
        d.getRealMetrics(realDisplayMetrics);

        int realHeight = realDisplayMetrics.heightPixels;
        int realWidth = realDisplayMetrics.widthPixels;

        DisplayMetrics displayMetrics = new DisplayMetrics();
        d.getMetrics(displayMetrics);

        int displayHeight = displayMetrics.heightPixels;
        int displayWidth = displayMetrics.widthPixels;

        return (realWidth - displayWidth) > 0 || (realHeight - displayHeight) > 0;
    }
    static int[] screenSize;
    static float[] screenSizeInDp;
    public static boolean HAD_GOT_SCREEN_SIZE = false;
    public static float getOneDps(Context context)
    {
        if(oneDPs !=-1) return oneDPs;
        //      oneDPs = context.getResources().getDimensionPixelOffset(R.dimen.oneDP);
        oneDPs = getPixelsFromDPs(context,1);
        return oneDPs;
    }
    public static float getPixelsFromDPs(Context activity, int dps){
        /*
            public abstract Resources getResources ()

                Return A Resources instance for your application's package.
        */
        Resources r = activity.getResources();

        /*
            TypedValue

                Container for A dynamically typed data value. Primarily
                used with Resources for holding resource values.
        */

        /*
            applyDimension(int unit, float value, DisplayMetrics metrics)

                Converts an unpacked complex data value holding
                A dimension to its final floating pp_point value.
        */

        /*
            Density-independent pixel (dp)

                A virtual pixel unit that you should use when defining UI layout,
                to express layout dimensions or posTop in A density-independent way.

                The density-independent pixel is equivalent to one physical pixel on
                A 160 dpi screen, which is the baseline density assumed by the system
                for A "medium" density screen. At runtime, the system transparently handles
                any scaling of the dp units, as necessary, based on the actual density
                of the screen in use. The conversion of dp units to screen pixels
                is simple: px = dp * (dpi / 160). For example, on A 240 dpi screen,
                1 dp equals 1.5 physical pixels. You should always use dp
                units when defining your application's UI, to ensure proper
                display of your UI on screens with different densities.
        */

        /*
            public static final int COMPLEX_UNIT_DIP

                TYPE_DIMENSION complex unit: Value is Device Independent Pixels.
                Constant Value: 1 (0x00000001)
        */
        return (TypedValue.applyDimension(
                TypedValue.COMPLEX_UNIT_DIP, dps, r.getDisplayMetrics()));
    }
    public static float oneDPs =-1;
    public static int getDpsFromPixel(Activity activity,int px) {
        DisplayMetrics displayMetrics = activity.getResources().getDisplayMetrics();
        return Math.round(px / (displayMetrics.xdpi / DisplayMetrics.DENSITY_DEFAULT));
    }
    public static int[] getScreenSize(Context context)
    {
        if(!HAD_GOT_SCREEN_SIZE) {
            Point p = new Point();
            Display d = ((WindowManager) context.getSystemService(Context.WINDOW_SERVICE)).getDefaultDisplay(); // this will get the view of screen
            d.getRealSize(p);
            int width = p.x;
            int height = p.y;
            screenSize = new int[] {width,height};
            screenSizeInDp = new float[] {(width+0.0f)/getOneDps(context),(height+0.0f)/getOneDps(context)};
            HAD_GOT_SCREEN_SIZE = true;
        }
        return screenSize;
    }
}
