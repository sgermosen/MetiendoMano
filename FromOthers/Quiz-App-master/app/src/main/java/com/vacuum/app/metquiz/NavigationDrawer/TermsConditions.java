package com.vacuum.app.metquiz.NavigationDrawer;

import android.graphics.Typeface;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.Toolbar;
import android.view.MenuItem;
import android.widget.TextView;

import com.vacuum.app.metquiz.R;


/**
 * Created by Home on 10/28/2017.
 */

public class TermsConditions  extends AppCompatActivity {


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.terms_conditions_layout);



        TextView tv =  findViewById(R.id.textView1);
        TextView tv2 =  findViewById(R.id.textView2);
        Toolbar mToolbar =  findViewById(R.id.toolbar);
        setSupportActionBar(mToolbar);
        getSupportActionBar().setDisplayHomeAsUpEnabled(true);
        getSupportActionBar().setDisplayShowHomeEnabled(true);
        getSupportActionBar().setTitle(R.string.Terms_Conditions_title);
        Typeface face = Typeface.createFromAsset(getAssets(),
                "fonts/brownregular.ttf");
        Typeface face2 = Typeface.createFromAsset(getAssets(),
                "fonts/airbnb.ttf");
        tv.setTypeface(face);
        tv2.setTypeface(face2);

    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        switch (item.getItemId()) {
            case android.R.id.home:
                // todo: goto back activity from here
                finish_Activity();
                return true;

            default:
                return super.onOptionsItemSelected(item);
        }
    }

    @Override
    public void onBackPressed() {
        finish_Activity();
    }
    private void finish_Activity() {
        //Intent intent = new Intent(TermsConditions.this, MainActivity.class);
        //startActivity(intent);
        finish();
        overridePendingTransition(R.anim.fade_in, R.anim.fade_out);

    }
}