package com.vacuum.app.metquiz.Fragments;

import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.app.Fragment;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.RelativeLayout;

import com.vacuum.app.metquiz.MainActivity;
import com.vacuum.app.metquiz.R;


/**
 * Created by Home on 10/7/2017.
 */

public class NotifyFragment extends Fragment {
    RecyclerView recyclerView;
    RelativeLayout red_badge;
    public NavigationView navigationView;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.notify_fragment, container, false);
        //recyclerView =  view.findViewById(R.id.recycler_view_one);
        red_badge = ((MainActivity) getActivity()).red_badge;
        navigationView = ((MainActivity) getActivity()).navigationView;

        if(savedInstanceState == null){
            red_badge.setVisibility(View.GONE);
            navigationView.getMenu().getItem(3).setActionView(null);
        }

        return view;
    }
}