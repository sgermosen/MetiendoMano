package com.ldt.cinematicket.ui.main.admin.addmovie2cinema;

import android.annotation.SuppressLint;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.design.widget.BottomSheetDialog;
import android.support.v4.widget.SwipeRefreshLayout;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.AlphaAnimation;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.EventListener;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.FirebaseFirestoreException;
import com.google.firebase.firestore.QuerySnapshot;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.Cinema;
import com.ldt.cinematicket.model.DateShowTime;
import com.ldt.cinematicket.model.Movie;
import com.ldt.cinematicket.model.ShowTime;
import com.ldt.cinematicket.ui.widget.SuccessTickView;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;
import com.tuyenmonkey.mkloader.MKLoader;

import java.util.ArrayList;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

/**
 * Truyền vào một rạp và một phim
 * Tạo một lịch chiếu cho phim đó trên rạp đó
 */
public class AddShowTime extends SupportFragment implements EventListener<QuerySnapshot>, OnSuccessListener<Void>, OnFailureListener, OnCompleteListener<DocumentSnapshot> {
    private static final String TAG ="AddShowTime";


    /**
     * Truyền vào một rạp và một phim
     * Tạo một lịch chiếu cho phim đó trên rạp đó
     * @param cinema rạp cần thêm phim
     * @param movie phim được thêm
     * @return Fragment tương ứng
     */
    public static AddShowTime newInstance(Cinema cinema, Movie movie, UpdatableCallback callback) {
        if(cinema==null||movie==null||callback==null) throw new NullPointerException("cinema and movie can't be null");
        AddShowTime csf = new AddShowTime();
        csf.mCinema = cinema;
        csf.mMovie = movie;
        csf.mCallback = callback;
        return csf;
    }
    private ShowTime mShowTime;

    @Override
    public void onEvent(@javax.annotation.Nullable QuerySnapshot queryDocumentSnapshots, @javax.annotation.Nullable FirebaseFirestoreException e) {
        mSwipeLayout.setRefreshing(false);
        if(e==null) {

            ShowTime s = null;
            if(queryDocumentSnapshots!=null)
                s = queryDocumentSnapshots.getDocuments().get(0).toObject(ShowTime.class);
           if(s==null) {
               Log.d(TAG, "onEvent: null ShowTIme");
               createNewShowTime();
           }
           else {
               Log.d(TAG, "onEvent: not null ShowTime");
               mShowTime = s;
               mShowTime.setCinemaID(mCinema.getId());
               mShowTime.setMovieID(mMovie.getId());
               setAdapterShowTime(mShowTime);
           }
        }
    }

    @Override
    public void onSuccess(Void aVoid) {
        Log.d(TAG, "onSuccess");
        success_step--;
        if(success_step==0)
        setOnSuccess();
    }

    @Override
    public void onFailure(@NonNull Exception e) {
        Log.d(TAG, "onFailure");
        setOnFailure();
    }
    long count;

    // Hàm này được gọi để lấy số phần tử của show time
    // Hàm này ko được gọi ở bất cứ chỗ nào khác
    @Override
    public void onComplete(@NonNull Task<DocumentSnapshot> task) {
        count = 0;
        if(task.getResult()!=null) {
           try {
               count = task.getResult().getLong("count");
           } catch (NullPointerException ignore) {};

        }
        count++;
        mIDEditText.setText(count+"");
    }
    private void disableIdField() {
        mIDEditText.setFocusable(false);
        mIDEditText.setEnabled(false);
        mIDEditText.setCursorVisible(false);
        // mIDEditText.setKeyListener(null);
    }

    public interface UpdatableCallback {
        void onUpdate();
    }
    private UpdatableCallback mCallback;

    Cinema mCinema;
    Movie mMovie;

    LinearHolderAdapter mAdapter;

    @BindView(R.id.show_time_parent)
    LinearLayout mShowTimeParent;

    @BindView(R.id.cinema_name)
    TextView mCinemaTextView;

    @BindView(R.id.movie_name) TextView mMovieTextView;
    @BindView(R.id.id)
    EditText mIDEditText;

    @BindView(R.id.swipe_layout)
    SwipeRefreshLayout mSwipeLayout;
    @OnClick(R.id.done_button)
    void submit() {
        mAdapter.getShowTime(mShowTime);
        int id = 0;
        try {
            id = Integer.parseInt(mIDEditText.getText().toString());
         } catch (Exception ignore) {};

        mShowTime.setID(id);
        Log.d(TAG, mShowTime.toString());

        mSendingDialog = new BottomSheetDialog(getActivity());

        mSendingDialog.setContentView(R.layout.send_new_movie);
        mSendingDialog.setCancelable(false);
        mSendingDialog.findViewById(R.id.close).setOnClickListener(v -> cancelSending());
        mSendingDialog.show();
        success_step = 1;
        mDb.collection("show_time").document(mShowTime.getID()+"").set(mShowTime).addOnSuccessListener(this).addOnFailureListener(this);

        if(!mCinema.getMovies().contains(mMovie.getId())){
            mCinema.getMovies().add(mMovie.getId());
            mCinema.getShowTimes().add(mShowTime.getID());
            success_step = 2;
            mDb.collection("database_info").document("show_time_info").update("count",count);
            mDb.collection("cinema_list").document(mCinema.getId()+"").set(mCinema).addOnSuccessListener(this).addOnFailureListener(this);
        }
    }
    private int success_step = 0;

    @OnClick(R.id.back_button)
    void back() {
        BottomSheetDialog dialog = new BottomSheetDialog(getActivity());

        dialog.setContentView(R.layout.alert_layout);
        dialog.findViewById(R.id.comfirm).setOnClickListener(v -> { dialog.dismiss();
        getMainActivity().dismiss();});
        dialog.show();
    }

    @Nullable
    @Override
    protected View onCreateView(LayoutInflater inflater, ViewGroup container) {
        return inflater.inflate(R.layout.admin_config_show_time,container,false);
    }

    FirebaseFirestore mDb;

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this,view);
        mDb = getMainActivity().mDb;
        mAdapter = new LinearHolderAdapter(getActivity(),mShowTimeParent);
        disableIdField();
        mSwipeLayout.setOnRefreshListener(this::refreshData);
        refreshData();
    }

    private void refreshData() {
        mSwipeLayout.setRefreshing(true);
       int foundPos = mCinema.getMovies().indexOf(mMovie.getId());
       if(foundPos!=-1)
        mDb.collection("show_time").whereEqualTo("id",mCinema.getShowTimes().get(foundPos)).limit(1).addSnapshotListener(this);
       else {
         createNewShowTime();
       }
    }
    private void createNewShowTime() {
        mDb.collection("database_info").document("show_time_info").get().addOnCompleteListener(this).addOnFailureListener(this);

        mShowTime = new ShowTime();
        mShowTime.setCinemaName(mCinema.getName());
        mShowTime.setMovieName(mMovie.getTitle());
        mShowTime.setCinemaID(mCinema.getId());
        mShowTime.setMovieID(mMovie.getId());
        DateShowTime dst = new DateShowTime();
        dst.setDate("");
        dst.setDetailShowTimes(new ArrayList<>());
        ArrayList<DateShowTime> arr = new ArrayList<>();
        arr.add(dst);
        mShowTime.setDateShowTime(arr);
        setAdapterShowTime(mShowTime);
    }

    @SuppressLint("DefaultLocale")
    private void setAdapterShowTime(ShowTime s) {
        mSwipeLayout.setRefreshing(false);
        mMovieTextView.setText(s.getMovieName());
        mCinemaTextView.setText(s.getCinemaName());
        mIDEditText.setText(String.format("%d", s.getID()));
        mAdapter.setShowTime(s);
    }
    BottomSheetDialog mSendingDialog;
    boolean cancelled = false;
    void cancelSending() {
        if(mSendingDialog!=null)
            mSendingDialog.dismiss();
        cancelled = true;
    }
    void setTextSending(String text,int color) {
        if(mSendingDialog!=null) {
            TextView textView = mSendingDialog.findViewById(R.id.sending_text);
            if(textView!=null) {

                AlphaAnimation aa = new AlphaAnimation(0,1);
                aa.setFillAfter(true);
                aa.setDuration(500);
                textView.setText(text);
                textView.setTextColor(color);
                textView.startAnimation(aa);
            }
        }
    }
    void setOnSuccess() {
        if(cancelled) return;
        cancelled= false;
        if(mSendingDialog!=null) {
            MKLoader mkLoader = mSendingDialog.findViewById(R.id.loading);
            if(mkLoader!=null) mkLoader.setVisibility(View.INVISIBLE);
            SuccessTickView s = mSendingDialog.findViewById(R.id.success_tick_view);
            if(s!=null) {

                s.postDelayed(() -> {
                    mSendingDialog.dismiss();
                    if(mCallback!=null) mCallback.onUpdate();
                    mIDEditText.postDelayed(()->getMainActivity().dismiss(),350);
                },2000);
                s.setVisibility(View.VISIBLE);
                s.startTickAnim();
                setTextSending("Showtime has been create/edit",getResources().getColor(R.color.FlatGreen));
            }
        }
    }
    void setOnFailure() {
        if(mSendingDialog!=null){
            mSendingDialog.dismiss();
            Toast.makeText(getContext(),"Cannot add showtime, please try again!",Toast.LENGTH_SHORT);
        }
    }

}
