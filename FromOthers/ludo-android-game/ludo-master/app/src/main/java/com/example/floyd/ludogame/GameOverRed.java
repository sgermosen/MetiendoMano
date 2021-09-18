package com.example.floyd.ludogame;

import android.content.Intent;
import android.os.Bundle;
import android.app.Activity;
import android.view.View;

public class GameOverRed extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_game_over_red);
    }

    public void over(View view){
        Intent in= new Intent(GameOverRed.this, PlayerSelect.class);
        startActivity(in);
    }

}
