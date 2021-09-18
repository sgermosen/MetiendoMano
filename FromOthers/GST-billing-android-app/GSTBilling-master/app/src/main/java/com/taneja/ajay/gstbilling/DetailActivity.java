package com.taneja.ajay.gstbilling;

import android.content.ContentValues;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.database.Cursor;
import android.graphics.Color;
import android.graphics.drawable.ColorDrawable;
import android.net.Uri;
import android.os.Bundle;
import android.os.PersistableBundle;
import android.preference.PreferenceManager;
import android.support.v4.app.LoaderManager;
import android.support.v4.content.CursorLoader;
import android.support.v4.content.Loader;
import android.support.v7.app.ActionBar;
import android.support.v7.app.AlertDialog;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.LinearLayoutManager;
import android.support.v7.widget.RecyclerView;
import android.text.InputType;
import android.text.method.PasswordTransformationMethod;
import android.view.Menu;
import android.view.MenuItem;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.taneja.ajay.gstbilling.data.GSTBillingContract;
import com.taneja.ajay.gstbilling.utils.NumberToWord;

public class DetailActivity extends AppCompatActivity implements LoaderManager.LoaderCallbacks<Cursor> {

    private static final int DETAIL_LOADER_ID = 44;

    private static final int ACTION_MARK_AS_PAID_ID = 400;
    private static final int ACTION_DELETE_BILL_ID = 401;
    private static final int ACTION_ADD_MORE_ITEMS_ID = 402;

    static final String ADDING_MORE_ITEMS = "adding-more-items-to-bill";
    static final String EDITING_ITEM = "editing-existing-item";

    private RecyclerView detailRecyclerView;
    private DetailAdapter adapter;
    private static String billId;
    private static String billStatus;
    private String phoneNumber;
    private String customerName;

    private static TextView totalTaxableValueTv;
    private static TextView totalCgstTv;
    private static TextView totalSgstTv;
    private static TextView totalGstTv;
    private static TextView totalAmountTv;
    private static TextView totalAmountInWordsTv;

    private static String inr;

    private int itemCount;
    public static final String ITEM_COUNT_KEY = "items-count-in-bill";

    private static ActionBar detailActionBar;

    private static Intent getDetailIntent;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_detail);

        detailActionBar = getSupportActionBar();

        getDetailIntent = getIntent();
        billId = getDetailIntent.getStringExtra(GSTBillingContract.GSTBillingEntry._ID);


        if(getDetailIntent.hasExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_STATUS)){
            billStatus = getDetailIntent.getStringExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_STATUS);
        }

        if(billStatus.equals(GSTBillingContract.BILL_STATUS_PAID)){
            detailActionBar.setBackgroundDrawable(new ColorDrawable(Color.parseColor("#00C853")));
        }else {
            detailActionBar.setBackgroundDrawable(new ColorDrawable(Color.RED));
        }

        if(getDetailIntent.hasExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_NAME)){
            customerName = getDetailIntent.getStringExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_NAME);
            detailActionBar.setTitle(customerName);
        }

        if(getDetailIntent.hasExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_PHONE_NUMBER)){
            phoneNumber = getDetailIntent.getStringExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_PHONE_NUMBER);
        }

        detailRecyclerView = (RecyclerView) findViewById(R.id.detail_recycler_view);
        detailRecyclerView.setLayoutManager(new LinearLayoutManager(this));
        detailRecyclerView.setHasFixedSize(true);
        adapter = new DetailAdapter(this);
        detailRecyclerView.setAdapter(adapter);

        totalTaxableValueTv = (TextView) findViewById(R.id.total_amount_before_tax_value);
        totalCgstTv = (TextView) findViewById(R.id.total_cgst_value);
        totalSgstTv = (TextView) findViewById(R.id.total_sgst_value);
        totalGstTv = (TextView) findViewById(R.id.total_gst_value);
        totalAmountTv = (TextView) findViewById(R.id.total_amount_after_tax_value);
        totalAmountInWordsTv = (TextView) findViewById(R.id.total_amount_in_words_value);

        inr = getString(R.string.inr) + " ";

        getSupportLoaderManager().initLoader(DETAIL_LOADER_ID, null, this);
    }

    public static void printTotalDetails(float totalTaxableValue, float totalSingleGst, float totalAmount){
        totalTaxableValueTv.setText(inr + String.format("%.2f", totalTaxableValue));
        totalCgstTv.setText(inr + String.format("%.2f", totalSingleGst));
        totalSgstTv.setText(inr + String.format("%.2f", totalSingleGst));
        totalGstTv.setText(inr + String.format("%.2f", (totalSingleGst+totalSingleGst)));
        totalAmountTv.setText(inr + String.format("%.2f", totalAmount));
        totalAmountInWordsTv.setText("Rupees. " + NumberToWord.getNumberInWords(String.valueOf((int)totalAmount)));
    }

    private void markAsPaid(){
        ContentValues contentValues = new ContentValues();
        contentValues.put(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_STATUS, GSTBillingContract.BILL_STATUS_PAID);
        getContentResolver().update(
                GSTBillingContract.GSTBillingEntry.CONTENT_URI.buildUpon().appendPath(String.valueOf(billId)).build(),
                contentValues,
                GSTBillingContract.GSTBillingEntry._ID + "=" + billId,
                null
        );
        Toast.makeText(this, getString(R.string.mark_as_paid_success), Toast.LENGTH_LONG).show();

        billStatus = GSTBillingContract.BILL_STATUS_PAID;
        detailActionBar.setBackgroundDrawable(new ColorDrawable(Color.parseColor("#00C853")));
        getDetailIntent.putExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_STATUS, billStatus);
    }

    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        getMenuInflater().inflate(R.menu.menu_detail, menu);
        return true;
    }

    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        int id = item.getItemId();
        if(id == R.id.action_mark_as_paid){
            if(billStatus.equals(GSTBillingContract.BILL_STATUS_UNPAID)){

                displayPasswordDialog(ACTION_MARK_AS_PAID_ID);

            }else {
                Toast.makeText(this, getString(R.string.marked_as_paid_already), Toast.LENGTH_LONG).show();
            }
        }else if(id == R.id.action_call_customer){
            if(phoneNumber != null && phoneNumber.length() == 10){
                Intent callIntent = new Intent(Intent.ACTION_VIEW);
                callIntent.setData(Uri.parse("tel:" + "+91" + phoneNumber));
                startActivity(callIntent);
            }else {
                Toast.makeText(this, getString(R.string.no_phone_number_error), Toast.LENGTH_SHORT).show();
            }
        }else if(id == R.id.action_delete_bill){
            if(billId != null && billId.length() != 0){

                displayPasswordDialog(ACTION_DELETE_BILL_ID);

            }
        }else if(id == R.id.action_add_more_items){
            if(billId != null && billId.length() != 0){

                displayPasswordDialog(ACTION_ADD_MORE_ITEMS_ID);

            }
        }else if(id == R.id.action_save_to_pdf){
            Intent pdfIntent = new Intent(this, SavePDFActivity.class);
            pdfIntent.putExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_NAME, customerName);
            pdfIntent.putExtra(GSTBillingContract.GSTBillingEntry._ID, billId);
            pdfIntent.putExtra(ITEM_COUNT_KEY, itemCount);
            startActivity(pdfIntent);
        }

        return super.onOptionsItemSelected(item);
    }

    private void deleteBill() {
        int rowsDeleted = getContentResolver().delete(GSTBillingContract.GSTBillingEntry.CONTENT_URI.buildUpon().appendPath(billId).build(), null, null);
        if(rowsDeleted == 1){
            Toast.makeText(this, getString(R.string.delete_bill_success), Toast.LENGTH_SHORT).show();
            finish();
        }else {
            Toast.makeText(this, getString(R.string.delete_bill_error), Toast.LENGTH_SHORT).show();
        }
    }

    private void displayPasswordDialog(final int actionId) {

        String title = getString(R.string.action_mark_as_paid_label);
        if(actionId == ACTION_DELETE_BILL_ID){
            title = getString(R.string.action_delete_bill_label);
        }else if(actionId == ACTION_ADD_MORE_ITEMS_ID){
            title = getString(R.string.action_add_more_items_label);
        }

        final EditText passwordInput = new EditText(this);
        passwordInput.setInputType(InputType.TYPE_CLASS_TEXT|InputType.TYPE_TEXT_VARIATION_PASSWORD);
        passwordInput.setTransformationMethod(PasswordTransformationMethod.getInstance());
        passwordInput.setHint(R.string.enter_password_dialog_hint);
        passwordInput.setHintTextColor(Color.LTGRAY);
        new AlertDialog.Builder(this)
                .setTitle(title)
                .setView(passwordInput)
                .setPositiveButton(getString(R.string.enter_password_dialog_ok), new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        String password = passwordInput.getText().toString();
                        String savedPassword = PreferenceManager.getDefaultSharedPreferences(DetailActivity.this)
                                .getString(SetupPasswordActivity.SETUP_PASSWORD_KEY, null);
                        if(savedPassword != null && savedPassword.equals(password)){

                            switch (actionId){
                                case ACTION_MARK_AS_PAID_ID:
                                    markAsPaid();
                                    break;
                                case ACTION_DELETE_BILL_ID:
                                    deleteBill();
                                    break;
                                case ACTION_ADD_MORE_ITEMS_ID:
                                    addMoreItems();
                                    break;
                                default:
                                    Toast.makeText(DetailActivity.this, getString(R.string.no_operation_specified_error), Toast.LENGTH_SHORT).show();
                            }

                        }else {
                            Toast.makeText(DetailActivity.this, getString(R.string.invalid_password_error), Toast.LENGTH_LONG).show();
                        }
                    }
                })
                .setNegativeButton(getString(R.string.enter_password_dialog_cancel), new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {

                    }
                })
                .show();

    }

    private void addMoreItems() {
        Intent addIntent = new Intent(this, NewBillActivity.class);
        addIntent.putExtra(ADDING_MORE_ITEMS, true);
        addIntent.putExtra(GSTBillingContract.GSTBillingEntry._ID, billId);
        addIntent.putExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_NAME, customerName);
        addIntent.putExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_PHONE_NUMBER, phoneNumber);
        startActivity(addIntent);
    }

    public static void editItem(final Context context, final int id, final String itemDescription, final float finalPrice, final int quantity){

        final EditText passwordInput = new EditText(context);
        passwordInput.setInputType(InputType.TYPE_CLASS_TEXT|InputType.TYPE_TEXT_VARIATION_PASSWORD);
        passwordInput.setTransformationMethod(PasswordTransformationMethod.getInstance());
        passwordInput.setHint(R.string.enter_password_dialog_hint);
        passwordInput.setHintTextColor(Color.LTGRAY);
        new AlertDialog.Builder(context)
                .setTitle(context.getString(R.string.action_edit_bill_item_label))
                .setView(passwordInput)
                .setPositiveButton(context.getString(R.string.enter_password_dialog_ok), new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {
                        String password = passwordInput.getText().toString();
                        String savedPassword = PreferenceManager.getDefaultSharedPreferences(context)
                                .getString(SetupPasswordActivity.SETUP_PASSWORD_KEY, null);
                        if(savedPassword != null && savedPassword.equals(password)){

                            Intent editIntent = new Intent(context, NewBillActivity.class);
                            editIntent.putExtra(EDITING_ITEM, billId);
                            editIntent.putExtra(GSTBillingContract.GSTBillingCustomerEntry._ID, id);
                            editIntent.putExtra(GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_COLUMN_ITEM_DESCRIPTION, itemDescription);
                            editIntent.putExtra(GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_COLUMN_FINAL_PRICE, finalPrice);
                            editIntent.putExtra(GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_COLUMN_QUANTITY, quantity);
                            context.startActivity(editIntent);

                        }else {
                            Toast.makeText(context, context.getString(R.string.invalid_password_error), Toast.LENGTH_LONG).show();
                        }
                    }
                })
                .setNegativeButton(context.getString(R.string.enter_password_dialog_cancel), new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialog, int which) {

                    }
                })
                .show();

    }

    @Override
    public Loader<Cursor> onCreateLoader(int id, Bundle args) {
        return new CursorLoader(
                this,
                GSTBillingContract.GSTBillingEntry.CONTENT_URI.buildUpon().appendPath(billId).build(),
                null,
                null,
                null,
                null
        );
    }

    @Override
    public void onLoadFinished(Loader<Cursor> loader, Cursor data) {
        adapter.swapCursor(data);
        itemCount = data.getCount();
    }

    @Override
    public void onLoaderReset(Loader<Cursor> loader) {
        adapter.swapCursor(null);
        itemCount = 0;
    }

    public static void changeBillStatus(){
        billStatus = GSTBillingContract.BILL_STATUS_UNPAID;
        detailActionBar.setBackgroundDrawable(new ColorDrawable(Color.RED));
        getDetailIntent.putExtra(GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_STATUS, billStatus);
    }
}
