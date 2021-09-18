package com.taneja.ajay.gstbilling;

import android.Manifest;
import android.content.Intent;
import android.content.SharedPreferences;
import android.content.pm.PackageManager;
import android.database.Cursor;
import android.graphics.Color;
import android.os.Build;
import android.os.Bundle;
import android.os.StrictMode;
import android.preference.PreferenceManager;
import android.support.design.widget.FloatingActionButton;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.LoaderManager;
import android.support.v4.content.CursorLoader;
import android.support.v4.content.Loader;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.support.v7.widget.Toolbar;
import android.view.View;
import android.view.Menu;
import android.view.MenuItem;

import com.taneja.ajay.gstbilling.data.GSTBillingContract;

public class BillsActivity extends AppCompatActivity implements LoaderManager.LoaderCallbacks<Cursor>, BillAdapter.BillItemClickListener {

    private RecyclerView unpaidRecyclerView;
    private BillAdapter adapter;
    private String billListStatus;
    private int billDividerColor;
    private String billSortOrder;

    private static final int BILL_LOADER_ID = 100;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_bills);
        Toolbar toolbar = (Toolbar) findViewById(R.id.toolbar);
        setSupportActionBar(toolbar);

        if(savedInstanceState != null){
            billListStatus = savedInstanceState.getString(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_STATUS);
        }else {
            billListStatus = GSTBillingContract.BILL_STATUS_UNPAID;
        }

        StrictMode.VmPolicy.Builder builder = new StrictMode.VmPolicy.Builder();
        StrictMode.setVmPolicy(builder.build());

        isStoragePermissionGranted();

        switch (billListStatus){
            case GSTBillingContract.BILL_STATUS_PAID:
                getSupportActionBar().setTitle(R.string.paid_bills_title);
                billDividerColor = Color.GREEN;
                billSortOrder = " DESC";
                break;
            case GSTBillingContract.BILL_STATUS_UNPAID:
                getSupportActionBar().setTitle(R.string.unpaid_bills_title);
                billDividerColor = Color.RED;
                billSortOrder = " ASC";
                break;
        }

        FloatingActionButton fab = (FloatingActionButton) findViewById(R.id.fab_unpaid);
        fab.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                startActivity(new Intent(BillsActivity.this, NewBillCustomerActivity.class));
            }
        });

        checkPasswordSetup();

        unpaidRecyclerView = (RecyclerView) findViewById(R.id.unpaid_recycler_view);
        unpaidRecyclerView.setLayoutManager(new LinearLayoutManager(this));
        unpaidRecyclerView.setHasFixedSize(true);
        adapter = new BillAdapter(this, this, billDividerColor);
        unpaidRecyclerView.setAdapter(adapter);

        getSupportLoaderManager().initLoader(BILL_LOADER_ID, null, this);

    }

    private void checkPasswordSetup() {
        SharedPreferences prefs = PreferenceManager.getDefaultSharedPreferences(this);
        if(prefs.getString(SetupPasswordActivity.SETUP_PASSWORD_KEY, null) == null){
            Intent intent = new Intent(this, SetupPasswordActivity.class);
            startActivity(intent);
            finish();
        }
    }

    public boolean isStoragePermissionGranted(){
        if(Build.VERSION.SDK_INT >= 23){
            if(checkSelfPermission(Manifest.permission.WRITE_EXTERNAL_STORAGE) == PackageManager.PERMISSION_GRANTED){
                return true;
            }else{
                ActivityCompat.requestPermissions(this, new String[]{Manifest.permission.WRITE_EXTERNAL_STORAGE}, 1);
                return false;
            }
        }else{
            return true;
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        // Inflate the menu; this adds items to the action bar if it is present.
        getMenuInflater().inflate(R.menu.menu_bills_list, menu);
        if(billListStatus.equals(GSTBillingContract.BILL_STATUS_PAID)){
            menu.findItem(R.id.action_swap_bills_list).setTitle(R.string.action_show_unpaid_bills);
        }
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle action bar item clicks here. The action bar will
        // automatically handle clicks on the Home/Up button, so long
        // as you specify a parent activity in AndroidManifest.xml.
        int id = item.getItemId();
        if(id == R.id.action_swap_bills_list){

            switch (billListStatus){
                case GSTBillingContract.BILL_STATUS_UNPAID:
                    billListStatus = GSTBillingContract.BILL_STATUS_PAID;
                    item.setTitle(getString(R.string.action_show_unpaid_bills));
                    getSupportActionBar().setTitle(getString(R.string.paid_bills_title));
                    billDividerColor = Color.GREEN;
                    billSortOrder = " DESC";
                    break;
                case GSTBillingContract.BILL_STATUS_PAID:
                    billListStatus = GSTBillingContract.BILL_STATUS_UNPAID;
                    item.setTitle(getString(R.string.action_show_paid_bills));
                    getSupportActionBar().setTitle(getString(R.string.unpaid_bills_title));
                    billDividerColor = Color.RED;
                    billSortOrder = " ASC";
                    break;
            }
            getSupportLoaderManager().restartLoader(BILL_LOADER_ID, null, this);

        }

        return super.onOptionsItemSelected(item);
    }

    @Override
    public Loader<Cursor> onCreateLoader(int id, Bundle args) {
        switch (id){
            case BILL_LOADER_ID:
                return new CursorLoader(
                        this,
                        GSTBillingContract.GSTBillingEntry.CONTENT_URI,
                        null,
                        GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_STATUS + "='" + billListStatus + "'",
                        null,
                        GSTBillingContract.GSTBillingEntry._ID + billSortOrder
                );
            default:
                throw new RuntimeException("Loader not implemented: " + id);
        }
    }

    @Override
    public void onLoadFinished(Loader<Cursor> loader, Cursor data) {
        adapter.swapCursor(data, billDividerColor);
    }

    @Override
    public void onLoaderReset(Loader<Cursor> loader) {
        adapter.swapCursor(null, Color.RED);
    }

    @Override
    public void onBillItemClick(String  clickedBillId, String customerName, String phoneNumber) {
        Intent detailIntent = new Intent(this, DetailActivity.class);

        detailIntent.putExtra(GSTBillingContract.GSTBillingEntry._ID, clickedBillId);
        detailIntent.putExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_STATUS, billListStatus);
        detailIntent.putExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_NAME, customerName);
        detailIntent.putExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_PHONE_NUMBER, phoneNumber);

        startActivity(detailIntent);
    }

    @Override
    protected void onSaveInstanceState(Bundle outState) {
        outState.putString(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_STATUS, billListStatus);

        super.onSaveInstanceState(outState);
    }
}
