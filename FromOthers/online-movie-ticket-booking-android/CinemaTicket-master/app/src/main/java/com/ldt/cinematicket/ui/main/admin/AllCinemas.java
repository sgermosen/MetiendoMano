package com.ldt.cinematicket.ui.main.admin;

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
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.QuerySnapshot;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.Cinema;
import com.ldt.cinematicket.ui.main.root.CinemaAdapter;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.PresentStyle;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;

import java.util.Collections;
import java.util.Comparator;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class AllCinemas extends SupportFragment implements OnCompleteListener<QuerySnapshot>, OnFailureListener{
    private static final String TAG ="AllCinemas";

    public static AllCinemas newInstance() {
        return new AllCinemas();
    }

    @BindView(R.id.back_button)
    ImageView mBackButton;

    @BindView(R.id.title)
    TextView mTitle;

    @BindView(R.id.recycle_view)
    RecyclerView mRecyclerView;

    @BindView(R.id.swipeLayout)
    SwipeRefreshLayout swipeLayout;

    @BindView(R.id.textView)
    TextView mErrorTextView;

    CinemaAdapter mAdapter;

    FirebaseFirestore db;


    @OnClick(R.id.back_button)
    void back() {
        getMainActivity().dismiss();
    }

    @Nullable
    @Override
    protected View onCreateView(LayoutInflater inflater, ViewGroup container) {
        return inflater.inflate(R.layout.admin_all_cinemas,container,false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this,view);
        db = getMainActivity().mDb;
        LinearLayoutManager layoutManager = new LinearLayoutManager(getContext(), LinearLayoutManager.VERTICAL,false);
        mRecyclerView.setLayoutManager(layoutManager);

        mAdapter = new CinemaAdapter(getActivity());
        mRecyclerView.setAdapter(mAdapter);

        setSwipeOptionForRecyclerView();


        swipeLayout.setOnRefreshListener(this::refreshData);
        refreshData();
    }

    private void setSwipeOptionForRecyclerView() {

    }

    public void refreshData() {
        swipeLayout.setRefreshing(true);
        db.collection("cinema_list")
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

    @Override
    public int getPresentTransition() {
        return PresentStyle.SLIDE_LEFT;
    }

}
