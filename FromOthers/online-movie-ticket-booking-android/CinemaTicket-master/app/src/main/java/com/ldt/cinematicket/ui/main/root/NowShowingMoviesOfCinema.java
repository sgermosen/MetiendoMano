package com.ldt.cinematicket.ui.main.root;

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
import android.widget.Toast;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.QuerySnapshot;
import com.ldt.cinematicket.R;


import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

import com.ldt.cinematicket.model.Movie;
import com.ldt.cinematicket.ui.main.MainActivity;
import com.ldt.cinematicket.ui.main.root.trendingtab.NowShowingAdapter;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

public class NowShowingMoviesOfCinema extends SupportFragment implements OnCompleteListener<QuerySnapshot>, OnFailureListener { // implement 2 cai nay
    private static final String TAG ="ShowingMoviesOfCinema";
    private ArrayList<Integer> mMovies;
    private String CinemaName;

    @BindView(R.id.swipe_layout)
    SwipeRefreshLayout swipeLayout;

    @BindView(R.id.errorMessage)
    TextView mErrorTextView;

    @BindView(R.id.noMovie)
    TextView mNoMovieTextView;

    @BindView(R.id.title)
    TextView mTitle;

    @BindView(R.id.recycle_view)
    RecyclerView mRecyclerView;

    NowShowingAdapter mAdapter;

    FirebaseFirestore db;

    @OnClick(R.id.back_button)
    void back() {
        getMainActivity().dismiss();
    }

    public static NowShowingMoviesOfCinema newInstance(ArrayList<Integer> Movies, String CinemaName) {
        NowShowingMoviesOfCinema fragment = new NowShowingMoviesOfCinema();

        fragment.mMovies = Movies;
        fragment.CinemaName = CinemaName;

        return fragment;
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container) {
        return inflater.inflate(R.layout.now_showing_for_cinema,container,false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        Log.d(TAG, "onViewCreated: ");
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this,view);

        mTitle.setText(CinemaName);

        db = ((MainActivity)getActivity()).mDb;
        LinearLayoutManager layoutManager = new LinearLayoutManager(getContext(), LinearLayoutManager.VERTICAL,false);
        mRecyclerView.setLayoutManager(layoutManager);

        mAdapter = new NowShowingAdapter(getActivity());
        mRecyclerView.setAdapter(mAdapter);
        swipeLayout.setOnRefreshListener(this::refreshData);

        refreshData();
    }

    public void refreshData() {
        swipeLayout.setRefreshing(true);
        db.collection("now_showing")
                .get()
                .addOnCompleteListener(this)
                .addOnFailureListener(this);
    }

    @Override
    public void onComplete(@NonNull Task<QuerySnapshot> task) {

        if(swipeLayout.isRefreshing())
            swipeLayout.setRefreshing(false);

        mErrorTextView.setVisibility(View.GONE);
        mRecyclerView.setVisibility(View.VISIBLE);

        if (task.isSuccessful()) {
            QuerySnapshot querySnapshot = task.getResult();

            List<Movie> mM = querySnapshot.toObjects(Movie.class);

            // Lọc lại danh sách Movies
            int i = 0;

            while (i < mM.size())
            {
                // Nếu ID phim không khớp với ID trong danh sách Cinema thì xoá phim đó
                if (!mMovies.contains(mM.get(i).getId()))
                {
                    mM.remove(i);
                }
                else
                    i++;
            }

            if (mM.isEmpty()) // Nếu danh sách Movies rỗng thì hiện thông báo rỗng
            {
                mRecyclerView.setVisibility(View.GONE);
                mNoMovieTextView.setVisibility(View.VISIBLE);
            }
            else { // Nếu đã rỗng thì khỏi sort luôn
                Collections.sort(mM, new Comparator<Movie>() {
                    @Override
                    public int compare(Movie o1, Movie o2) {
                        return o1.getId() - o2.getId();
                    }
                });
            }

            if(mAdapter!=null)
                mAdapter.setData(mM);

        } else
            Log.w(TAG, "Error getting documents.", task.getException());
    }

    @Override
    public void onFailure(@NonNull Exception e) {
        Log.d(TAG, "onFailure");
        if(swipeLayout.isRefreshing())
            swipeLayout.setRefreshing(false);

        mRecyclerView.setVisibility(View.GONE);
        mErrorTextView.setVisibility(View.VISIBLE);
    }
}
