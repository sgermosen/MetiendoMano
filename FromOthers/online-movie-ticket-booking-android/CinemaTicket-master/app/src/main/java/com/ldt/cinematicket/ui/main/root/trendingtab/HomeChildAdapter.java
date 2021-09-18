package com.ldt.cinematicket.ui.main.root.trendingtab;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.google.zxing.common.StringUtils;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.Movie;
import com.ldt.cinematicket.ui.main.MainActivity;
import com.ldt.cinematicket.ui.main.book.BookingFragment;
import com.ldt.cinematicket.ui.main.book.MovieDetail;

import java.util.ArrayList;
import java.util.List;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class HomeChildAdapter  extends RecyclerView.Adapter<HomeChildAdapter.ItemHolder> {
    private static final String TAG ="HomeChildAdapter";

    private List<Movie> mData = new ArrayList<>();
    Context mContext;

    public HomeChildAdapter(Context context) {
        mContext = context;
    }
    public void setData(List<Movie> data) {
        mData.clear();
        if (data !=null) {
            mData.addAll(data);
        }
        notifyDataSetChanged();
    }

    public void addData(List<Movie> data) {
        if(data!=null) {
            int posBefore = mData.size();
            mData.addAll(data);
            notifyItemRangeInserted(posBefore,data.size());
        }
    }

    @NonNull
    @Override
    public ItemHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
        LayoutInflater inflater = LayoutInflater.from(viewGroup.getContext());
        return new ItemHolder(inflater.inflate(R.layout.item_movie_home_tab,viewGroup,false));
    }

    @Override
    public void onBindViewHolder(@NonNull ItemHolder itemHolder, int i) {
        itemHolder.bind(mData.get(i));
    }

    @Override
    public int getItemCount() {
        return mData.size();
    }

    public class ItemHolder extends RecyclerView.ViewHolder implements View.OnClickListener {
        @BindView(R.id.image)
        ImageView mImage;

        @BindView(R.id.title)
        TextView mTitle;

        @BindView(R.id.note_text) TextView mNote;
        @BindView(R.id.rate) TextView mRate;
        public ItemHolder(View itemView) {
            super(itemView);
            itemView.setOnClickListener(this);
            ButterKnife.bind(this,itemView);
        }
        public String upperCaseAllFirst(String value) {

            char[] array = value.toCharArray();
            // Uppercase first letter.
            array[0] = Character.toUpperCase(array[0]);

            // Uppercase all letters that follow a whitespace character.
            for (int i = 1; i < array.length; i++) {
                if (Character.isWhitespace(array[i - 1])) {
                    array[i] = Character.toUpperCase(array[i]);
                }
            }

            // Result.
            return new String(array);
        }

        private void bind(Movie movie) {
            String title = movie.getTitle().toLowerCase();
            title = upperCaseAllFirst(title);
            Log.d(TAG, "bind: " +title);
            mTitle.setText(upperCaseAllFirst(title));
            mNote.setText(movie.getOpeningDay());
            mRate.setText(String.format("%s", movie.getRate()));

            RequestOptions requestOptions = new RequestOptions();
            Glide.with(mContext)
                    .load(movie.getImageUrl())
                    .apply(requestOptions)
                    .into(mImage);
        }

        @Override
        public void onClick(View v) {
            if(mContext instanceof MainActivity) {
                ((MainActivity) mContext).presentFragment(MovieDetail.newInstance(mData.get(getAdapterPosition())));
            }
        }

        @OnClick(R.id.book)
        void goToBook() {
            if(mContext instanceof MainActivity) {
                ((MainActivity) mContext).presentFragment(BookingFragment.newInstance(mData.get(getAdapterPosition())));
            }
        }
    }
}
