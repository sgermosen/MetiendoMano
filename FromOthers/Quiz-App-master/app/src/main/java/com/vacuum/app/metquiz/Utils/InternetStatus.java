package com.vacuum.app.metquiz.Utils;

import android.content.Context;
import android.net.ConnectivityManager;

/**
 * Created by Home on 11/26/2017.
 */

public class InternetStatus {

    Context mContext;
    public InternetStatus(Context context)
    {
        this.mContext = context;
    }

    public boolean isNetworkConnected() {
        ConnectivityManager cm = (ConnectivityManager) mContext.getSystemService(Context.CONNECTIVITY_SERVICE);

        return cm.getActiveNetworkInfo() != null;
    }

}
