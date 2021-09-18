package com.ldt.cinematicket.data;

import android.content.Context;
import android.content.SharedPreferences;
import android.widget.Toast;

public class MyPrefs {
    SharedPreferences myPrefs;

    public MyPrefs(Context context){
        myPrefs = context.getSharedPreferences("data",Context.MODE_PRIVATE);
    }

    public void setIsSignIn(boolean state){
        SharedPreferences.Editor editor = myPrefs.edit();
        editor.putBoolean("SigninState",state);
        editor.apply();
    }

    public boolean getIsSignIn(){ return myPrefs.getBoolean("SigninState",false); }

    public void setIsAdmin(boolean state){
        SharedPreferences.Editor editor = myPrefs.edit();
        editor.putBoolean("IsAdmin",state);
        editor.apply();
    }

    public boolean getIsAdmin(){ return myPrefs.getBoolean("IsAdmin",false); }

    public void setIsRememberMe(boolean state){
        SharedPreferences.Editor editor = myPrefs.edit();
        editor.putBoolean("IsRemember",state);
        editor.apply();
    }

    public boolean getIsRememberMe(){ return myPrefs.getBoolean("IsRemember",false); }

    public void setAccount(String[] account){
        SharedPreferences.Editor editor = myPrefs.edit();
        editor.putString("Email",account[0]);
        editor.putString("Password",account[1]);
        editor.apply();
    }

    public String[] getAccount(){
        String[] account = new String[2];
        account[0] = myPrefs.getString("Email",null);
        account[1] = myPrefs.getString("Password",null);
        return account;
    }
}
