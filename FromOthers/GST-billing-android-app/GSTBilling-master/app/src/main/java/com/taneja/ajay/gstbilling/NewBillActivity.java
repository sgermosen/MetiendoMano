package com.taneja.ajay.gstbilling;

import android.content.ContentValues;
import android.content.Intent;
import android.net.Uri;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.AdapterView;
import android.widget.ArrayAdapter;
import android.widget.Button;
import android.widget.EditText;
import android.widget.Spinner;
import android.widget.Toast;
import com.taneja.ajay.gstbilling.data.GSTBillingContract.GSTBillingCustomerEntry;
import com.taneja.ajay.gstbilling.data.GSTBillingContract.GSTBillingEntry;

import com.taneja.ajay.gstbilling.data.GSTBillingContract;

import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class NewBillActivity extends AppCompatActivity {

    public static boolean addingMoreItems = false;

    private Spinner taxSlabSpinner;
    private EditText itemDescription;
    private EditText finalPriceEt;
    private EditText quantityEt;
    private Button finishBtn;

    private int taxSlab;
    List<ContentValues> cvList;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_bill);

        taxSlabSpinner = (Spinner) findViewById(R.id.tax_slab_spinner);
        setupTaxSpinner();
        itemDescription = (EditText) findViewById(R.id.new_item_value);
        finalPriceEt = (EditText) findViewById(R.id.new_final_price_value);
        quantityEt = (EditText) findViewById(R.id.new_quantity_value);
        finishBtn = (Button) findViewById(R.id.finish_btn);
        finishBtn.setEnabled(false);

        if(getIntent().hasExtra(DetailActivity.EDITING_ITEM)){
            getSupportActionBar().setTitle(R.string.action_edit_bill_item_label);

            findViewById(R.id.add_to_bill_btn).setVisibility(View.GONE);
            finishBtn.setVisibility(View.GONE);

            final Intent editIntent = getIntent();
            final int idValue = editIntent.getIntExtra(GSTBillingCustomerEntry._ID, 0);
            String itemDescriptionValue = editIntent.getStringExtra(GSTBillingCustomerEntry.SECONDARY_COLUMN_ITEM_DESCRIPTION);
            float finalPriceValue = editIntent.getFloatExtra(GSTBillingCustomerEntry.SECONDARY_COLUMN_FINAL_PRICE, 0f);
            int quantityValue = editIntent.getIntExtra(GSTBillingCustomerEntry.SECONDARY_COLUMN_QUANTITY, 0);

            itemDescription.setText(itemDescriptionValue);
            finalPriceEt.setText(String.valueOf((int) finalPriceValue));
            quantityEt.setText(String.valueOf(quantityValue));

            Button doneEditingBtn = (Button) findViewById(R.id.done_edit_item_btn);
            doneEditingBtn.setVisibility(View.VISIBLE);
            doneEditingBtn.setOnClickListener(new View.OnClickListener() {
                @Override
                public void onClick(View v) {
                    if(itemDescription.getText().toString().length() == 0){
                        itemDescription.setText("NA");
                    }
                    if(finalPriceEt.getText().toString().length() == 0){
                        finalPriceEt.requestFocus();
                        Toast.makeText(NewBillActivity.this, getString(R.string.enter_final_price_error), Toast.LENGTH_SHORT).show();
                        return;
                    }
                    if(quantityEt.getText().toString().length() == 0){
                        quantityEt.setText("1");
                    }

                    ContentValues cv = new ContentValues();
                    cv.put(GSTBillingCustomerEntry.SECONDARY_COLUMN_ITEM_DESCRIPTION, itemDescription.getText().toString());
                    cv.put(GSTBillingCustomerEntry.SECONDARY_COLUMN_FINAL_PRICE, Integer.parseInt(finalPriceEt.getText().toString()));
                    cv.put(GSTBillingCustomerEntry.SECONDARY_COLUMN_QUANTITY, Integer.parseInt(quantityEt.getText().toString()));
                    cv.put(GSTBillingCustomerEntry.SECONDARY_COLUMN_TAX_SLAB, taxSlab);
                    getContentResolver().update(
                            GSTBillingEntry.CONTENT_URI.buildUpon().appendPath(editIntent.getStringExtra(DetailActivity.EDITING_ITEM)).appendPath(String.valueOf(idValue)).build(),
                            cv,
                            null,
                            null
                    );
                    ContentValues contentValues = new ContentValues();
                    contentValues.put(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_STATUS, GSTBillingContract.BILL_STATUS_UNPAID);
                    getContentResolver().update(
                            GSTBillingContract.GSTBillingEntry.CONTENT_URI.buildUpon().appendPath(editIntent.getStringExtra(DetailActivity.EDITING_ITEM)).build(),
                            contentValues,
                            GSTBillingContract.GSTBillingEntry._ID + "=" + editIntent.getStringExtra(DetailActivity.EDITING_ITEM),
                            null
                    );
                    DetailActivity.changeBillStatus();

                    finish();

                }
            });
        }else{
            cvList = new ArrayList<>();
        }

    }

    private void setupTaxSpinner() {
        ArrayAdapter<CharSequence> adapter = ArrayAdapter.createFromResource(this, R.array.tax_slab_list_array,
                android.R.layout.simple_spinner_item);
        adapter.setDropDownViewResource(android.R.layout.simple_spinner_dropdown_item);
        taxSlabSpinner.setAdapter(adapter);
        taxSlabSpinner.setOnItemSelectedListener(new AdapterView.OnItemSelectedListener() {
            @Override
            public void onItemSelected(AdapterView<?> parent, View view, int position, long id) {
                switch (position){
                    case 0:
                        taxSlab = 28;
                        break;
                    case 1:
                        taxSlab = 18;
                        break;
                    case 2:
                        taxSlab = 12;
                        break;
                    case 3:
                        taxSlab = 5;
                        break;
                }
            }

            @Override
            public void onNothingSelected(AdapterView<?> parent) {
                taxSlab = 28;
            }
        });
    }

    public void addToBill(View view){
        if(itemDescription.getText().toString().length() == 0){
            itemDescription.setText("NA");
        }
        if(finalPriceEt.getText().toString().length() == 0){
            finalPriceEt.requestFocus();
            Toast.makeText(this, getString(R.string.enter_final_price_error), Toast.LENGTH_SHORT).show();
            return;
        }
        if(quantityEt.getText().toString().length() == 0){
            quantityEt.setText("1");
        }

        ContentValues cv = new ContentValues();
        cv.put(GSTBillingCustomerEntry.SECONDARY_COLUMN_ITEM_DESCRIPTION, itemDescription.getText().toString());
        cv.put(GSTBillingCustomerEntry.SECONDARY_COLUMN_FINAL_PRICE, Integer.parseInt(finalPriceEt.getText().toString()));
        cv.put(GSTBillingCustomerEntry.SECONDARY_COLUMN_QUANTITY, Integer.parseInt(quantityEt.getText().toString()));
        cv.put(GSTBillingCustomerEntry.SECONDARY_COLUMN_TAX_SLAB, taxSlab);
        cvList.add(cv);
        Toast.makeText(this, getString(R.string.item_added_success), Toast.LENGTH_SHORT).show();

        itemDescription.setText("");
        finalPriceEt.setText("");
        quantityEt.setText("");

        finishBtn.setEnabled(true);

        itemDescription.requestFocus();
    }

    public void finishAddingItems(View view){

        // Check if any item is added in Selling price EditText before finishing the bill
        if(finalPriceEt.getText().toString().length() != 0){
            Toast.makeText(this, getString(R.string.add_item_to_bill_error), Toast.LENGTH_SHORT).show();
            return;
        }

        if(!getIntent().hasExtra(DetailActivity.ADDING_MORE_ITEMS)){
            // Inserting customer details in primary table
            Intent intent = getIntent();
            String customerName = intent.getStringExtra(NewBillCustomerActivity.ADD_CUSTOMER_NAME_KEY);
            String phoneNumber = intent.getStringExtra(NewBillCustomerActivity.ADD_CUSTOMER_PHONE_KEY);

            String billDate = new SimpleDateFormat("dd-MM-yyyy").format(new Date());
            String billStatus = GSTBillingContract.BILL_STATUS_UNPAID;

            ContentValues contentValues = new ContentValues();
            contentValues.put(GSTBillingEntry.PRIMARY_COLUMN_NAME, customerName);
            contentValues.put(GSTBillingEntry.PRIMARY_COLUMN_PHONE_NUMBER, phoneNumber);
            contentValues.put(GSTBillingEntry.PRIMARY_COLUMN_DATE, billDate);
            contentValues.put(GSTBillingEntry.PRIMARY_COLUMN_STATUS, billStatus);

            Uri idUri = getContentResolver().insert(GSTBillingEntry.CONTENT_URI, contentValues);

            // Inserting item details in secondary table
            String id = idUri.getLastPathSegment();
            getContentResolver().bulkInsert(GSTBillingContract.GSTBillingEntry.CONTENT_URI.buildUpon().appendPath(id).build(),
                    cvList.toArray(new ContentValues[cvList.size()]));

            // Opening detail activity
            Intent detailIntent = new Intent(this, DetailActivity.class);

            detailIntent.putExtra(GSTBillingEntry._ID, id);
            detailIntent.putExtra(GSTBillingEntry.PRIMARY_COLUMN_NAME, customerName);
            detailIntent.putExtra(GSTBillingEntry.PRIMARY_COLUMN_PHONE_NUMBER, phoneNumber);
            detailIntent.putExtra(GSTBillingEntry.PRIMARY_COLUMN_STATUS, GSTBillingContract.BILL_STATUS_UNPAID);

            startActivity(detailIntent);

            finish();
        }else {
            addingMoreItems = true;

            String id = getIntent().getStringExtra(GSTBillingEntry._ID);
            getContentResolver().bulkInsert(GSTBillingContract.GSTBillingEntry.CONTENT_URI.buildUpon().appendPath(id).build(),
                    cvList.toArray(new ContentValues[cvList.size()]));

            ContentValues contentValues = new ContentValues();
            contentValues.put(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_STATUS, GSTBillingContract.BILL_STATUS_UNPAID);
            getContentResolver().update(
                    GSTBillingContract.GSTBillingEntry.CONTENT_URI.buildUpon().appendPath(String.valueOf(id)).build(),
                    contentValues,
                    GSTBillingContract.GSTBillingEntry._ID + "=" + id,
                    null
            );
            DetailActivity.changeBillStatus();

            finish();
        }
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu_bill, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();
        if(id == R.id.action_discard){
            finish();
        }

        return super.onOptionsItemSelected(item);
    }
}
