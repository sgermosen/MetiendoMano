package com.ldt.cinematicket.ui.main.admin;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.support.annotation.NonNull;
import android.support.v7.widget.AppCompatCheckBox;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.CompoundButton;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.Cinema;
import com.ldt.cinematicket.ui.main.MainActivity;
import com.ldt.cinematicket.ui.main.book.MovieDetail;
import com.makeramen.roundedimageview.RoundedImageView;

import java.util.ArrayList;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnCheckedChanged;
import butterknife.OnClick;

public class ChooseCinemaAdapter extends RecyclerView.Adapter<ChooseCinemaAdapter.ItemHolder> {
    private static final String TAG ="ChooseCinemaAdapter";

    private List<Cinema> mCinemaData = new ArrayList<>();
    private List<Cinema> mSavedSelectedCinemaData = new ArrayList<>();
    private List<Boolean> mSelectedData = new ArrayList<>();
    private Context mContext;
    int count = 0;

    public interface CountingCallBack {
        void onCountChanged(int newValue);
    }

    private CountingCallBack mCountingCallBack;

    public void setCountingCallBack(CountingCallBack callBack) {
        mCountingCallBack = callBack;

        if(callBack!=null)
            callBack.onCountChanged(count);
    }

    ChooseCinemaAdapter(Context context) {
        this.mContext = context;
    }

    private void setCount(int value) {
        count = value;
        if(mCountingCallBack!=null) mCountingCallBack.onCountChanged(value);
    }

    public void setCinemaData(List<Cinema> data) {
        mCinemaData.clear();
        mSelectedData.clear();
        setCount(0);

        if (data !=null) {
            mCinemaData.addAll(data);
        }

        getSelectedIndex();
        notifyDataSetChanged();
    }

    public List<Cinema> getSavedSelectedData() {
        return mSavedSelectedCinemaData;
    }

    private void getSelectedIndex() {
        mSelectedData.clear();
        setCount(0);

        for (Cinema ignored : mCinemaData) mSelectedData.add(false);

        for (int i = 0; i < mCinemaData.size(); i++) {

            for (int j = 0; j < mSavedSelectedCinemaData.size(); j++) {
                if (mCinemaData.get(i).getId() == mSavedSelectedCinemaData.get(j).getId()) {

                    Log.d(TAG, "setSelectedData: detect selected i = " + i);
                    setCount(count + 1);
                    mSelectedData.set(i, true);
                    break;
                }
            }

        }
    }

    public void setSelectedData(List<Cinema> data) {
        mSavedSelectedCinemaData.clear();

        if(data!=null)
            mSavedSelectedCinemaData.addAll(data);

        getSelectedIndex();
        notifyDataSetChanged();
    }

    public ArrayList<Cinema> getSelectedData() {
        ArrayList<Cinema> data = new ArrayList<>();

        for (int i = 0; i < mCinemaData.size(); i++) {
            if(mSelectedData.get(i)) data.add(mCinemaData.get(i));
        }

        return data;
    }


    @NonNull
    @Override
    public ItemHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        LayoutInflater inflater = LayoutInflater.from(parent.getContext());
        View view = inflater.inflate(R.layout.item_admin_choose_cinema, parent, false);

        return new ItemHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ItemHolder holder, int position) {
        holder.bind(mCinemaData.get(position));
    }

    @Override
    public int getItemCount() {
        return mCinemaData.size();
    }


    public class ItemHolder extends RecyclerView.ViewHolder {

        @BindView(R.id.panel)
        View mPanel;

        @BindView(R.id.checkbox)
        AppCompatCheckBox mCheckbox;

        @BindView(R.id.img)
        RoundedImageView mImg;

        @BindView(R.id.txt_name)
        TextView mName;

        @BindView(R.id.txt_id)
        TextView mID;

        @BindView(R.id.txt_address)
        TextView mAddress;


        ItemHolder(View itemView) {
            super(itemView);
            ButterKnife.bind(this,itemView);
        }



        @OnClick(R.id.panel)
        void clickPanel() {
            boolean b = mSelectedData.get(getAdapterPosition());
            mSelectedData.set(getAdapterPosition(),!mSelectedData.get(getAdapterPosition()));
            b = !b;

            if(b)
                setCount(count+1);
            else
                setCount(count-1);

            notifyItemChanged(getAdapterPosition());
        }

        @SuppressLint("DefaultLocale")
        public void bind(Cinema cinema) {

            mName.setText(cinema.getName());
            mID.setText(String.format("ID : %d", cinema.getId()));

            // set Address
            String Address = cinema.getAddress();


            mAddress.setText(String.format(mContext.getString(R.string.address), Address));

            if(mSelectedData.size() > getAdapterPosition() && mSelectedData.get(getAdapterPosition())) {
                Log.d(TAG, "bind: select position = " + getAdapterPosition());
                mPanel.setBackgroundResource(R.drawable.background_item_choose_movie_select);
                if(!mCheckbox.isChecked())
                mCheckbox.setChecked(true);
            }
            else {
                mPanel.setBackgroundResource(R.drawable.background_item_choose_movie);
                if(mCheckbox.isChecked())
                mCheckbox.setChecked(false);
            }

            RequestOptions requestOptions = new RequestOptions().override(mImg.getWidth());
            Glide.with(mContext)
                    .load(cinema.getImageUrl())
                    .apply(requestOptions)
                    .into(mImg);
        }
    }
}
