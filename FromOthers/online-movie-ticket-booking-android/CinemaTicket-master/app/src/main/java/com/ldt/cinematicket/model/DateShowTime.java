package com.ldt.cinematicket.model;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

/**
 * Showtime chứa một mảng các ngày chiếu,
 * mỗi ngày chiếu chứa ngày chiếu, và mảng giờ chiếu
 * mỗi giờ chiếu chứa một phòng chiếu, giá vé và một mảng ghế là boolean thể hiện ghế còn trống hay không, số ghế mặc định là 25
 */

public class DateShowTime {
    @Override
    public String toString() {
        return "DateShowTime{" +
                "mId=" + mId +
                ", mDate='" + mDate + '\'' +
                ", mDetailShowTimes=" + mDetailShowTimes +
                '}';
    }

    @SerializedName("id")
    private int mId;
    @SerializedName("date")
    private String mDate;

    public int getId() {
        return mId;
    }

    public void setId(int mId) {
        this.mId = mId;
    }

    public String getDate() {
        return mDate;
    }

    public void setDate(String mDate) {
        this.mDate = mDate;
    }

    public ArrayList<DetailShowTime> getDetailShowTimes() {
        return mDetailShowTimes;
    }

    public void setDetailShowTimes(ArrayList<DetailShowTime> mDetailShowTimes) {
        this.mDetailShowTimes = mDetailShowTimes;
    }

    @SerializedName("detailShowTimes")
    private ArrayList<DetailShowTime> mDetailShowTimes;
}
