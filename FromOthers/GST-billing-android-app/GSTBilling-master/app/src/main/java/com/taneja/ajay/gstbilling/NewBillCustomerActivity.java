package com.taneja.ajay.gstbilling;

import android.content.Intent;
import android.os.Bundle;
import android.support.v7.app.AppCompatActivity;
import android.view.Menu;
import android.view.MenuItem;
import android.view.View;
import android.widget.EditText;
import android.widget.Toast;

public class NewBillCustomerActivity extends AppCompatActivity {

    private EditText customerNameEt;
    private EditText phoneNumberEt;

    public static final String ADD_CUSTOMER_NAME_KEY = "customerName";
    public static final String ADD_CUSTOMER_PHONE_KEY = "phoneNumber";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_new_bill_customer);

        customerNameEt = (EditText) findViewById(R.id.customer_name_value);
        phoneNumberEt = (EditText) findViewById(R.id.phone_number_value);

    }

    public void addCustomer(View view){
        String customerName = customerNameEt.getText().toString();
        String phoneNumber = phoneNumberEt.getText().toString();
        if(customerName.length() == 0){
            Toast.makeText(this, getString(R.string.enter_customer_name_error), Toast.LENGTH_SHORT).show();
            return;
        }
        if(phoneNumber.length() > 0 && phoneNumber.length() < 10){
            Toast.makeText(this, getString(R.string.invalid_phone_number_error), Toast.LENGTH_SHORT).show();
            return;
        }else if(phoneNumber.length() == 0){
            phoneNumber = "NA";
        }


        Intent intent = new Intent(this, NewBillActivity.class);
        intent.putExtra(ADD_CUSTOMER_NAME_KEY, customerName);
        intent.putExtra(ADD_CUSTOMER_PHONE_KEY, phoneNumber);
        startActivity(intent);

        finish();
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
