package com.ldt.cinematicket.model;

import android.support.annotation.NonNull;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class Movie {
    @SerializedName("cast")
    private String cast;
    @SerializedName("description")
    private String description;
    @SerializedName("director")
    private String director;
    @SerializedName("duration")
    private int duration;
    @SerializedName("genre")
    private String genre;
    @SerializedName("id")
    private int id;
    @SerializedName("imageUrl")
    private String imageUrl;
    @SerializedName("openingDay")
    private String openingDay;
    @SerializedName("reviews")
    private ArrayList<String> reviews;
    @SerializedName("status")
    private String status;
    @SerializedName("title")
    private String title;
    @SerializedName("trailerYoutube")
    private String trailerYoutube;
    @SerializedName("type")
    private ArrayList<String> type;
    @SerializedName("rate")
    private double rate;


    public String getCast() {
        return cast;
    }

    public void setCast(String cast) {
        this.cast = cast;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getDirector() {
        return director;
    }

    public void setDirector(String director) {
        this.director = director;
    }

    public int getDuration() {
        return duration;
    }

    public void setDuration(int duration) {
        this.duration = duration;
    }

    public String getGenre() {
        return genre;
    }

    public void setGenre(String genre) {
        this.genre = genre;
    }

    public int getId() {
        return id;
    }

    public void setId(int id) {
        this.id = id;
    }

    public String getImageUrl() {
        return imageUrl;
    }

    public void setImageUrl(String imageUrl) {
        this.imageUrl = imageUrl;
    }

    public String getOpeningDay() {
        return openingDay;
    }

    public void setOpeningDay(String openingDay) {
        this.openingDay = openingDay;
    }

    public ArrayList<String> getReviews() {
        return reviews;
    }

    public void setReviews(ArrayList<String> reviews) {
        this.reviews = reviews;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getTitle() {
        return title;
    }

    public void setTitle(String title) {
        this.title = title;
    }

    public String getTrailerYoutube() {
        return trailerYoutube;
    }

    public void setTrailerYoutube(String trailerYoutube) {
        this.trailerYoutube = trailerYoutube;
    }

    public ArrayList<String> getType() {
        return type;
    }

    public void setType(ArrayList<String> type) {
        this.type = type;
    }

    @NonNull
    @Override
    public String toString() {
        return "Movie{" +
                "cast='" + cast + '\'' +
                ", description='" + description + '\'' +
                ", director='" + director + '\'' +
                ", duration=" + duration +
                ", genre='" + genre + '\'' +
                ", id=" + id +
                ", imageUrl='" + imageUrl + '\'' +
                ", openingDay='" + openingDay + '\'' +
                ", reviews=" + reviews +
                ", status='" + status + '\'' +
                ", title='" + title + '\'' +
                ", trailerYoutube='" + trailerYoutube + '\'' +
                ", type=" + type +
                '}';
    }

    public double getRate() {
        return rate;
    }

    public void setRate(double rate) {
        this.rate = rate;
    }
}
