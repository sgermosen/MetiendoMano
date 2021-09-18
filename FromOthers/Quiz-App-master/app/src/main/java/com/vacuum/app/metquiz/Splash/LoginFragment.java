package com.vacuum.app.metquiz.Splash;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.util.Log;
import android.view.KeyEvent;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.view.inputmethod.EditorInfo;
import android.widget.Button;
import android.widget.EditText;
import android.widget.LinearLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.vacuum.app.metquiz.MainActivity;
import com.vacuum.app.metquiz.Model.User;
import com.vacuum.app.metquiz.NavigationDrawer.PrivacyPolicyActivity;
import com.vacuum.app.metquiz.R;
import com.vacuum.app.metquiz.Utils.RegisterAPI;

import java.io.IOException;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

import static android.content.Context.MODE_PRIVATE;
import static com.vacuum.app.metquiz.Splash.SignupFragment.SIGNUP_FRAGMENT_TAG;
import static com.vacuum.app.metquiz.Splash.SplashScreen.MY_PREFS_NAME;

public class LoginFragment extends Fragment implements View.OnClickListener{

    final static String LOGIN_FRAGMENT_TAG ="LOGIN_FRAGMENT_TAG";

    String ROOT_URL ;
    private EditText login_cardnumber,login_password;
    TextView later,terms,terms2,register;
    Button login_btn;
    Context mContext;
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.login_fragment, container, false);
        terms =  view.findViewById(R.id.terms);
        terms2 =  view.findViewById(R.id.terms2);
        login_btn =  view.findViewById(R.id.login_btn);
        register =  view.findViewById(R.id.register);

        login_cardnumber =  view.findViewById(R.id.login_cardnumber);
        login_password =  view.findViewById(R.id.login_password);


        mContext = this.getActivity();

        login_password.setOnEditorActionListener(new EditText.OnEditorActionListener() {
            @Override
            public boolean onEditorAction(TextView v, int actionId, KeyEvent event) {
                if (actionId == EditorInfo.IME_ACTION_DONE) {
                   login();
                    return true;
                }
                return false;
            }
        });


        later =  view.findViewById(R.id.later);


        register.setOnClickListener(this);
        login_btn.setOnClickListener(this);
        terms2.setOnClickListener(this);
        later.setOnClickListener(this);

        return view;
    }

    @Override
    public void onClick(View view) {

        switch (view.getId())
        {
            case R.id.login_btn:
                login();
                break;

            case R.id.terms2:
                startActivity(new Intent(getActivity(), PrivacyPolicyActivity.class));
                getActivity().overridePendingTransition(R.anim.fade_in, R.anim.fade_out);
                break;
            case R.id.register:
                Fragment fragment = new SignupFragment();
                FragmentTransaction fragmentTransaction = getActivity().getSupportFragmentManager().beginTransaction();
                fragmentTransaction.setCustomAnimations(android.R.anim.fade_in,
                        android.R.anim.fade_out);
                fragmentTransaction.replace(R.id.frame, fragment, SIGNUP_FRAGMENT_TAG);
                fragmentTransaction.addToBackStack(null);
                fragmentTransaction.commitAllowingStateLoss();
                break;
            case R.id.later:
                skipSplash();
                break;
        }
    }

    private void login() {

        SharedPreferences prefs = mContext.getSharedPreferences(MY_PREFS_NAME, MODE_PRIVATE);
        ROOT_URL = prefs.getString("ip", "http://192.168.1.5/");

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(ROOT_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        RegisterAPI api = retrofit.create(RegisterAPI.class);
        api.loging_user(
                login_cardnumber.getText().toString(),
                login_password.getText().toString()
        ).enqueue(new Callback<User>() {
            @Override
            public void onResponse(Call<User> call, Response<User> response) {

                if(response.isSuccessful()) {
                    User u = response.body();

                    //System.out.println("====================================================");
                    //System.out.println(responsse);
                    Toast.makeText(mContext,"Hello, "+u.getFname(), Toast.LENGTH_SHORT).show();
                    //Log.e("TAG", responsse.toString());
                    SharedPreferences.Editor editor = mContext.getSharedPreferences(MY_PREFS_NAME, MODE_PRIVATE).edit();
                    editor.putString("student_id",u.getStudentId());
                    editor.putString("card_id",u.getCardId());
                    editor.putString("email",u.getEmail());
                    editor.putString("password",u.getPassword());
                    editor.putString("fname",u.getFname());
                    editor.putString("lname",u.getLname());
                    editor.putInt("grade_id",Integer.parseInt(u.getGradeId()));
                    editor.putInt("division_id",Integer.parseInt(u.getDivisionId()));
                    editor.apply();
                    skipSplash();
                }
            }

            @Override
            public void onFailure(Call<User> call, Throwable t) {
                Log.e("TAG", "Unable to submit post to API.");
            }
        });
    }
    private void skipSplash()
    {
        Intent i = new Intent(getActivity(), MainActivity.class);
        startActivity(i);
        getActivity().finish();
    }


}