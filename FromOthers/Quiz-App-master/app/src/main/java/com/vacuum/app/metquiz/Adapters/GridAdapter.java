package com.vacuum.app.metquiz.Adapters;

import android.Manifest;
import android.content.Context;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.graphics.Typeface;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentActivity;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.content.ContextCompat;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.vacuum.app.metquiz.Fragments.BarcodeFragment;
import com.vacuum.app.metquiz.Fragments.QuestionsFragment;
import com.vacuum.app.metquiz.Fragments.ResultFragment;
import com.vacuum.app.metquiz.Model.Example;
import com.vacuum.app.metquiz.Model.Item;
import com.vacuum.app.metquiz.Model.QuestionModel;
import com.vacuum.app.metquiz.NavigationDrawer.SettingsFragment;
import com.vacuum.app.metquiz.R;
import com.vacuum.app.metquiz.Utils.RegisterAPI;

import java.util.ArrayList;
import java.util.List;


import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;
import retrofit2.Retrofit;
import retrofit2.converter.gson.GsonConverterFactory;

import static android.content.Context.MODE_PRIVATE;
import static com.vacuum.app.metquiz.MainActivity.TAG_Barcodescanner;
import static com.vacuum.app.metquiz.MainActivity.TAG_HOME;
import static com.vacuum.app.metquiz.MainActivity.TAG_QUESTIONS;
import static com.vacuum.app.metquiz.MainActivity.TAG_SETTINGS;
import static com.vacuum.app.metquiz.Splash.SplashScreen.MY_PREFS_NAME;

/**
 * Created by Home on 2017-08-29.
 */

public class GridAdapter extends RecyclerView.Adapter<GridAdapter.MyViewHolder> {

    String ROOT_URL ;

        private List<Item> items;
        private Context mContext;
        public class MyViewHolder extends RecyclerView.ViewHolder {
        public TextView title;
        public ImageView image;
        public Button circle;

        public MyViewHolder(View view) {
            super(view);
            title = (TextView) view.findViewById(R.id.title);
            image = (ImageView) view.findViewById(R.id.image);
            circle = (Button) view.findViewById(R.id.circle);

        }

    }


    public GridAdapter(List<Item> items, Context mContext) {
        this.items = items;
        this.mContext = mContext;
        SharedPreferences prefs = mContext.getSharedPreferences(MY_PREFS_NAME, MODE_PRIVATE);
        String restoredText = prefs.getString("ip", "http://192.168.1.3");
        ROOT_URL = restoredText;

    }

    @Override
    public GridAdapter.MyViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View itemView = LayoutInflater.from(parent.getContext())
                .inflate(R.layout.grid_layout, parent, false);

        return new GridAdapter.MyViewHolder(itemView);
    }





    @Override
    public void onBindViewHolder(GridAdapter.MyViewHolder holder, final int position) {
        Typeface Gess_two = Typeface.createFromAsset(mContext.getAssets(),
                "fonts/airbnb.ttf");

        holder.title.setText(items.get(position).getTitle());
        holder.image.setImageResource(items.get(position).getImage());

        //=========================================
        holder.title.setTypeface(Gess_two);
        //=========================================

        //holder.circle.setVisibility(View.VISIBLE);

        holder.itemView.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                if (position == 0){
                    ResultFragment questions = new ResultFragment();
                    loadfragment(questions,TAG_QUESTIONS);

                }else if(position == 1){
                    check_ip();

                }else if(position == 2){
                    Toast.makeText(mContext, "this is number " +position, Toast.LENGTH_SHORT).show();
                }else if(position == 4){

                    if (ContextCompat.checkSelfPermission(mContext, Manifest.permission.CAMERA)
                            == PackageManager.PERMISSION_DENIED)
                    {
                        FragmentActivity activity = (FragmentActivity) mContext;
                        ActivityCompat.requestPermissions(activity, new String[] {Manifest.permission.CAMERA}, 100);
                    }else {
                        BarcodeFragment barcodeFragment = new BarcodeFragment();
                        loadfragment(barcodeFragment,TAG_Barcodescanner);
                    }

                }
                else {
                    SettingsFragment settingsFragment = new SettingsFragment();
                    loadfragment(settingsFragment,TAG_SETTINGS);
                }

            }
        });

    }

    private void check_ip() {
        Retrofit retrofit = new Retrofit.Builder()
                .baseUrl(ROOT_URL)
                .addConverterFactory(GsonConverterFactory.create())
                .build();

        RegisterAPI api = retrofit.create(RegisterAPI.class);
        api.getQuestions().enqueue(new Callback<Example>() {
            @Override
            public void onResponse(Call<Example> call, Response<Example> response) {

                QuestionsFragment questions = new QuestionsFragment();
                loadfragment(questions,TAG_QUESTIONS);
            }

            @Override
            public void onFailure(Call<Example> call, Throwable t) {
                Toast.makeText(mContext, "Add Correct ip", Toast.LENGTH_SHORT).show();
                Log.e("TAG",t.toString());
            }
        });
    }


    @Override
    public int getItemCount() {
        return items.size();
    }

    private void loadfragment(Fragment Fragment,String TAG) {
        FragmentActivity activity = (FragmentActivity) mContext;
        FragmentTransaction fragmentTransaction = activity.getSupportFragmentManager().beginTransaction();
        fragmentTransaction.setCustomAnimations(android.R.anim.fade_in,
                android.R.anim.fade_out);
        fragmentTransaction.replace(R.id.container, Fragment, TAG);
        fragmentTransaction.addToBackStack(TAG);
        fragmentTransaction.commitAllowingStateLoss();
    }


}
