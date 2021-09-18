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

public class MovieManagement extends SupportFragment {
    public static MovieManagement newInstance() {
        return new MovieManagement();
    }
    @BindView(R.id.back_button)
    ImageView mBackButton;
    @BindView(R.id.title)
    TextView mTitle;

    @OnClick(R.id.back_button)
    void back() {
        getMainActivity().dismiss();
    }

    @OnClick(R.id.see_all_movie_panel)
    void goToAllMoviePage() {
        getMainActivity().presentFragment(AllMovie.newInstance());
    }

    @OnClick({R.id.cncm_movie,R.id.next_cncm})
    void goToChooseMoviesForNowShowing() {
        getMainActivity().presentFragment(ChooseMovie.newInstance(ChooseMovie.MODE.NOW_SHOWING));
    }

    @OnClick({R.id.choose_feature_movie,R.id.next_feature_movie})
    void goToChooseFeatureMovies() {
        getMainActivity().presentFragment(ChooseMovie.newInstance(ChooseMovie.MODE.FEATURE));
    }

    @OnClick({R.id.choose_upcoming_movie,R.id.next_choose_upcoming})
    void goToChooseMoviesForUpComing() {
        getMainActivity().presentFragment(ChooseMovie.newInstance(ChooseMovie.MODE.UP_COMING));
    }

    @OnClick(R.id.add_new_movie_panel)
    void goToAddNewMovie() {
        getMainActivity().presentFragment(AddNewMovie.newInstance());
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
        return inflater.inflate(R.layout.admin_movie_dashboard,container,false);
    }
}
