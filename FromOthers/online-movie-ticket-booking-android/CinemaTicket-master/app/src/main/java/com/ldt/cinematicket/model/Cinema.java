package com.ldt.cinematicket.model;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class Cinema {
    @SerializedName("address")
    private String Address;
    @SerializedName("hotline")
    private String Hotline;
    @SerializedName("id")
    private int ID;
    @SerializedName("imageUrl")
    private String ImageUrl;
    @SerializedName("name")
    private String Name;
    @SerializedName("movies")
    private ArrayList<Integer> Movies;
    @SerializedName("showTimes")
    private ArrayList<Integer> ShowTimes;

    public String getAddress() {
        return Address;
    }

    public void setAddress(String Address) {
        this.Address = Address;
    }

    public String getHotline() {
        return Hotline;
    }

    public void setHotline(String Hotline) {
        this.Hotline = Hotline;
    }

    public int getId() {
        return ID;
    }

    public void setId(int ID) {
        this.ID = ID;
    }

    public String getImageUrl() {
        return ImageUrl;
    }

    public void setImageUrl(String ImageUrl) {
        this.ImageUrl = ImageUrl;
    }

    public String getName() {
        return Name;
    }

    public void setName(String Name) {
        this.Name = Name;
    }

    public ArrayList<Integer> getMovies() {
        return Movies;
    }

    public void setMovies(ArrayList<Integer> Movies) {
        this.Movies = Movies;
    }

    public ArrayList<Integer> getShowTimes() {
        return ShowTimes;
    }

    public void setShowTimes(ArrayList<Integer> ShowTimes) {
        this.ShowTimes = ShowTimes;
    }
}
