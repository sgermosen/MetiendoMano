package com.ldt.cinematicket.ui.main.admin.addmovie2cinema;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.widget.EditText;
import android.widget.LinearLayout;

import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.DateShowTime;
import com.ldt.cinematicket.model.DetailShowTime;
import com.ldt.cinematicket.model.ShowTime;

import java.util.ArrayList;
import java.util.Collections;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class LinearHolderAdapter {
    private ArrayList<DateShowTime> mDateShowTimes = new ArrayList<>();
    private ArrayList<ItemHolder> mItemHolders = new ArrayList<>();
    private LinearLayout mParenLayout;
    private Context mContext;

    public LinearHolderAdapter(Context context, LinearLayout linearLayout) {
        this.mParenLayout = linearLayout;
        mContext = context;
    }
    public void setShowTime(ShowTime s) {
        mDateShowTimes.clear();
        mDateShowTimes.addAll(s.getDateShowTime());
        mParenLayout.removeAllViews();
        mItemHolders.clear();

        for (int i = 0; i < mDateShowTimes.size(); i++) {
            createAndShowNewItemHolder(mDateShowTimes.get(i));
        }
    }
    public void getShowTime(ShowTime s) {
        for (int i = 0; i < mDateShowTimes.size(); i++) {
            mItemHolders.get(i).getDateShowTime(mDateShowTimes.get(i));
        }
        s.setDateShowTime(mDateShowTimes);
    }

    private void createAndShowNewDateShowTime() {
        DateShowTime s = new DateShowTime();
        s.setDate("");
        s.setDetailShowTimes(new ArrayList<>());
        mDateShowTimes.add(s);
        createAndShowNewItemHolder(s);
    }

    private void createAndShowNewItemHolder(DateShowTime dateShowTime) {
        View v = LayoutInflater.from(mContext).inflate(R.layout.item_show_time,mParenLayout,false);
        ItemHolder itemHolder = new ItemHolder(v,mItemHolders.size());
        itemHolder.bind(dateShowTime);
        mItemHolders.add(itemHolder);
        mParenLayout.addView(v);
    }

    private void removeItemHolder(int pos) {
        mParenLayout.removeView(mItemHolders.get(pos).mItemView);
        mItemHolders.remove(mItemHolders);
    }


    public class ItemHolder {
        private int mPosition;
        private View mItemView;
        private ArrayList<DetailShowTime> mDetailShowTimes = new ArrayList<>();
        private ArrayList<SubItemHolder> mSubItemHolders = new ArrayList<>();
        @BindView(R.id.date)
        EditText mDate;
        @BindView(R.id.time_parent) LinearLayout mTimeParent;

        @OnClick(R.id.add_time)
        void addTime() {
        createAndShowNewTime();
        }
        void createAndShowNewTime() {
            DetailShowTime d = new DetailShowTime();
            d.setTime("");
            d.setRoom(mDetailShowTimes.size()+1);
            d.setPrice(100000);
            d.setSeatRowNumber(5);
            d.setSeatColumnNumber(5);
            mDetailShowTimes.add(d);
            createAndShowNewSubItemHolder(d);
        }

        void createAndShowNewSubItemHolder(DetailShowTime detailShowTime) {
            View v = LayoutInflater.from(mContext).inflate(R.layout.item_detail_show_time,mParenLayout,false);
            SubItemHolder itemHolder = new SubItemHolder(v,mItemHolders.size());
            itemHolder.bind(detailShowTime);
            mSubItemHolders.add(itemHolder);
            mTimeParent.addView(v);
        }

        @OnClick(R.id.minus)
        void minus() {
            removeItemHolder(mPosition);
        }

        @OnClick(R.id.add)
        void add() {
            createAndShowNewDateShowTime();
        }
        // use once time
        public void bind(DateShowTime dateShowTime) {
            mTimeParent.removeAllViews();
            mDate.setText(dateShowTime.getDate());

            // bind sub item holder
            mDetailShowTimes.clear();
            mSubItemHolders.clear();

            mDetailShowTimes.addAll(dateShowTime.getDetailShowTimes());
            for (int i = 0; i < mDetailShowTimes.size(); i++) {
             createAndShowNewSubItemHolder(mDetailShowTimes.get(i));
            }
        }
        public void getDateShowTime(DateShowTime dateShowTime) {
            dateShowTime.setDate(mDate.getText().toString());

            for (int i = 0; i < mDetailShowTimes.size(); i++) {
                mSubItemHolders.get(i).getDetailShowTime(mDetailShowTimes.get(i));
            }
            dateShowTime.getDetailShowTimes().clear();
            dateShowTime.getDetailShowTimes().addAll(mDetailShowTimes);
        }

        public ItemHolder(View v, int position) {
            this.mPosition = position;
            this.mItemView = v;
            ButterKnife.bind(this,v);
        }

        public class SubItemHolder {
            private int mPosition;
            private View mItemView;
            @BindView(R.id.time) EditText mTime;
            @BindView(R.id.room) EditText mRoom;
            @BindView(R.id.price) EditText mPrice;
            @BindView(R.id.column) EditText mColumn;
            @BindView(R.id.row) EditText mRow;

            public void bind(DetailShowTime detailShowTime) {
                mTime.setText(detailShowTime.getTime());
                mRoom.setText(detailShowTime.getRoom()+"");
                mPrice.setText(detailShowTime.getPrice()+"");
                mColumn.setText(String.format("%d", detailShowTime.getSeatColumnNumber()));
                mRow.setText(String.format("%d", detailShowTime.getSeatRowNumber()));
            }

            public SubItemHolder(View v, int position) {
                this.mPosition = position;
                this.mItemView = v;
                ButterKnife.bind(this,v);
            }

            public void getDetailShowTime(DetailShowTime detailShowTime) {
              detailShowTime.setTime(mTime.getText().toString());
              int room = mPosition;

              try {
                  room =Integer.parseInt(mRoom.getText().toString());
              } catch (Exception ignore) {};

              detailShowTime.setRoom(room);

              int price = 100000;
              try {
                  price = Integer.parseInt(mPrice.getText().toString());
              } catch (Exception ignore) {}

              detailShowTime.setPrice(price);
              int column = 5;
                try {
                    column = Integer.parseInt(mColumn.getText().toString());
                } catch (Exception ignore) {}

                detailShowTime.setSeatColumnNumber(column);
                int row = 5;
                try {
                    row = Integer.parseInt(mRow.getText().toString());
                } catch (Exception ignore) {}

                detailShowTime.setSeatRowNumber(row);
                ArrayList<Boolean> b = new ArrayList<>(detailShowTime.getSeatColumnNumber()*detailShowTime.getSeatRowNumber());
                b.addAll(Collections.nCopies(detailShowTime.getSeatColumnNumber()*detailShowTime.getSeatRowNumber(), Boolean.FALSE));
                detailShowTime.setSeats(b);
            }

        }
    }
}
