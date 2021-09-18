package com.ldt.cinematicket.ui.main.root.trendingtab;

import android.app.Activity;
import android.content.Context;
import android.graphics.Color;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.LinearLayout;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.Movie;
import com.ldt.cinematicket.ui.main.MainActivity;
import com.ldt.cinematicket.ui.main.book.MovieDetail;

import java.util.ArrayList;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class NowShowingAdapter extends RecyclerView.Adapter<NowShowingAdapter.ItemHolder> {
    private List<Movie> mData = new ArrayList<>();
    Context mContext;

    public NowShowingAdapter(Context context) {
        this.mContext = context;
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


    @Override
    public ItemHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        LayoutInflater inflater = LayoutInflater.from(parent.getContext());
        View view = inflater.inflate(R.layout.item_movie_nowshowing_tab, parent, false);

        return new ItemHolder(view);
    }

    @Override
    public void onBindViewHolder(ItemHolder holder, int position) {
       holder.bind(mData.get(position));
    }

    @Override
    public int getItemCount() {
        return mData.size();
    }


    public class ItemHolder extends RecyclerView.ViewHolder {
        @BindView(R.id.txt_name) TextView txtName;
        @BindView(R.id.txt_rating) TextView txtRating;

        @BindView(R.id.type_linear_layout) LinearLayout mTypeParent;

        @BindView(R.id.txt_director) TextView txtDirector;
        @BindView(R.id.txt_actors) TextView txtCast;
        @BindView(R.id.img) ImageView image;
        @BindView(R.id.panel) View panel;

        public ItemHolder(View itemView) {
            super(itemView);
            ButterKnife.bind(this,itemView);

        }

        @OnClick(R.id.panel)
        void clickPanel() {
            if(mContext instanceof MainActivity)
                ((MainActivity) mContext).presentFragment(MovieDetail.newInstance(mData.get(getAdapterPosition())));
        }
        public void bind(Movie movie) {

            txtName.setText(movie.getTitle());

            // holder.txtRating.setText(mData.get(position).getRating().toString());

            List<String> types = movie.getType();
            mTypeParent.removeAllViews();
            if(mContext instanceof Activity) {
                LayoutInflater inflater = ((Activity) mContext).getLayoutInflater();
                if(inflater!=null)
                for (String type : types) {
                    if(!type.isEmpty()) {
                        TextView tv = (TextView) inflater.inflate(R.layout.type_movie_text_view, mTypeParent, false);
                        tv.setText(type);
                        //tv.setBackgroundColor(Color.GREEN);
                        tv.setBackgroundResource(R.drawable.round_yellow_drawable);
                        mTypeParent.addView(tv);
                    }
                }
            }

            txtDirector.setText(movie.getDirector());
            txtCast.setText(movie.getCast());

            RequestOptions requestOptions = new RequestOptions().override(image.getWidth());
            Glide.with(mContext)
                    .load(movie.getImageUrl())
                    .apply(requestOptions)
                    .into(image);
        }
    }
}
