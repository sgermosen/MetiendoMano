package com.vacuum.app.metquiz.Splash;

import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;

import com.vacuum.app.metquiz.MainActivity;
import com.vacuum.app.metquiz.Model.User;
import com.vacuum.app.metquiz.R;
import com.vacuum.app.metquiz.Utils.RegisterAPI;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

import static android.content.Context.MODE_PRIVATE;
import static com.vacuum.app.metquiz.Splash.SplashScreen.MY_PREFS_NAME;

public class SignupFragment extends Fragment implements View.OnClickListener{

    final static String SIGNUP_FRAGMENT_TAG = "SIGNUP_FRAGMENT_TAG";
    private EditText card_id,email,password,fname,lname;
    Button buttonRegister;
    String ROOT_URL;
    Context mContext;
    Spinner spinner_grad,spinner_division;
    static int grade_id,division_id;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.signup_layout, container, false);
        mContext = this.getActivity();
        card_id =  view.findViewById(R.id.card_id);
        email =  view.findViewById(R.id.email);
        password =  view.findViewById(R.id.password);
        fname =  view.findViewById(R.id.fname);
        lname =  view.findViewById(R.id.lname);
        buttonRegister =  view.findViewById(R.id.buttonRegister);
        spinner_grad= view.findViewById(R.id.spinner_grad);
        spinner_division= view.findViewById(R.id.spinner_division);



        spinner_setup();

        buttonRegister.setOnClickListener(this);
        return view;
    }

    private void spinner_setup() {
        List<String> grades = new ArrayList<>();
        grades.add("الفرقة الاولى");
        grades.add("الفرقة الثانية");
        grades.add("الفرقة الثالثه");
        grades.add("الفرقة الرابعة");


        ArrayAdapter<String> adapter = new ArrayAdapter<>(mContext, R.layout.spinner_item_layout, grades);
        spinner_grad.setAdapter(adapter);
        spinner_grad.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                grade_id = position+1;
            }
            @Override
            public void onNothingSelected(AdapterView<?> parentView) {
            }
        });
        //=======================================================
        //Devision
        final List<String> division_list = new ArrayList<>();
        division_list.add("علوم حاسب");
        division_list.add("شعبة نظم المعلومات الإدارية");
        division_list.add("محاسبة");
        division_list.add("إدارة الأعمال");

        ArrayAdapter<String> adapter_division = new ArrayAdapter<String>(mContext, R.layout.spinner_item_layout, division_list);
        spinner_division.setAdapter(adapter_division);
        spinner_division.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parentView, View selectedItemView, int position, long id) {
                division_id = position+1;
            }
            @Override
            public void onNothingSelected(AdapterView<?> parentView) {
            }
        });


    }


    private void insertUser() {
        SharedPreferences prefs = mContext.getSharedPreferences(MY_PREFS_NAME, MODE_PRIVATE);
        ROOT_URL = prefs.getString("ip", "http://192.168.1.5/");

        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(ROOT_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        RegisterAPI api = retrofit.create(RegisterAPI.class);
        api.insertUser(
                card_id.getText().toString(),
                email.getText().toString(),
                password.getText().toString(),
                fname.getText().toString(),
                lname.getText().toString(),
                grade_id,
                division_id
        ).enqueue(new Callback<User>() {
            @Override
            public void onResponse(Call<User> call, Response<User> response) {

                if(response.isSuccessful()) {
                    User u = response.body();
                        Toast.makeText(mContext,"Registered Successfully", Toast.LENGTH_SHORT).show();

                        SharedPreferences.Editor editor = mContext.getSharedPreferences(MY_PREFS_NAME, MODE_PRIVATE).edit();
                        editor.putString("student_id",u.getStudentId());
                        editor.putString("card_id",u.getCardId());
                        editor.putString("email",u.getEmail());
                        editor.putString("password",u.getPassword());
                        editor.putString("fname",u.getFname()+" " +u.getLname());
                        editor.putString("lname",u.getLname());
                        editor.putInt("grade_id",Integer.parseInt(u.getGradeId()));
                        editor.putInt("division_id",Integer.parseInt(u.getDivisionId()));
                        editor.apply();
                        skipSplash();
                }
            }

            @Override
            public void onFailure(Call<User> call, Throwable t) {
                Log.e("TAG", "insertUser():: Unable to submit post to API.");
            }
        });
    }

    private void validateFields() {
        if (card_id.getText().length() == 0) {
            card_id.setError("Empty Field");
        }else if (email.getText().length() == 0){
            email.setError("Empty Field");

        }else {
            insertUser();
        }
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.buttonRegister:
                validateFields();

                break;
        }
    }

    private void skipSplash()
    {
        Intent i = new Intent(getActivity(), MainActivity.class);
        startActivity(i);
        getActivity().finish();
    }

}