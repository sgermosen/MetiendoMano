package com.ldt.cinematicket.ui.main.book;

import android.content.Context;
import android.support.annotation.NonNull;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.DateShowTime;
import com.ldt.cinematicket.model.DetailShowTime;
import com.ldt.cinematicket.model.ShowTime;
import com.nex3z.flowlayout.FlowLayout;

import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.List;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class DetailShowTimeAdapter extends RecyclerView.Adapter<DetailShowTimeAdapter.ItemHolder> {
    private static final String TAG ="DetailShowTimeAdapter";

    public DetailShowTimeAdapter(Context context) {
        mContext = context;
    }
    private Context mContext;
    private ArrayList<ShowTime> mData = new ArrayList<>();
    private ArrayList<ShowTime> mFoundData = new ArrayList<>();
    private ArrayList<Integer> mFoundPos = new ArrayList<>();
    private Date mDateQuery = Calendar.getInstance().getTime();

    public void setDateQuery(Date date) {
        mDateQuery = date;
        getDatePos();
        notifyDataSetChanged();
    }

    public static boolean compareTwoDates(Date startDate, Date endDate) {
        Date sDate = getZeroTimeDate(startDate);
        Date eDate = getZeroTimeDate(endDate);
        return sDate.compareTo(eDate)==0;
    }

    private static Date getZeroTimeDate(Date date) {
        Calendar calendar = Calendar.getInstance();
        calendar.setTime(date);
        calendar.set(Calendar.HOUR_OF_DAY, 0);
        calendar.set(Calendar.MINUTE, 0);
        calendar.set(Calendar.SECOND, 0);
        calendar.set(Calendar.MILLISECOND, 0);
        date = calendar.getTime();
        return date;
    }
    public interface OnTimeClickListener{
        void onTimeClick(ShowTime showTime, int datePos, int timePos) ;
        void onNoResult();
    }
    private OnTimeClickListener mListener;
    public void setOnTimeClickListener(OnTimeClickListener listener) {
        mListener = listener;
    }
    private void getDatePos() {
        mFoundData.clear();
        mFoundPos.clear();
        if(mDateQuery!=null) {
            for (int i = 0; i < mData.size(); i++) {
                ShowTime s = mData.get(i);
                ArrayList<DateShowTime> mDateTime = s.getDateShowTime();
                for(int j=0;j<mDateTime.size();j++) {
                    boolean b = false;
                    try {
                        b = check2Date(mDateQuery,mDateTime.get(j).getDate());
                        Log.d(TAG, "getDatePos: date is "+mDateTime.get(j).getDate()+", it is "+b);

                    } catch (ParseException ignore) {}

                    if (b) {
                        // found
                        mFoundPos.add(j);
                        mFoundData.add(mData.get(i));
                        break;
                    }
                }
            }
        }
    }

    boolean check2StringDate(String one,String two) throws ParseException {
        SimpleDateFormat dateFormat = new SimpleDateFormat("dd/MM/yyyy");
        Date d1 = dateFormat.parse(one);
        Date d2 = dateFormat.parse(two);
        return d1.equals(d2);
    }
    SimpleDateFormat dateFormat = new SimpleDateFormat("dd/MM/yyyy");
    boolean check2Date(Date one, String two) throws ParseException {
       Date d2 = dateFormat.parse(two);
    //    Log.d(TAG, "check2Date: d1 is " + one.toString());
   //     Log.d(TAG, "check2Date: d2 is "+d2.toString());
    //    Log.d(TAG, "check2Date: result : "+one.equals(d2));
       return compareTwoDates(one,d2);
    }

    public void setData(List<ShowTime> data) {
        mData.clear();
        if(data!=null)
            mData.addAll(data);
        getDatePos();
        notifyDataSetChanged();
    }

    @NonNull
    @Override
    public ItemHolder onCreateViewHolder(@NonNull ViewGroup viewGroup, int i) {
        LayoutInflater inflater = LayoutInflater.from(viewGroup.getContext());
        View view = inflater.inflate(R.layout.item_cinema_booking, viewGroup, false);
        return new ItemHolder(view);
    }

    @Override
    public void onBindViewHolder(@NonNull ItemHolder itemHolder, int i) {
        itemHolder.bind(mFoundData.get(i));
    }

    @Override
    public int getItemCount() {
        return mFoundData.size();
    }

    public class ItemHolder extends RecyclerView.ViewHolder implements View.OnClickListener {
        @BindView(R.id.cinema_name)
        TextView mCinema;

        @BindView(R.id.minus)
        ImageView mMinus;

        @BindView(R.id.expand)
        View mExpandLayout;

        @BindView(R.id.type_movie) TextView mTypeMovie;
        @BindView(R.id.flow_layout)
        FlowLayout mFlowLayout;


        public ItemHolder(@NonNull View itemView) {
            super(itemView);
            ButterKnife.bind(this,itemView);
        }


        void bind(ShowTime showTime) {
            mCinema.setText(showTime.getCinemaName());
            int foundPos =mFoundPos.get(getAdapterPosition());
            if(foundPos!=-1) {
                itemView.setVisibility(View.VISIBLE);
                DateShowTime date = showTime.getDateShowTime().get(foundPos);
                ArrayList<DetailShowTime> details = date.getDetailShowTimes();
                mFlowLayout.removeAllViews();
                for (int i = 0; i < details.size(); i++) {
                    DetailShowTime detail = details.get(i);
                    TextView time = (TextView) LayoutInflater.from(itemView.getContext()).inflate(R.layout.time_text_view, mFlowLayout, false);
                    time.setText(detail.getTime());
                    time.setTag(R.id.savedValue1, getAdapterPosition());
                    time.setTag(R.id.savedValue2,foundPos);
                    time.setTag(R.id.savedValue3,i);
                    time.setOnClickListener(this);
                    mFlowLayout.addView(time);
                }

            } else {
                itemView.setVisibility(View.GONE);
            }
        }

        @OnClick({R.id.minus,R.id.cinema_name})
        void expandOrCollapse() {
            if(mExpandLayout.getVisibility()==View.GONE) {
                mExpandLayout.setVisibility(View.VISIBLE);
                mMinus.setImageResource(R.drawable.minus);
            } else {
                mExpandLayout.setVisibility(View.GONE);
                mMinus.setImageResource(R.drawable.ic_add_circle_black_24dp);

            }
        }

        @Override
        public void onClick(View v) {
            // Cần chỉ ra phần tử ShowTime nào, Thuộc DateShowTime nào, thuộc DetailShowTime nào
            Log.d(TAG, "onClick");
            if(v.getTag(R.id.savedValue1) instanceof Integer) {
                int showPos = (int) v.getTag(R.id.savedValue1);
                int datePos = (int) v.getTag(R.id.savedValue2);
                int timePos = (int) v.getTag(R.id.savedValue3);
                Log.d(TAG, "onClick: showPos = "+showPos+", datePos = "+datePos+", timePos = "+timePos);
                if(mListener!=null) mListener.onTimeClick(mFoundData.get(showPos),datePos,timePos);
            }
        }
    }
}
