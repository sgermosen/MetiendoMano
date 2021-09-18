package com.ldt.cinematicket.ui.main.root.trendingtab;

import android.app.Activity;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.firestore.QuerySnapshot;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.Movie;
import com.ldt.cinematicket.ui.main.MainActivity;

import java.util.Collections;
import java.util.Comparator;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;

public class HomeChildTab extends Fragment implements OnCompleteListener<QuerySnapshot>, OnFailureListener {

    private static final String TAG ="HomeChildTab";

    public static HomeChildTab newInstance() {
        return new HomeChildTab();
    }

    @BindView(R.id.recycle_view) RecyclerView mRecyclerView;

    @BindView(R.id.swipe_layout) SwipeRefreshLayout mSwipeLayout;
    @BindView(R.id.error)
    TextView mError;

    HomeChildAdapter mAdapter;

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.home_child_tab,container,false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this,view);
        mAdapter = new HomeChildAdapter(getActivity());
        mRecyclerView.setAdapter(mAdapter);
        mRecyclerView.setLayoutManager(new LinearLayoutManager(getContext(),LinearLayoutManager.VERTICAL,false));
        mSwipeLayout.setOnRefreshListener(this::refreshData);
        refreshData();
    }

    private void refreshData() {
        mSwipeLayout.setRefreshing(true);
        Activity activity  = getActivity();
        if(activity instanceof MainActivity) {
            ((MainActivity) getActivity()).mDb.collection("feature_movie")
                    .get()
                    .addOnCompleteListener(this)
                    .addOnFailureListener(this);
        }
    }

    @Override
    public void onComplete(@NonNull Task<QuerySnapshot> task) {
        if(mSwipeLayout.isRefreshing())
            mSwipeLayout.setRefreshing(false);

        mError.setVisibility(View.GONE);
        mRecyclerView.setVisibility(View.VISIBLE);

        if (task.isSuccessful()) {
            QuerySnapshot querySnapshot = task.getResult();

            List<Movie> mM = querySnapshot.toObjects(Movie.class);
            Collections.sort(mM, new Comparator<Movie>() {
                @Override
                public int compare(Movie o1, Movie o2) {
                    return o2.getId() - o1.getId();
                }});
            if(mAdapter!=null)
                mAdapter.setData(mM);

        } else
            Log.w(TAG, "Error getting documents.", task.getException());

    }

    @Override
    public void onFailure(@NonNull Exception e) {
        Log.d(TAG, "onFailure");
        if(mSwipeLayout.isRefreshing())
            mSwipeLayout.setRefreshing(false);

        mRecyclerView.setVisibility(View.GONE);
        mError.setVisibility(View.VISIBLE);
    }
}
