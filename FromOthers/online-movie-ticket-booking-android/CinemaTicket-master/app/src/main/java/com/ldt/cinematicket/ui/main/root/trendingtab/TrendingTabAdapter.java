package com.ldt.cinematicket.ui.main.root.trendingtab;

import android.content.Context;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentManager;
import android.support.v4.app.FragmentPagerAdapter;
import android.support.v4.view.ViewPager;

import com.ldt.cinematicket.R;

import java.util.ArrayList;

public class TrendingTabAdapter extends FragmentPagerAdapter {
    private Context mContext;
    private ArrayList<Fragment> mData = new ArrayList<>();
    private void initData() {
        mData.add(HomeChildTab.newInstance());
        mData.add(NowShowingChildTab.newInstance());
        mData.add(UpComingChildTab.newInstance());

    }

    public TrendingTabAdapter(Context context,FragmentManager fragmentManager) {
        super(fragmentManager);
        mContext = context;
        initData();
    }

    // Returns total number of pages
    @Override
    public int getCount() {
        return mData.size();
    }

    // Returns the fragment to display for that page
    @Override
    public Fragment getItem(int position) {
       return mData.get(position);
    }

    // Returns the page title for the top indicator
    @Override
    public CharSequence getPageTitle(int position) {
        switch (position) {
            case 0: return mContext.getResources().getString(R.string.home);
            case 1: return mContext.getResources().getString(R.string.now_showing);
            case 2: return mContext.getResources().getString(R.string.up_coming);
            default: return null;
        }

    }
}
