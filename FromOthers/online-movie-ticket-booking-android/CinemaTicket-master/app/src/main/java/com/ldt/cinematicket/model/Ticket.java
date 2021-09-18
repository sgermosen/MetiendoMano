package com.ldt.cinematicket.model;

import com.google.gson.annotations.SerializedName;

public class Ticket {
    @SerializedName("id")
    private int mID;
    @SerializedName("movieName")
    private String mMovieName;
    @SerializedName("cinemaName")
    private String mCinemaName;
    @SerializedName("time")
    private String mTime;
    @SerializedName("date")
    private String mDate;
    @SerializedName("seat")
    private String mSeat;
    @SerializedName("price")
    private int mPrice;
    @SerializedName("userUID")
    private String mUserUID;
    @SerializedName("room")
    private int mRoom;
    @SerializedName("cinemaID")
    private int mCinemaID;
    @SerializedName("movieID")
    private int mMovieID;

    public int getID() {
        return mID;
    }

    public Ticket setID(int mID) {
        this.mID = mID;
        return this;
    }

    public String getMovieName() {
        return mMovieName;
    }

    public Ticket setMovieName(String mMovieName) {
        this.mMovieName = mMovieName;
        return this;
    }

    public String getCinemaName() {
        return mCinemaName;
    }

    public Ticket setCinemaName(String mCinemaName) {
        this.mCinemaName = mCinemaName;
        return this;
    }

    public String getTime() {
        return mTime;
    }

    public Ticket setTime(String mTime) {
        this.mTime = mTime;
        return this;
    }

    public String getDate() {
        return mDate;
    }

    public Ticket setDate(String mDate) {
        this.mDate = mDate;
        return this;
    }

    public String getSeat() {
        return mSeat;
    }

    public Ticket setSeat(String mSeat) {
        this.mSeat = mSeat;
        return this;
    }

    public int getPrice() {
        return mPrice;
    }

    public Ticket setPrice(int mPrice) {
        this.mPrice = mPrice;
        return this;
    }

    public String getUserUID() {
        return mUserUID;
    }

    public Ticket setUserUID(String mUserUID) {
        this.mUserUID = mUserUID;
        return this;
    }

    public int getRoom() {
        return mRoom;
    }

    public Ticket setRoom(int mRoom) {
        this.mRoom = mRoom;
        return this;
    }

    public int getCinemaID() {
        return mCinemaID;
    }

    public Ticket setCinemaID(int mCinemaID) {
        this.mCinemaID = mCinemaID;
        return this;
    }

    public int getMovieID() {
        return mMovieID;
    }

    public Ticket setMovieID(int mMovieID) {
        this.mMovieID = mMovieID;
        return this;
    }

    @Override
    public String toString() {
        return "Ticket{" +
                "mID=" + mID +
                ", mMovieName='" + mMovieName + '\'' +
                ", mCinemaName='" + mCinemaName + '\'' +
                ", mTime='" + mTime + '\'' +
                ", mDate='" + mDate + '\'' +
                ", mSeat='" + mSeat + '\'' +
                ", mPrice='" + mPrice + '\'' +
                ", mUserUID='" + mUserUID + '\'' +
                ", mRoom=" + mRoom +
                ", mCinemaID=" + mCinemaID +
                ", mMovieID=" + mMovieID +
                '}';
    }
}
