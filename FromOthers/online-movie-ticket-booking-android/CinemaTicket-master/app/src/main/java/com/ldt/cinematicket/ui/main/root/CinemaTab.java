package com.ldt.cinematicket.ui.main.root;

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
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.QuerySnapshot;
import com.ldt.cinematicket.R;


import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

import com.ldt.cinematicket.model.Cinema;
import com.ldt.cinematicket.ui.main.MainActivity;

import java.util.ArrayList;
import java.util.Collections;
import java.util.Comparator;
import java.util.List;

public class CinemaTab extends Fragment implements OnCompleteListener<QuerySnapshot>, OnFailureListener {
    private static final String TAG ="CinemaTab";

    @BindView(R.id.swipe_layout)
    SwipeRefreshLayout swipeLayout;

    @BindView(R.id.textView)
    TextView mErrorTextView;

    @BindView(R.id.recycle_view)
    RecyclerView mRecyclerView;

    CinemaAdapter mAdapter;

    FirebaseFirestore db;

    public static CinemaTab newInstance() {
        CinemaTab fragment = new CinemaTab();
        return fragment;
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.now_showing,container,false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        Log.d(TAG, "onViewCreated: ");
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this,view);

        db = ((MainActivity)getActivity()).mDb;
        LinearLayoutManager layoutManager = new LinearLayoutManager(getContext(), LinearLayoutManager.VERTICAL,false);
        mRecyclerView.setLayoutManager(layoutManager);

        mAdapter = new CinemaAdapter(getActivity());
        mRecyclerView.setAdapter(mAdapter);
        swipeLayout.setOnRefreshListener(this::refreshData);
        refreshData();
    }
    public void refreshData() {
        swipeLayout.setRefreshing(true);
        db.collection("showing_cinema")
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

            List<Cinema> mM = querySnapshot.toObjects(Cinema.class);
            Collections.sort(mM, new Comparator<Cinema>() {
                @Override
                public int compare(Cinema o1, Cinema o2) {
                    return o1.getId() - o2.getId();
                }});
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
