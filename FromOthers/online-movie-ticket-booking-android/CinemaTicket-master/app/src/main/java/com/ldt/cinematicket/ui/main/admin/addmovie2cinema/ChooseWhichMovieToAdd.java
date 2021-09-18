package com.ldt.cinematicket.ui.main.admin.addmovie2cinema;

import android.annotation.SuppressLint;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.QuerySnapshot;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.Cinema;
import com.ldt.cinematicket.model.Movie;
import com.ldt.cinematicket.ui.main.root.trendingtab.NowShowingAdapter;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.PresentStyle;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;

import java.util.Collections;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class ChooseWhichMovieToAdd extends SupportFragment implements ChooseMovieForShowTimeAdapter.MovieOnClickListener, ChooseMovieForShowTimeAdapter.CountingCallBack, OnCompleteListener<QuerySnapshot>, OnFailureListener, AddShowTime.UpdatableCallback {
    private static final String TAG ="ChooseWhichMovieToAdd";

    public static ChooseWhichMovieToAdd newInstance(@NonNull Cinema cinema) {
        ChooseWhichMovieToAdd f = new ChooseWhichMovieToAdd();
        f.mCinema = cinema;
        return f;
    }

    private Cinema mCinema;

    @Nullable
    @Override
    protected View onCreateView(LayoutInflater inflater, ViewGroup container) {
        return inflater.inflate(R.layout.admin_choose_movie_to_add_showtime,container,false);
    }

    @BindView(R.id.back_button)
    ImageView mBackButton;

    @BindView(R.id.title)
    TextView mTitle;

    @BindView(R.id.recycle_view)
    RecyclerView mRecyclerView;

    @BindView(R.id.swipe_layout)
    SwipeRefreshLayout mSwipeLayout;

    @BindView(R.id.textView)
    TextView mErrorTextView;

    @BindView(R.id.count) TextView mCountTextView;

    ChooseMovieForShowTimeAdapter mAdapter;

    FirebaseFirestore mDb;

    @OnClick(R.id.back_button)
    void back() {
        getMainActivity().dismiss();
    }


    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this,view);
        mDb = getMainActivity().mDb;
        LinearLayoutManager layoutManager = new LinearLayoutManager(getContext(), LinearLayoutManager.VERTICAL,false);
        mRecyclerView.setLayoutManager(layoutManager);

        mAdapter = new ChooseMovieForShowTimeAdapter(getActivity());
        mAdapter.setMovieOnClickListener(this);
        mAdapter.setCountingCallBack(this);
        mAdapter.setHighlightMovieIDData(mCinema.getMovies());

        mRecyclerView.setAdapter(mAdapter);

        mSwipeLayout.setOnRefreshListener(this::refreshData);
        refreshData();
    }

    public void refreshData() {
        mSwipeLayout.setRefreshing(true);
        mDb.collection("movie")
                .get()
                .addOnCompleteListener(this)
                .addOnFailureListener(this);
    }

    @Override
    public void onComplete(@NonNull Task<QuerySnapshot> task) {

        if(mSwipeLayout.isRefreshing())
            mSwipeLayout.setRefreshing(false);

        mErrorTextView.setVisibility(View.GONE);
        mRecyclerView.setVisibility(View.VISIBLE);

        if (task.isSuccessful()) {
            QuerySnapshot querySnapshot = task.getResult();

            List<Movie> mM = querySnapshot.toObjects(Movie.class);
            Collections.sort(mM, (o1, o2) -> o1.getId() - o2.getId());
            if(mAdapter!=null)
                mAdapter.setMovieData(mM);

        } else
            Log.w(TAG, "Error getting documents.", task.getException());
    }

    @Override
    public void onFailure(@NonNull Exception e) {
        Log.d(TAG, "onFailure");
        if(mSwipeLayout.isRefreshing())
            mSwipeLayout.setRefreshing(false);

        mRecyclerView.setVisibility(View.GONE);
        mErrorTextView.setVisibility(View.VISIBLE);
    }

    @Override
    public int getPresentTransition() {
        return PresentStyle.SLIDE_UP;
    }

    @Override
    public void OnMovieClicked(Movie movie) {
        getMainActivity().presentFragment(AddShowTime.newInstance(mCinema,movie,this));
    }

    @SuppressLint("DefaultLocale")
    @Override
    public void onCountChanged(int newValue) {
        if(mCountTextView!=null) {
            if(newValue==0) mCountTextView.setVisibility(View.INVISIBLE); else mCountTextView.setVisibility(View.VISIBLE);
            mCountTextView.setText(String.format("%d", newValue));
        }

    }


    @Override
    public void onUpdate() {
        refreshData();
    }
}
