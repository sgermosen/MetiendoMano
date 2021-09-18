package com.ldt.cinematicket.model;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

/**
 * Showtime chứa một mảng các ngày chiếu,
 * mỗi ngày chiếu chứa ngày chiếu, và mảng giờ chiếu
 * mỗi giờ chiếu chứa một phòng chiếu, giá vé và một mảng ghế là boolean thể hiện ghế còn trống hay không, số ghế mặc định là 25
 */
public class ShowTime {
    @SerializedName("cinemaName")
    private String mCinemaName ="";
    @SerializedName("dates")
    private ArrayList<DateShowTime> mDateShowTime;
    @SerializedName("id")
    private int mID;
    @SerializedName("movieName")
    private String mMovieName="";
    @SerializedName("movieID")
    private Integer mMovieID;
    @SerializedName("cinemaID")
    private Integer mCinemaID;

    public Integer getMovieID() {
        return mMovieID;
    }

    public void setMovieID(Integer mMovieID) {
        this.mMovieID = mMovieID;
    }

    public void setCinemaID(Integer mCinemaID) {
        this.mCinemaID = mCinemaID;
    }

    @Override
    public String toString() {
        return "ShowTime{" +
                "mID=" + mID +
                ", mMovieName='" + mMovieName + '\'' +
                ", mCinemaName='" + mCinemaName + '\'' +
                ", mDateShowTime=" + mDateShowTime +
                '}';
    }

    public ArrayList<DateShowTime> getDateShowTime() {
        return mDateShowTime;
    }

    public void setDateShowTime(ArrayList<DateShowTime> mDateShowTime) {
        this.mDateShowTime = mDateShowTime;
    }

    public String getMovieName() {
        return mMovieName;
    }

    public void setMovieName(String mMovieName) {
        this.mMovieName = mMovieName;
    }

    public String getCinemaName() {
        return mCinemaName;
    }

    public void setCinemaName(String mCinemaName) {
        this.mCinemaName = mCinemaName;
    }

    public int getID() {
        return mID;
    }

    public void setID(int mID) {
        this.mID = mID;
    }

    public Integer getCinemaID() {
        return mCinemaID;
    }
}
