package com.ldt.cinematicket.ui.main.root.trendingtab;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.design.widget.TabLayout;
import android.support.v4.app.Fragment;
import android.support.v4.view.ViewPager;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.ldt.cinematicket.R;

public class TrendingTab extends Fragment {
    private static final String TAG ="TrendingTab";

    private TabLayout mTabLayout;
    TrendingTabAdapter mTabAdapter;
    ViewPager mViewPager;
    public static TrendingTab newInstance() {
        TrendingTab fragment = new TrendingTab();
        return fragment;
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.trending_tab,container,false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        Log.d(TAG, "onViewCreated: ");

        super.onViewCreated(view, savedInstanceState);
       mTabLayout =  view.findViewById(R.id.trending_tab_layout);
       mTabAdapter = new TrendingTabAdapter(getActivity(),getChildFragmentManager());
       mViewPager = view.findViewById(R.id.tab_view_pager);
       mViewPager.setAdapter(mTabAdapter);
       mTabLayout.setupWithViewPager(mViewPager);
        mViewPager.addOnPageChangeListener(new TabLayout.TabLayoutOnPageChangeListener(mTabLayout));
       // mTabLayout.setTabsFromPagerAdapter(mTabAdapter);//deprecated
        mTabLayout.addOnTabSelectedListener(new TabLayout.ViewPagerOnTabSelectedListener(mViewPager));


    }
}
