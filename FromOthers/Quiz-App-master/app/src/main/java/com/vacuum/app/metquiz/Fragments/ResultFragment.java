package com.vacuum.app.metquiz.Fragments;


import android.content.Context;
import android.os.Bundle;
import android.support.v4.app.Fragment;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ExpandableListAdapter;
import android.widget.ExpandableListView;
import android.widget.Toast;
import com.vacuum.app.metquiz.Adapters.ProductAdapter;
import com.vacuum.app.metquiz.MainActivity;
import com.vacuum.app.metquiz.Model.Product;
import com.vacuum.app.metquiz.R;
import com.vacuum.app.metquiz.Utils.Expandable.CustomExpandableListAdapter;
import com.vacuum.app.metquiz.Utils.Expandable.ExpandableListDataPump;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;


/**
 * Created by Home on 11/28/2017.
 */

public class ResultFragment extends Fragment {


    Context mContext;
    RecyclerView recyclerView;
    ExpandableListView expandableListView;
    ExpandableListAdapter expandableListAdapter;
    List<String> expandableListTitle;
    HashMap<String, List<String>> expandableListDetail;
    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container,
                             Bundle savedInstanceState) {
        // Inflate the layout for this fragment
        View view = inflater.inflate(R.layout.result_fragment, container, false);

        mContext = this.getActivity();
        recyclerView = (RecyclerView) view.findViewById(R.id.recycler_view);
        expandableListView = (ExpandableListView) view.findViewById(R.id.expandableListView);
        expandableListDetail = ExpandableListDataPump.getData();
        expandableListTitle = new ArrayList<String>(expandableListDetail.keySet());
        expandableListAdapter = new CustomExpandableListAdapter(mContext, expandableListTitle, expandableListDetail);
        expandableListView.setAdapter(expandableListAdapter);
        expandableListView.setOnGroupExpandListener(new ExpandableListView.OnGroupExpandListener() {

            @Override
            public void onGroupExpand(int groupPosition) {
                Toast.makeText(mContext,
                        expandableListTitle.get(groupPosition) + " List Expanded.",
                        Toast.LENGTH_SHORT).show();
            }
        });
        expandableListView.setOnGroupCollapseListener(new ExpandableListView.OnGroupCollapseListener() {

            @Override
            public void onGroupCollapse(int groupPosition) {
                Toast.makeText(mContext,
                        expandableListTitle.get(groupPosition) + " List Collapsed.",
                        Toast.LENGTH_SHORT).show();

            }
        });
        expandableListView.setOnChildClickListener(new ExpandableListView.OnChildClickListener() {
            @Override
            public boolean onChildClick(ExpandableListView parent, View v,
                                        int groupPosition, int childPosition, long id) {
                Toast.makeText(
                        mContext,
                        expandableListTitle.get(groupPosition)
                                + " -> "
                                + expandableListDetail.get(
                                expandableListTitle.get(groupPosition)).get(
                                childPosition), Toast.LENGTH_SHORT
                ).show();
                return false;
            }
        });
        // run the sentence in a new thread
        new Thread(new Runnable() {
            @Override
            public void run() {
                //List<Product> products = MainActivity.get().getDB().productDao().getAll();
                //boolean force = MainActivity.get().isForceUpdate();
                retrieveProducts();
            }
        }).start();
        return view;
    }
    private void retrieveProducts() {
        MainActivity.get().setForceUpdate(false);
        List<Product> list2 = new ArrayList<>();
        list2 = MainActivity.get().getDB().productDao().getAll();
        populateProducts(list2);
    }
    private void populateProducts(final List<Product> products) {
        getActivity().runOnUiThread(new Runnable() {
            @Override
            public void run() {
                Log.e("TAG","Set adabpter ================================");
                LinearLayoutManager llm = new LinearLayoutManager(mContext);
                recyclerView.setLayoutManager(llm);
                recyclerView.setAdapter(new ProductAdapter(products));
            }
        });
    }
}