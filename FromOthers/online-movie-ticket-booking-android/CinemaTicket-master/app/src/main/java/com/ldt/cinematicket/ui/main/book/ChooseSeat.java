package com.ldt.cinematicket.ui.main.book;


import android.os.Bundle;

import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.ldt.cinematicket.R;
import com.ldt.cinematicket.ui.main.MainActivity;
import com.ldt.cinematicket.ui.widget.MotionLayout;
import com.ldt.cinematicket.ui.widget.SeatPicker;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;
import com.tuyenmonkey.mkloader.MKLoader;

/**
 * A simple {@link Fragment} subclass.
 */
public class ChooseSeat extends SupportFragment implements MotionLayout.MotionListener {
private static final String TAG ="ChooseSeat";
private static void lg(String value) {
    Log.d(TAG,value);
}


    public ChooseSeat() {
        // Required empty public constructor
    }
    protected int[] local_pos = new int[2];
    protected View srcView;
    public static ChooseSeat newInstance(View sourceView) {

        ChooseSeat chooseSeat = new ChooseSeat();
        sourceView.getLocationInWindow(chooseSeat.local_pos);
        lg(chooseSeat.local_pos[0]+", "+chooseSeat.local_pos[1]);
       // sourceView.getLocationOnScreen(chooseSeat.local_pos);
        chooseSeat.srcView = sourceView;
        return chooseSeat;
    }
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container) {
        // Inflate the layout for this fragment
        root = (MotionLayout) inflater.inflate(R.layout.choose_seat, container, false);
        root.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                ((MainActivity)getActivity()).dismiss();
            }
        });
        seatPicker = root.findViewById(R.id.seat_picker);
        loader = root.findViewById(R.id.mkloader);
        bookNowButton = root.findViewById(R.id.book);
        return root;
    }
    private MKLoader loader;
    private SeatPicker seatPicker;
    private MotionLayout root;
    private View bookNowButton;

    @Override
    public void onActivityCreated(@Nullable Bundle savedInstanceState) {
        super.onActivityCreated(savedInstanceState);
        root.postDelayed(new Runnable() {
            @Override
            public void run() {
                root.StartMotion(ChooseSeat.this,srcView,local_pos);
            }
        },100);


    }

    @Override
    public void EndMotion() {
        root.postDelayed(new Runnable() {
            @Override
            public void run() {
            loader.setVisibility(View.GONE);
            seatPicker.setVisibility(View.VISIBLE);
            bookNowButton.setVisibility(View.VISIBLE);
            //TODO: Show the seat picker
            }
        },1000);
    }
}
