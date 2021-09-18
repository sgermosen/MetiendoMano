package com.ldt.cinematicket.model;

import com.google.gson.annotations.SerializedName;

import java.util.ArrayList;

public class UserInfo {
    @SerializedName("userType")
    private String userType;
    @SerializedName("id")
    private String id;
    @SerializedName("fullName")
    private String fullName;
    @SerializedName("avaUrl")
    private String avaUrl;
    @SerializedName("idTicket")
    private ArrayList<Integer> idTicket;
    @SerializedName("balance")
    private int balance;
    @SerializedName("email")
    private String email;
    @SerializedName("birthday")
    private String birthDay;
    @SerializedName("gender")
    private String gender;
    @SerializedName("phoneNumber")
    private String phoneNumber;
    @SerializedName("address")
    private String address;

    public String getAvaUrl() {
        return avaUrl;
    }

    public void setAvaUrl(String avaUrl) {
        this.avaUrl = avaUrl;
    }

    public ArrayList<Integer> getIdTicket() {
        return idTicket;
    }

    public void setIdTicket(ArrayList<Integer> idTicket) {
        this.idTicket = idTicket;
    }

    public int getBalance() {
        return balance;
    }

    public void setBalance(int balance) {
        this.balance = balance;
    }

    public String getUserType() {
        return userType;
    }

    public void setUserType(String userType) {
        this.userType = userType;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getFullName() {
        return fullName;
    }

    public void setFullName(String fullName) {
        this.fullName = fullName;
    }

    public String getEmail() {
        return email;
    }

    public void setEmail(String email) {
        this.email = email;
    }

    public String getBirthDay() {
        return birthDay;
    }

    public void setBirthDay(String birthDay) {
        this.birthDay = birthDay;
    }

    public String getGender() {
        return gender;
    }

    public void setGender(String gender) {
        this.gender = gender;
    }

    public String getPhoneNumber() {
        return phoneNumber;
    }

    public void setPhoneNumber(String phoneNumber) {
        this.phoneNumber = phoneNumber;
    }

    public String getAddress() {
        return address;
    }

    public void setAddress(String address) {
        this.address = address;
    }
}
