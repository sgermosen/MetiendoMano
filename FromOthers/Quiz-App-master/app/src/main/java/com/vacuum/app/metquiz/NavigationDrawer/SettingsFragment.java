package com.vacuum.app.metquiz.NavigationDrawer;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.os.Handler;
import android.os.Vibrator;
import android.support.v4.app.Fragment;
import android.support.v7.app.AppCompatActivity;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.EditorInfo;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.CompoundButton;
import android.widget.EditText;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.kyleduo.switchbutton.SwitchButton;
import com.vacuum.app.metquiz.MainActivity;
import com.vacuum.app.metquiz.R;

import static android.content.Context.MODE_PRIVATE;
import static com.vacuum.app.metquiz.MainActivity.TAG_SETTINGS;
import static com.vacuum.app.metquiz.MainActivity.activityTitles;
import static com.vacuum.app.metquiz.MainActivity.navItemIndex;
import static com.vacuum.app.metquiz.Splash.SplashScreen.MY_PREFS_NAME;


/**
 * Created by Home on 10/19/2017.
 */

public class SettingsFragment extends Fragment implements CompoundButton.OnCheckedChangeListener,View.OnClickListener {

    Button clearcach,button_ip;
    SharedPreferences.Editor editor;
    Context mContext;
    SwitchButton sb1,sb3,sb4,sb5,sb6,sb7,sb8;
    Vibrator vibe;
    RelativeLayout terms,policy,Themes,Language;
    EditText edittext_ip;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        mContext = this.getActivity();
        final View view = inflater.inflate(R.layout.settings_fragment, container, false);
        mContext = this.getActivity();


        vibe = (Vibrator)mContext.getSystemService(Context.VIBRATOR_SERVICE);
        clearcach =  view.findViewById(R.id.clearcach);
        terms = view.findViewById(R.id.terms);
        policy = view.findViewById(R.id.policy);
        Themes = view.findViewById(R.id.Themes);
        Language = view.findViewById(R.id.Language);
        edittext_ip = view.findViewById(R.id.edittext_ip);
        button_ip = view.findViewById(R.id.button_ip);


//====================================================================
        MainActivity.CURRENT_TAG =TAG_SETTINGS;
        navItemIndex = 4;
        MainActivity.navigationView.getMenu().getItem(4).setChecked(true);
        ((AppCompatActivity)getActivity()).getSupportActionBar().setTitle(activityTitles[4]);
//====================================================================


        sb1=  view.findViewById(R.id.sb_default_measure1);
        sb3 =  view.findViewById(R.id.sb_default_measure3);
        sb4 =  view.findViewById(R.id.sb_default_measure4);
        sb5 =  view.findViewById(R.id.sb_default_measure5);
        sb6 =  view.findViewById(R.id.sb_default_measure6);
        sb7 =  view.findViewById(R.id.sb_default_measure7);
        sb8 =  view.findViewById(R.id.sb_default_measure8);





        editor = mContext.getSharedPreferences(MY_PREFS_NAME, MODE_PRIVATE).edit();
        clearcach.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                editor.clear().commit();
                Toast.makeText(mContext,"Cache Cleared",Toast.LENGTH_SHORT).show();

            }
        });


        edittext_ip.setOnEditorActionListener(new EditText.OnEditorActionListener() {
            @Override
            public boolean onEditorAction(TextView v, int actionId, KeyEvent event) {
                if (actionId == EditorInfo.IME_ACTION_DONE) {
                    Done(view);
                    return true;
                }
                return false;
            }
        });
        sb1.setOnCheckedChangeListener(this);
        sb3.setOnCheckedChangeListener(this);
        sb4.setOnCheckedChangeListener(this);
        sb5.setOnCheckedChangeListener(this);
        sb6.setOnCheckedChangeListener(this);
        sb7.setOnCheckedChangeListener(this);
        sb8.setOnCheckedChangeListener(this);

        terms.setOnClickListener(this);
        policy.setOnClickListener(this);
        Themes.setOnClickListener(this);
        Language.setOnClickListener(this);
        button_ip.setOnClickListener(this);
        return view;
    }



    @Override
    public void onCheckedChanged(final CompoundButton compoundButton, boolean b) {
        new Handler().postDelayed(new Runnable() {
            @Override
            public void run() {
                if(compoundButton.isChecked()){
                    compoundButton.setChecked(false);
                    Toast.makeText(mContext, "Go Preimum first", Toast.LENGTH_SHORT).show();
                    vibe.vibrate(100);
                }
            }
        }, 500);

    }

    @Override
    public void onClick(View view) {
        if(view == policy){
            startActivity(new Intent(mContext, PrivacyPolicyActivity.class));
            getActivity().overridePendingTransition(R.anim.fade_in, R.anim.fade_out);
        }else if(view==Themes||view==Language){
            Toast.makeText(mContext, "Go Preimum first", Toast.LENGTH_SHORT).show();
            vibe.vibrate(100);
        }else if(view == button_ip){
            Done(view);
        }   else {
            startActivity(new Intent(mContext, TermsConditions.class));
            getActivity().overridePendingTransition(R.anim.fade_in, R.anim.fade_out);
        }
    }

    private void Done(View view) {
        editor.putString("ip", "http://192.168."+ edittext_ip.getText().toString()+ "/" );
        editor.apply();
        View view2 = getActivity().getCurrentFocus();
        if (view2 != null) {
            InputMethodManager imm = (InputMethodManager)mContext.getSystemService(Context.INPUT_METHOD_SERVICE);
            imm.hideSoftInputFromWindow(view.getWindowToken(), 0);
        }
        Toast.makeText(mContext, "Successfully Changed", Toast.LENGTH_SHORT).show();
    }
}