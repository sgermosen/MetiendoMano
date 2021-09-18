package com.ldt.cinematicket.ui.main.admin;

import android.annotation.SuppressLint;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.v4.widget.SwipeRefreshLayout;
import android.support.v7.widget.AppCompatButton;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.QuerySnapshot;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.Cinema;
import com.ldt.cinematicket.ui.widget.SuccessTickView;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;
import com.tuyenmonkey.mkloader.MKLoader;

import java.util.ArrayList;
import java.util.Collections;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class ChooseCinema extends SupportFragment implements ChooseCinemaAdapter.CountingCallBack {
    private static final String TAG ="ChooseCinema";

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

    @BindView(R.id.count) TextView mCountTextView;

    @BindView(R.id.loader) MKLoader mLoader;

    @BindView(R.id.comfirm)
    AppCompatButton mComfirmButton;

    @BindView(R.id.success_tick_view)
    SuccessTickView mSuccessTickView;

    ChooseCinemaAdapter mAdapter;

    FirebaseFirestore db;


    @OnClick(R.id.back_button)
    void back() {
        getMainActivity().dismiss();
    }

    @SuppressLint("DefaultLocale")
    @Override
    public void onCountChanged(int newValue) {
        if(mCountTextView!=null) {
            if(newValue==0) mCountTextView.setVisibility(View.INVISIBLE); else mCountTextView.setVisibility(View.VISIBLE);
            mCountTextView.setText(String.format("%d", newValue));
        }

    }

    public static ChooseCinema newInstance() {
        ChooseCinema cm = new ChooseCinema();

        return cm;
    }

    @Nullable
    @Override
    protected View onCreateView(LayoutInflater inflater, ViewGroup container) {
        return inflater.inflate(R.layout.admin_choose_cinema,container,false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this,view);
        step = 0;

        mTitle.setText(R.string.choose_cinemas_for_showing);

        db = getMainActivity().mDb;
        LinearLayoutManager layoutManager = new LinearLayoutManager(getContext(), LinearLayoutManager.VERTICAL,false);
        mRecyclerView.setLayoutManager(layoutManager);

        mAdapter = new ChooseCinemaAdapter(getActivity());
        mRecyclerView.setAdapter(mAdapter);
        mAdapter.setCountingCallBack(this);
        swipeLayout.setOnRefreshListener(this::refreshData);
        refreshData();

    }

    // load 2 collection, counting
    int step = 0; // 0 -> 2
    public void refreshData() {

        mComfirmButton.setText(R.string.load_live);
        ChooseCinema.this.swipeLayout.setRefreshing(true);
        step = 0;
        db.collection("cinema_list")
                .get()
                .addOnCompleteListener(task -> ChooseCinema.this.onComplete("cinema_list",task))
                .addOnFailureListener(e -> ChooseCinema.this.onFailure("cinema_list",e));


        db.collection("showing_cinema")
                .get()
                .addOnCompleteListener(task -> ChooseCinema.this.onComplete("showing_cinema",task))
                .addOnFailureListener(e -> ChooseCinema.this.onFailure("showing_cinema",e));

    }

    @OnClick(R.id.comfirm)
    public void savedDataToServer() {

        mLoader.setVisibility(View.VISIBLE);
        mComfirmButton.setEnabled(false);
        mComfirmButton.setText(R.string.loading);

        List<Cinema> savedCinemas = mAdapter.getSavedSelectedData();
        for (int i = 0; i < savedCinemas.size(); i++) {
            db.collection("showing_cinema").document(savedCinemas.get(i).getId()+"").delete();
        }

        ArrayList<Cinema> cinemas = mAdapter.getSelectedData();

        for (int i = 0; i < cinemas.size(); i++) {
            if(i != cinemas.size() - 1)
                db.collection("showing_cinema").document(cinemas.get(i).getId()+"").set(cinemas.get(i));
            else
                db.collection("showing_cinema").document(cinemas.get(i).getId()+"").set(cinemas.get(i)).addOnSuccessListener(aVoid -> {

                    refreshData();

                }).addOnFailureListener(e -> {

                    refreshData();
                });
        }
    }

    public void onComplete(String query, @NonNull Task<QuerySnapshot> task) {
        step++;
        if(step==2) {
            if (swipeLayout.isRefreshing())
                swipeLayout.setRefreshing(false);

            mErrorTextView.setVisibility(View.GONE);
            mRecyclerView.setVisibility(View.VISIBLE);

            mLoader.setVisibility(View.INVISIBLE);
            mComfirmButton.setText(R.string.save_change);

            if(!mComfirmButton.isEnabled()) {

                mComfirmButton.postDelayed(() -> {
                    mComfirmButton.setEnabled(true);
                    mComfirmButton.setVisibility(View.VISIBLE);
                    mSuccessTickView.setVisibility(View.GONE);
                }, 1500);
                mSuccessTickView.setVisibility(View.VISIBLE);
                mSuccessTickView.startTickAnim();
            }
        }

        if (task.isSuccessful()&&query.equals("cinema_list")) {

            // result of cinema collection

            QuerySnapshot querySnapshot = task.getResult();
            List<Cinema> mM = querySnapshot.toObjects(Cinema.class);
            Collections.sort(mM, (o1, o2) -> o1.getId() - o2.getId());
            if(mAdapter!=null)
                mAdapter.setCinemaData(mM);

        } else if(task.isSuccessful()&&query.equals("showing_cinema")) {

            // result of now_showing or upcoming collection
            QuerySnapshot querySnapshot = task.getResult();
            List<Cinema> mM = querySnapshot.toObjects(Cinema.class);
            Collections.sort(mM, (o1, o2) -> o1.getId() - o2.getId());
            Log.d(TAG, "onComplete: number selected = "+mM.size());
            if(mAdapter!=null)
                mAdapter.setSelectedData(mM);

        } else
            Log.w(TAG, "Error getting documents.", task.getException());
    }


    public void onFailure(String query, @NonNull Exception e) {
        step++;
        Log.d(TAG, "onFailure");
        if(swipeLayout.isRefreshing())
            swipeLayout.setRefreshing(false);

        mRecyclerView.setVisibility(View.GONE);
        mErrorTextView.setVisibility(View.VISIBLE);
    }
}
