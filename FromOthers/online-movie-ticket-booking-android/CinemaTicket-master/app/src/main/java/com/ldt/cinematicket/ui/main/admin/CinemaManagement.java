package com.ldt.cinematicket.ui.main.admin;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.ldt.cinematicket.R;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.PresentStyle;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class CinemaManagement extends SupportFragment {
    public static CinemaManagement newInstance() {
        return new CinemaManagement();
    }

    @BindView(R.id.back_button)
    ImageView mBackButton;
    @BindView(R.id.title)
    TextView mTitle;

    @OnClick(R.id.back_button)
    void back() {
        getMainActivity().dismiss();
    }


    @OnClick(R.id.see_all_cinema_panel)
    void goToAllCinemasPage() {
        getMainActivity().presentFragment(AllCinemas.newInstance());
    }

    @OnClick(R.id.choose_cinema_for_showing_panel)
    void goToChooseCinemasForShowing() {
        getMainActivity().presentFragment(ChooseCinema.newInstance());
    }

    @OnClick(R.id.add_new_cinema_panel)
    void gotToAddNewCinema() {
         getMainActivity().presentFragment(AddNewCinema.newInstance());
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this,view);
    }

    @Override
    public int getPresentTransition() {
        return PresentStyle.SLIDE_LEFT;
    }
    @Nullable
    @Override
    protected View onCreateView(LayoutInflater inflater, ViewGroup container) {
        return inflater.inflate(R.layout.admin_cinema_dashboard,container,false);
    }
}
