package com.example.floyd.ludogame;

import android.content.Intent;
import android.os.Bundle;
import android.app.Activity;
import android.view.View;

public class GameOverGreen extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_game_over_green);
    }

    public void over(View view){
        Intent in= new Intent(GameOverGreen.this, PlayerSelect.class);
        startActivity(in);
    }

}
