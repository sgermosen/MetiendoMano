package com.ldt.cinematicket.ui.main.admin;

import android.annotation.SuppressLint;
import android.graphics.drawable.Drawable;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.constraint.ConstraintLayout;
import android.support.design.widget.BottomSheetDialog;
import android.support.v4.widget.SwipeRefreshLayout;
import android.text.InputType;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.animation.AlphaAnimation;
import android.view.animation.Animation;
import android.view.animation.RotateAnimation;
import android.webkit.URLUtil;
import android.widget.EditText;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.bumptech.glide.Glide;
import com.bumptech.glide.load.DataSource;
import com.bumptech.glide.load.engine.GlideException;
import com.bumptech.glide.request.RequestListener;
import com.bumptech.glide.request.RequestOptions;
import com.bumptech.glide.request.target.Target;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnFailureListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.QuerySnapshot;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.Movie;
import com.ldt.cinematicket.ui.main.book.WebViewFragment;
import com.ldt.cinematicket.ui.widget.SuccessTickView;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;
import com.tuyenmonkey.mkloader.MKLoader;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Collections;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;
import butterknife.OnTextChanged;

public class AddNewMovie extends SupportFragment implements RequestListener<Drawable>, OnFailureListener, OnCompleteListener<QuerySnapshot> {
    private static final String TAG ="AddNewMovie";

    public static AddNewMovie newInstance() {
        return new AddNewMovie();
    }

    FirebaseFirestore mDb;

    @BindView(R.id.back_button)
    ImageView mBackButton;

    @BindView(R.id.title)
    TextView mTitle;
    @BindView(R.id.photo) ImageView mPhoto;
    @BindView(R.id.photo_empty_icon) ImageView mEmptyPhotoIcon;

    @BindView(R.id.option_panel)
    ConstraintLayout mOptionPanel;

    @BindView(R.id.click_to_expand_option)
    ImageView mClickToExpandOption;
    @BindView(R.id.photo_loader) MKLoader mPhotoLoader;

    @BindView(R.id.id) EditText mIDEditText;

    @BindView(R.id.swipe_layout)
    SwipeRefreshLayout mSwipeLayout;
    @BindView(R.id.done_button) ImageView mDoneButton;
    @BindView(R.id.check_youtube_link) View mCheckYoutubeLink;

    @BindView(R.id.opening_day) EditText mOpeningDay;
    @BindView(R.id.cast) EditText mCast;
    @BindView(R.id.director) EditText mDirector;
    @BindView(R.id.description) EditText mDescription;
    @BindView(R.id.genre) EditText mGenre;
    @BindView(R.id.type) EditText mType;
    @BindView(R.id.duration) EditText mDuration;
    @BindView(R.id.rate) EditText mRate;

    @OnClick(R.id.back_button)
    void back() {
        BottomSheetDialog dialog = new BottomSheetDialog(getActivity());

        dialog.setContentView(R.layout.alert_layout);
        dialog.findViewById(R.id.comfirm).setOnClickListener(v -> { dialog.dismiss(); getMainActivity().dismiss();});
        dialog.show();

    }
    @OnClick(R.id.click_to_expand_option)
    void ExpandOrCollapseOptionPanel(){
        if(mOptionPanel.getVisibility()==View.VISIBLE) {
            mClickToExpandOption.setRotation((float) Math.PI);
            RotateAnimation ra = new RotateAnimation(180,360,Animation.RELATIVE_TO_SELF, 0.5f, Animation.RELATIVE_TO_SELF, 0.5f);
            ra.setFillAfter(true);
            ra.setDuration(350);
            mClickToExpandOption.startAnimation(ra);
            mOptionPanel.setVisibility(View.GONE);
        }
        else {
         //   mClickToExpandOption.setRotation((float) Math.PI);
            RotateAnimation ra = new RotateAnimation(0,180,Animation.RELATIVE_TO_SELF, 0.5f, Animation.RELATIVE_TO_SELF, 0.5f);
            ra.setFillAfter(true);
            ra.setDuration(350);
            mClickToExpandOption.startAnimation(ra);
            mOptionPanel.setVisibility(View.VISIBLE);
        }
    }
    @OnClick(R.id.photo)
    void focusImageUrlEditText(){
        mImageUrlEditText.requestFocus();
    }
    private boolean[] mDone = new boolean[] {false,false,false,false};

    @BindView(R.id.image_url)
    EditText mImageUrlEditText;
    @BindView(R.id.name) EditText mName;

    @OnTextChanged(R.id.name)
    void onNameChanged(CharSequence s, int start, int before, int count) {
        if(mDone[1]&&s.length()==0) mDone[1] = false;
        else if(!mDone[1]&&s.length()!=0) mDone[1] = true;
        checkoutDone();
    }
    @OnTextChanged(R.id.opening_day)
    void onOpeningDayChanged(CharSequence s, int start, int before, int count) {
        if(mDone[2]&&s.length()==0) mDone[2] = false;
        else if(!mDone[2]&&s.length()!=0) mDone[2] = true;
        checkoutDone();
    }
    @OnTextChanged(R.id.trailer_youtube)
    void onTrailderYoutubeChanged(CharSequence s, int start, int before, int count) {
        if(mDone[3]&&s.length()==0) mDone[3] = false;
        else if(s.length()!=0) {
            if(URLUtil.isValidUrl(s.toString())&&WebViewFragment.getIDYoutube(getContext(),s.toString())!=null)
            mDone[3] = true;
            else {
                mDone[3] = false;
            }
        }
        if(mDone[3])  mCheckYoutubeLink.setVisibility(View.VISIBLE); else mCheckYoutubeLink.setVisibility(View.GONE);
        checkoutDone();
    }
    @OnTextChanged(R.id.id)
    void onIDChanged(CharSequence s, int start, int before, int count) {
        String str = s.toString();
        str = str.replace(getString(R.string.auto_id),"").replaceAll("\\D+","");
        if(!str.isEmpty()) {
                mID = Integer.parseInt(str);
                mFoundPos = findIdInData((int) mID);
                if(mFoundPos!=-1) {
                    mIDDetail.setVisibility(View.VISIBLE);
                    mAutoFill.setVisibility(View.VISIBLE);
                } else {
                    mIDDetail.setVisibility(View.GONE);
                    mAutoFill.setVisibility(View.GONE);
                }
        } else {
            mIDDetail.setVisibility(View.GONE);
            mAutoFill.setVisibility(View.GONE);
        }
    }
    @BindView(R.id.id_detail) TextView mIDDetail;
    @BindView(R.id.auto_fill) TextView mAutoFill;
    int mFoundPos = -1;
    int findIdInData(int id) {
        for (int i = 0; i < mData.size(); i++) {
            if(mData.get(i).getId()==id)
                return i;
        }
        return -1;
    }

    @OnClick(R.id.auto_fill)
    void autoFillForm() {
        Log.d(TAG, "autoFillForm: "+mFoundPos);
    if(mFoundPos!=-1&&mFoundPos<mData.size()) {
        Movie m = mData.get(mFoundPos);

        mImageUrlEditText.setText(m.getImageUrl());
        mIDEditText.setText(String.format("%d", m.getId()));
        mName.setText(m.getTitle());
        mCast.setText(m.getCast());
        mDirector.setText(m.getDirector());
        mDescription.setText(m.getDescription());
        mGenre.setText(m.getGenre());
        mType.setText(android.text.TextUtils.join(",", m.getType()));
        mDuration.setText(String.format("%d", m.getDuration()));
        mOpeningDay.setText(m.getOpeningDay());
        mTrailerYoutube.setText(m.getTrailerYoutube());
        mRate.setText(String.format("%s", m.getRate()));
    }
    }
    boolean mOk =false;
    private void checkoutDone() {
        for (int i = 0; i< mDone.length; i++) {
            Log.d(TAG, "checkoutDone: done["+i+"] = "+mDone[i]);
            if (!mDone[i]) {
                mDoneButton.setColorFilter(getResources().getColor(R.color.setting_label_color));
                mOk = false;
                return;
            }
        }
        mOk = true;
        mDoneButton.setColorFilter(getResources().getColor(R.color.FlatBlue));
    }

    @OnClick(R.id.done_button)
    void onClickDoneButton() {
        if(mOk) {
            addNewMovie();
        }
    }
    void addNewMovie() {
        Movie m = new Movie();
        m.setId((int) mID);
        m.setImageUrl(mImageUrlEditText.getText().toString());
        m.setTitle(mName.getText().toString());
        m.setOpeningDay(mOpeningDay.getText().toString());
        m.setTrailerYoutube(mTrailerYoutube.getText().toString());
        m.setCast(mCast.getText().toString());

        m.setDirector(mDirector.getText().toString());
        m.setDescription(mDescription.getText().toString());
        m.setGenre(mGenre.getText().toString());
        m.setType(new ArrayList<>(Arrays.asList(mType.getText().toString().split("\\s*,\\s*"))));

        String dur = mDuration.getText().toString();
        int durInt=0;
        if(!dur.isEmpty()) {
            try {
             durInt = Integer.parseInt(dur);
            } catch (NumberFormatException ignored) { }
        }

        m.setDuration(durInt);

        String rateStr = mRate.getText().toString();
        double rate = 0;
        if(!rateStr.isEmpty()) {
            try {
                rate = Double.parseDouble(rateStr);
            } catch (NumberFormatException ignored) { }
        }
        m.setRate(rate);

        Log.d(TAG, m.toString());
         mSendingDialog = new BottomSheetDialog(getActivity());

        mSendingDialog.setContentView(R.layout.send_new_movie);
        mSendingDialog.setCancelable(false);
        mSendingDialog.findViewById(R.id.close).setOnClickListener(v -> cancelSending());
        mSendingDialog.show();
        sendData(m);
    }
    void sendData(Movie movie){
        mDb.collection("movie").document(movie.getId()+"").set(movie).addOnSuccessListener(aVoid -> {
            setOnSuccess();

        }).addOnFailureListener(e -> {
            setOnFailure();
        });
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
                    mTitle.postDelayed(()->getMainActivity().dismiss(),350);
                },2000);
                s.setVisibility(View.VISIBLE);
                s.startTickAnim();
                setTextSending("Movie has been added to database",getResources().getColor(R.color.FlatGreen));
            }
        }
    }
    void setOnFailure() {
        if(mSendingDialog!=null){
            mSendingDialog.dismiss();
            Toast.makeText(getContext(),"Cannot add movie, please try again!",Toast.LENGTH_SHORT);
        }
    }

    @BindView(R.id.trailer_youtube) EditText mTrailerYoutube;
    @OnClick(R.id.check_youtube_link)
    void goToYoutube(){
        getMainActivity().presentFragment(WebViewFragment.newInstance(mTrailerYoutube.getText().toString()));
    }
    @OnTextChanged(R.id.image_url)
    void onImageUrlChanged(CharSequence s, int start, int before, int count) {
        String url = s.toString();
        if(!url.isEmpty()&&URLUtil.isValidUrl(url)) {
            mPhotoLoader.setVisibility(View.VISIBLE);
            RequestOptions requestOptions = new RequestOptions();
            Glide.with(getContext())
                    .load(url)
                    .addListener(this)
                    .into(mPhoto);

        } else {
            mPhoto.setImageDrawable(null);
            mEmptyPhotoIcon.setVisibility(View.VISIBLE);
            mPhotoLoader.setVisibility(View.GONE);
            mDone[0] = false;
            checkoutDone();
            // TODO: create a snack bar say that error
        }

    }

    @Nullable
    @Override
    protected View onCreateView(LayoutInflater inflater, ViewGroup container) {
        return inflater.inflate(R.layout.admin_add_new_movie,container,false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this,view);
        mDb = getMainActivity().mDb;
        ExpandOrCollapseOptionPanel();
        //disableIdField();
        mSwipeLayout.setOnRefreshListener(this::refreshData);
        refreshData(false);
       // autoFill();
    }
    private void disableIdField() {
        mIDEditText.setFocusable(false);
        mIDEditText.setEnabled(false);
        mIDEditText.setCursorVisible(false);
       // mIDEditText.setKeyListener(null);
    }
    private void enableIDField() {
        mIDEditText.setFocusable(true);
        mIDEditText.setEnabled(true);
        mIDEditText.setCursorVisible(true);
      //  mIDEditText.setInputType(InputType.TYPE_NULL);
        mIDEditText.setInputType(InputType.TYPE_CLASS_TEXT);
    }

    @Override
    public boolean onLoadFailed(@Nullable GlideException e, Object model, Target<Drawable> target, boolean isFirstResource) {
        mPhoto.setImageDrawable(null);
        mEmptyPhotoIcon.setVisibility(View.VISIBLE);
        mPhotoLoader.setVisibility(View.GONE);
        mDone[0] = false;
        checkoutDone();
        return false;
    }

    @Override
    public boolean onResourceReady(Drawable resource, Object model, Target<Drawable> target, DataSource dataSource, boolean isFirstResource) {
        mEmptyPhotoIcon.setVisibility(View.GONE);
        mPhotoLoader.setVisibility(View.GONE);
        mDone[0] = true;
        checkoutDone();
        return false;
    }

    @Override
    public boolean isReadyToDismiss() {
        back();
        return false;
    }

    @Override
    public void onFailure(@NonNull Exception e) {
        mID = -1;
        mIDEditText.setText(getString(R.string.fail_to_create_id));
    }
    public void refreshData() {
        refreshData(true);
    }

    public void refreshData(boolean b) {

//        mDb.collection("database_info").document("movie_info")
//                .get()
//                .addOnSuccessListener(this)
//                .addOnFailureListener(this);
        if(b)
        mSwipeLayout.setRefreshing(true);
        mDb.collection("movie")
                .get()
                .addOnCompleteListener(this)
                .addOnFailureListener(this);
    }

    public long mID = -1;

    @Override
    public void onComplete(@NonNull Task<QuerySnapshot> task) {
        if(mSwipeLayout.isRefreshing())
            mSwipeLayout.setRefreshing(false);
        if (task.isSuccessful()) {
            QuerySnapshot querySnapshot = task.getResult();

            List<Movie> mM = querySnapshot.toObjects(Movie.class);
            Collections.sort(mM, (o1, o2) -> (int) (o1.getId() - o2.getId()));
            mData.clear();
            mData.addAll(mM);
           createNewID(mM);

        } else
            Log.w(TAG, "Error getting documents.", task.getException());
    }
    ArrayList<Movie> mData = new ArrayList<>();
    private void createNewID(List<Movie> data) {
        Log.d(TAG, "createNewID: size = "+data.size());
        for (int i = 0; i < data.size(); i++) {
            if(data.get(i).getId()>i+1) {
                Log.d(TAG, "createNewID: i = "+(i+1));
                mID = i+1;
                break;
            } else if(i==data.size()-1) {
                Log.d(TAG, "createNewID: end = "+(data.size()+1));
                mID = data.size()+1;
            }
        }
        mIDEditText.setText(String.format("%d %s", mID, getString(R.string.auto_id)));
      //  enableIDField();
    }
    @SuppressLint("SetTextI18n")
    void autoFill() {
        mTrailerYoutube.setText("https://youtu.be/nQ0YLvMUNb4");
        mImageUrlEditText.setText("http://t3.gstatic.com/images?q=tbn:ANd9GcQtNHKNrwbRPGbZMxcMHT52YTf79LLcmEWx8OQX5Nrx7nc1SWRa");
        mName.setText("A NAME FOR EVERY MOVIE");
        mOpeningDay.setText("10/11/2019");
    }
}
