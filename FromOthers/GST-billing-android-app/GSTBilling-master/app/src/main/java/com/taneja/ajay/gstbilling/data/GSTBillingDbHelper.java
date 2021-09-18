package com.taneja.ajay.gstbilling.data;

import android.content.Context;
import android.database.sqlite.SQLiteDatabase;
import android.database.sqlite.SQLiteOpenHelper;

/**
 * Created by Ajay on 7/23/2017.
 */

public class GSTBillingDbHelper extends SQLiteOpenHelper {

    private static final String DATABASE_NAME = "GSTBillsDB.db";
    private static final int VERSION = 1;

    public GSTBillingDbHelper(Context context) {
        super(context, DATABASE_NAME, null, VERSION);
    }


    @Override
    public void onCreate(SQLiteDatabase db) {

        final String CREATE_TABLE = "CREATE TABLE "  + GSTBillingContract.GSTBillingEntry.PRIMARY_TABLE_NAME + " (" +
                GSTBillingContract.GSTBillingEntry._ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_NAME + " TEXT NOT NULL, " +
                GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_PHONE_NUMBER + " TEXT NOT NULL, " +
                GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_DATE + " TEXT NOT NULL, " +
                GSTBillingContract.GSTBillingEntry.PRIMARY_COLUMN_STATUS + " TEXT NOT NULL);";

        db.execSQL(CREATE_TABLE);

    }

    @Override
    public void onUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {

    }

    public static void createBillTable(SQLiteDatabase db, String billId){

        final String CREATE_TABLE = "CREATE TABLE "  + GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_TABLE_NAME + billId + " (" +
                GSTBillingContract.GSTBillingCustomerEntry._ID + " INTEGER PRIMARY KEY AUTOINCREMENT, " +
                GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_COLUMN_ITEM_DESCRIPTION + " TEXT NOT NULL, " +
                GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_COLUMN_FINAL_PRICE + " REAL NOT NULL, " +
                GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_COLUMN_QUANTITY + " INTEGER NOT NULL, " +
                GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_COLUMN_TAX_SLAB + " INTEGER NOT NULL);";

        db.execSQL(CREATE_TABLE);

    }

    public static void dropBillTable(SQLiteDatabase db, String billId){

        final String DROP_TABLE = "DROP TABLE IF EXISTS " + GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_TABLE_NAME + billId;
        db.execSQL(DROP_TABLE);

    }

}
