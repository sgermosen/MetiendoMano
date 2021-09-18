package com.ldt.cinematicket.data;

public class DataFilm {
    int Image;
    String Name;
    String Director;
    String Actors;
    String DimenType;
    String ShowingType;
    Double Rating;

    public DataFilm(int image, String name, String dimenType, String showingType, String director, String actors, Double rating){
        Image = image;
        Name = name;
        Director = director;
        Actors = actors;
        DimenType = dimenType;
        ShowingType = showingType;
        Rating = rating;
    }

    public int getImage() {
        return Image;
    }

    public void setImage(int image) {
        Image = image;
    }

    public String getName() {
        return Name;
    }

    public void setName(String name) {
        Name = name;
    }

    public String getDirector() {
        return Director;
    }

    public void setDirector(String director) {
        Director = director;
    }

    public String getActors() {
        return Actors;
    }

    public void setActors(String actors) {
        Actors = actors;
    }

    public String getDimenType() {
        return DimenType;
    }

    public void setDimenType(String dimenType) {
        DimenType = dimenType;
    }

    public String getShowingType() {
        return ShowingType;
    }

    public void setShowingType(String showingType) {
        ShowingType = showingType;
    }

    public Double getRating() {
        return Rating;
    }

    public void setRating(Double rating) {
        Rating = rating;
    }
}
