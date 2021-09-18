package com.vacuum.app.metquiz.Fragments;

import android.content.Context;
import android.os.AsyncTask;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.GridLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.daimajia.slider.library.SliderLayout;
import com.daimajia.slider.library.SliderTypes.BaseSliderView;
import com.daimajia.slider.library.SliderTypes.TextSliderView;
import com.vacuum.app.metquiz.Adapters.GridAdapter;
import com.vacuum.app.metquiz.Model.Item;
import com.vacuum.app.metquiz.R;
import com.vacuum.app.metquiz.Utils.InternetStatus;

import org.jsoup.Jsoup;
import org.jsoup.nodes.Document;
import org.jsoup.nodes.Element;
import org.jsoup.select.Elements;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;

/**
 * Created by Home on 2017-08-29.
 */

public class HomeFragment extends Fragment {
    RecyclerView recyclerView;
    GridAdapter countryAdapter;
    SliderLayout mDemoSlider;
    HashMap<String,String> url_maps;
    Context mContext;
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.fragment_one, container, false);
        recyclerView = (RecyclerView) view.findViewById(R.id.recycler_view_one);
        mDemoSlider = (SliderLayout)view.findViewById(R.id.slider);
        mContext = this.getActivity();


        //================================================

        if (new InternetStatus(mContext).isNetworkConnected())
        {
            new LongOperation().execute();
            //Toast.makeText(mContext, "Internet", Toast.LENGTH_SHORT).show();
        }else {
            hashmap();
            //Toast.makeText(mContext, "no connection", Toast.LENGTH_SHORT).show();
        }


        prepareMovieData2();
        return view;
    }




    private void prepareMovieData2() {
        List<Item> items = new ArrayList<>();
        items.add(new Item("Results",  R.drawable.if_heart_favourite_favorite_love_2203510));
        items.add(new Item("Exam",  R.drawable.ic_notifications_black_24dp));
        items.add(new Item("Notification", R.drawable.if_explore_326636));
        items.add(new Item("Settings", R.drawable.ic_settings_black_24dp));
        items.add(new Item("Barcode scanner", R.drawable.if_qrcode_1608801));




        recyclerView.setHasFixedSize(true);
        countryAdapter = new GridAdapter(items,this.getActivity());
        RecyclerView.LayoutManager mLayoutManager = new GridLayoutManager(mContext,2);
        recyclerView.setLayoutManager(mLayoutManager);
        recyclerView.setAdapter(countryAdapter);
    }


    private class LongOperation extends AsyncTask<String, Void, String> {

        List<String> images = new ArrayList<>();
        List<String> titles = new ArrayList<>();

        @Override
        protected String doInBackground(String... params) {

            System.out.println("title : ==============================================");
            System.out.println("title : //////////////////////////////////////////////" );

            Document doc;
            try {

                // need http protocol
                doc = Jsoup.connect("http://misr.academy/").get();


                // get page title

                // get all links
                Elements links = doc.select("div.slider");
                Elements img = links.select("img");
                Elements h1 = links.select("h1");

                for (Element el : img) {
                    String src = el.absUrl("src");
                    //System.out.println("Image Found!");
                    //System.out.println("src attribute is : "+src);
                    images.add(src);
                }
                int index = 0;
                for (Element el : h1) {
                    System.out.println("text : "+el.text());
                    System.out.println("========================================");
                    String NewString = el.text().replaceAll("\\+", " ");
                    titles.add(NewString+ "\n" +index++ );

                }

            } catch (IOException e) {
                e.printStackTrace();
            }

            return null;
        }

        @Override
        protected void onPostExecute(String result) {

            url_maps = new HashMap<String, String>();
            for (int i=0;i<images.size();i++)
            {
                url_maps.put(titles.get(i), images.get(i));
            }
            makeSlider();
        }
    }


    private void makeSlider() {


        for(String name : url_maps.keySet()){
            TextSliderView textSliderView = new TextSliderView(this.getActivity());
            // initialize a SliderLayout
            textSliderView
                    .description(name)
                    .image(url_maps.get(name))
                    .setScaleType(BaseSliderView.ScaleType.Fit);

            //add your extra information
            textSliderView.bundle(new Bundle());
            textSliderView.getBundle()
                    .putString("extra",name);

            mDemoSlider.addSlider(textSliderView);
        }
    }
    private void hashmap() {
        HashMap<String,Integer> drawable_maps;

        drawable_maps = new HashMap<>();
        drawable_maps.put("اكاديمية مصر للهندسة و التكنولوجيا بالمنصورة"+"\n", R.drawable.met1);
        drawable_maps.put("جانب من فعاليات زيارة وفد معهد مصر العالي للتجارة والحاسبات بالمنصورة لشركة IBM Egypt"+"\n", R.drawable.met2);
        drawable_maps.put("فعاليات معرض الكتاب بجامعة المنصورة2"+"\n", R.drawable.met3);
        drawable_maps.put("جانب من فعاليات زيارة وفد معهد مصر العالي للتجارة والحاسبات بالمنصورة لشركة IBM Egypt 2"+"\n", R.drawable.met4);
        drawable_maps.put("فعاليات معرض الكتاب بجامعة المنصورة"+"\n", R.drawable.met5);
        drawable_maps.put("جانب من فعاليات زيارة وفد معهد مصر العالي للتجارة والحاسبات بالمنصورة لشركة IBM Egypt 3"+"\n", R.drawable.met6);

        for(String name : drawable_maps.keySet()){
            TextSliderView textSliderView = new TextSliderView(this.getActivity());
            // initialize a SliderLayout
            textSliderView
                    .description(name)
                    .image(drawable_maps.get(name))
                    .setScaleType(BaseSliderView.ScaleType.Fit);

            //add your extra information
            textSliderView.bundle(new Bundle());
            textSliderView.getBundle()
                    .putString("extra",name);

            mDemoSlider.addSlider(textSliderView);
        }
    }



}
