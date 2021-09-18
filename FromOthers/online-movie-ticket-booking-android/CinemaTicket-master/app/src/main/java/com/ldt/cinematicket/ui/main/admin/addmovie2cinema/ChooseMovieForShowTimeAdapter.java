package com.ldt.cinematicket.ui.main.admin.addmovie2cinema;

import android.annotation.SuppressLint;
import android.content.Context;
import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.Movie;
import com.makeramen.roundedimageview.RoundedImageView;

import java.util.ArrayList;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class ChooseMovieForShowTimeAdapter extends RecyclerView.Adapter<ChooseMovieForShowTimeAdapter.ItemHolder> {
    private static final String TAG ="ChooseMovieAdapter";

    private List<Movie> mMovieData = new ArrayList<>();
    private List<Integer> mHighlightMovieIDData = new ArrayList<>();
    private List<Boolean> mSelectedData = new ArrayList<>();
    private Context mContext;
    private int count = 0;
    public interface CountingCallBack {
        void onCountChanged(int newValue);
    }
    private CountingCallBack mCountingCallBack;
    void setCountingCallBack(CountingCallBack callBack) {
        mCountingCallBack = callBack;

        if(callBack!=null)
            callBack.onCountChanged(count);
    }
    public interface MovieOnClickListener {
        void OnMovieClicked(Movie movie);
    }

    private MovieOnClickListener mListener;
    public void setMovieOnClickListener(MovieOnClickListener listener) {
        mListener = listener;
    }

    ChooseMovieForShowTimeAdapter(Context context) {
        this.mContext = context;
    }
    private void setCount(int value) {
        count = value;
        if(mCountingCallBack!=null) mCountingCallBack.onCountChanged(value);
    }

    public void setMovieData(List<Movie> data) {
        mMovieData.clear();
        mSelectedData.clear();
        setCount(0);
        if (data !=null) {
            mMovieData.addAll(data);

        }
        getSelectedIndex();
        notifyDataSetChanged();
    }
    private void getSelectedIndex() {
        mSelectedData.clear();
        setCount(0);

        for (Movie ignored : mMovieData) mSelectedData.add(false);

        for (int i = 0; i < mMovieData.size(); i++) {

            for (int j = 0; j < mHighlightMovieIDData.size(); j++) {
                if (mMovieData.get(i).getId() == mHighlightMovieIDData.get(j)) {

                    Log.d(TAG, "setSelectedData: detect selected i = " + i);

                    mMovieData.add(count,mMovieData.remove(i));
                    mSelectedData.set(i, true);
                    mSelectedData.add(count,mSelectedData.remove(i));

                    setCount(count + 1);
                    break;
                }
            }

        }
    }

    public void setHighlightMovieIDData(List<Integer> data) {
        mHighlightMovieIDData.clear();
        if(data!=null)
        mHighlightMovieIDData.addAll(data);

        getSelectedIndex();
        notifyDataSetChanged();
    }
    public ArrayList<Movie> getSelectedData() {
        ArrayList<Movie> data = new ArrayList<>();
        for (int i = 0; i < mMovieData.size(); i++) {
            if(mSelectedData.get(i)) data.add(mMovieData.get(i));
        }
        return data;
    }


    @NonNull
    @Override
    public ItemHolder onCreateViewHolder(@NonNull ViewGroup parent, int viewType) {
        LayoutInflater inflater = LayoutInflater.from(parent.getContext());
        View view = inflater.inflate(R.layout.item_choose_movie_for_showtime, parent, false);

        return new ItemHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ItemHolder holder, int position) {
        holder.bind(mMovieData.get(position));
    }

    @Override
    public int getItemCount() {
        return mMovieData.size();
    }


    public class ItemHolder extends RecyclerView.ViewHolder {

        @BindView(R.id.panel)
        View mPanel;

        @BindView(R.id.img)
        RoundedImageView mImg;

        @BindView(R.id.txt_name)
        TextView mName;

        @BindView(R.id.txt_id)
        TextView mID;

        @BindView(R.id.txt_release)
        TextView mReleaseDay;


        ItemHolder(View itemView) {
            super(itemView);
            ButterKnife.bind(this,itemView);

        }

        @OnClick(R.id.panel)
        void clickPanel() {
          if(mListener!=null) mListener.OnMovieClicked(mMovieData.get(getAdapterPosition()));
        }

        @SuppressLint("DefaultLocale")
        public void bind(Movie movie) {

            mName.setText(movie.getTitle());
            mID.setText(String.format("%d", movie.getId()));
            mReleaseDay.setText(movie.getOpeningDay());

            if(mSelectedData.size()>getAdapterPosition() && mSelectedData.get(getAdapterPosition())) {
                Log.d(TAG, "bind: select position = "+getAdapterPosition());
                mPanel.setBackgroundResource(R.drawable.black_rounded_big_selected);
            }
            else {
                mPanel.setBackgroundResource(R.drawable.black_rounded_big);
            }

            RequestOptions requestOptions = new RequestOptions().override(mImg.getWidth());
            Glide.with(mContext)
                    .load(movie.getImageUrl())
                    .apply(requestOptions)
                    .into(mImg);

        }
    }
}
