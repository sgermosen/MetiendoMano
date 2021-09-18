package com.example.floyd.ludogame;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.animation.Animation;
import android.view.animation.AnimationUtils;
import android.widget.ImageView;

import java.util.Timer;
import java.util.TimerTask;

public class SplashScreen extends Activity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_splash_screen);

        ImageView image = (ImageView)findViewById(R.id.icon);
        Animation animation1 = AnimationUtils.loadAnimation(getApplicationContext(), R.anim.rotate);
        image.startAnimation(animation1);

        Timer timer = new Timer();
        timer.schedule(new TimerTask() {

            public void run() {

                Intent i = new Intent(SplashScreen.this, PlayerSelect.class);
                startActivity(i);

            }

        }, 1500);
    }

}
