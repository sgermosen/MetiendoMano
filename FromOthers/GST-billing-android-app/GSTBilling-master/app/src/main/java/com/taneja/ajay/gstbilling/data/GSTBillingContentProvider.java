package com.taneja.ajay.gstbilling.data;

import android.content.ContentProvider;
import android.content.ContentUris;
import android.content.ContentValues;
import android.content.Context;
import android.content.UriMatcher;
import android.database.Cursor;
import android.database.SQLException;
import android.database.sqlite.SQLiteDatabase;
import android.net.Uri;
import android.support.annotation.Nullable;

import com.taneja.ajay.gstbilling.NewBillActivity;

/**
 * Created by Ajay on 7/23/2017.
 */

public class GSTBillingContentProvider extends ContentProvider {

    public static final int BILLS = 100;
    public static final int BILL_WITH_ID = 101;
    public static final int BILL_WITH_ID_WITH_ID = 102;

    private static final UriMatcher sUriMatcher = buildUriMatcher();

    private static UriMatcher buildUriMatcher() {
        UriMatcher uriMatcher = new UriMatcher(UriMatcher.NO_MATCH);
        uriMatcher.addURI(GSTBillingContract.AUTHORITY, GSTBillingContract.PATH_BILLS, BILLS);
        uriMatcher.addURI(GSTBillingContract.AUTHORITY, GSTBillingContract.PATH_BILLS + "/#", BILL_WITH_ID);
        uriMatcher.addURI(GSTBillingContract.AUTHORITY, GSTBillingContract.PATH_BILLS + "/#/#", BILL_WITH_ID_WITH_ID);
        return uriMatcher;
    }

    private GSTBillingDbHelper mBillingDbHelper;

    @Override
    public boolean onCreate() {
        Context context = getContext();
        mBillingDbHelper = new GSTBillingDbHelper(context);
        return true;
    }

    @Nullable
    @Override
    public Cursor query(Uri uri, String[] projection, String selection, String[] selectionArgs, String sortOrder) {
        final SQLiteDatabase db = mBillingDbHelper.getReadableDatabase();

        int match = sUriMatcher.match(uri);
        Cursor retCursor;

        switch (match){
            case BILLS:
                retCursor = db.query(
                        GSTBillingContract.GSTBillingEntry.PRIMARY_TABLE_NAME,
                        projection,
                        selection,
                        selectionArgs,
                        null,
                        null,
                        sortOrder
                );
                retCursor.setNotificationUri(getContext().getContentResolver(), uri);
                break;
            case BILL_WITH_ID:
                retCursor = db.query(
                        GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_TABLE_NAME + uri.getLastPathSegment(),
                        projection,
                        selection,
                        selectionArgs,
                        null,
                        null,
                        sortOrder
                );
                retCursor.setNotificationUri(getContext().getContentResolver(),
                        GSTBillingContract.BASE_CONTENT_URI.buildUpon().appendPath(GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_TABLE_NAME + uri.getLastPathSegment()).build());
                break;
            default:
                throw new UnsupportedOperationException("Unknown uri: " + uri);
        }

        return retCursor;
    }

    @Nullable
    @Override
    public String getType(Uri uri) {
        return null;
    }

    @Nullable
    @Override
    public Uri insert(Uri uri, ContentValues values) {
        final SQLiteDatabase db = mBillingDbHelper.getWritableDatabase();

        int match = sUriMatcher.match(uri);
        Uri returnUri;
        switch (match){
            case BILLS:
                long id = db.insert(GSTBillingContract.GSTBillingEntry.PRIMARY_TABLE_NAME, null, values);
                if(id > 0){
                    returnUri = ContentUris.withAppendedId(GSTBillingContract.GSTBillingEntry.CONTENT_URI, id);
                }else {
                    throw new SQLException("Failed to insert row into : " + uri);
                }
                break;
            default:
                throw new UnsupportedOperationException("Unknown uri: " + uri);
        }
        getContext().getContentResolver().notifyChange(uri, null);
        return returnUri;
    }

    @Override
    public int bulkInsert(Uri uri, ContentValues[] values) {
        final SQLiteDatabase db = mBillingDbHelper.getWritableDatabase();

        int match = sUriMatcher.match(uri);
        switch (match){
            case BILL_WITH_ID:
                if(NewBillActivity.addingMoreItems == false){
                    GSTBillingDbHelper.createBillTable(db, uri.getLastPathSegment());
                }
                db.beginTransaction();
                int rowsInserted = 0;
                try{
                    for (ContentValues value : values) {
                        long _id = db.insert(GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_TABLE_NAME + uri.getLastPathSegment(), null, value);
                        if(_id != -1){
                            rowsInserted++;
                        }
                    }
                    db.setTransactionSuccessful();
                }finally {
                    db.endTransaction();
                }

                if(rowsInserted > 0){
                    getContext().getContentResolver().notifyChange(GSTBillingContract.BASE_CONTENT_URI.buildUpon().appendPath(GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_TABLE_NAME + uri.getLastPathSegment()).build(), null);
                }
                return rowsInserted;
            default:
                return super.bulkInsert(uri, values);
        }
    }

    @Override
    public int delete(Uri uri, String selection, String[] selectionArgs) {
        final SQLiteDatabase db = mBillingDbHelper.getWritableDatabase();

        int match = sUriMatcher.match(uri);
        int rowsDeleted = 0;
        switch (match){
            case BILL_WITH_ID:
                try{
                    db.beginTransaction();

                    GSTBillingDbHelper.dropBillTable(db, uri.getLastPathSegment());
                    rowsDeleted = db.delete(
                            GSTBillingContract.GSTBillingEntry.PRIMARY_TABLE_NAME,
                            GSTBillingContract.GSTBillingEntry._ID + "=" + uri.getLastPathSegment(),
                            selectionArgs
                    );

                    db.setTransactionSuccessful();
                }finally {
                    db.endTransaction();
                }

                if(rowsDeleted > 0){
                    getContext().getContentResolver().notifyChange(GSTBillingContract.GSTBillingEntry.CONTENT_URI, null);
                }
                break;
            default:
                throw new UnsupportedOperationException("Unknown uri: " + uri);
        }
        return rowsDeleted;
    }


    @Override
    public int update(Uri uri, ContentValues values, String selection, String[] selectionArgs) {
        final SQLiteDatabase db = mBillingDbHelper.getWritableDatabase();

        int match = sUriMatcher.match(uri);
        int rowsUpdated = 0;
        switch (match){
            case BILL_WITH_ID:
                rowsUpdated = db.update(GSTBillingContract.GSTBillingEntry.PRIMARY_TABLE_NAME, values, selection, selectionArgs);
                if(rowsUpdated > 0){
                    getContext().getContentResolver().notifyChange(GSTBillingContract.GSTBillingEntry.CONTENT_URI, null);
                }
                break;
            case BILL_WITH_ID_WITH_ID:
                rowsUpdated = db.update(
                        GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_TABLE_NAME + uri.getPathSegments().get(1),
                        values,
                        GSTBillingContract.GSTBillingCustomerEntry._ID + "=" + uri.getLastPathSegment(),
                        null
                );
                if(rowsUpdated > 0){
                    getContext().getContentResolver().notifyChange(GSTBillingContract.BASE_CONTENT_URI.buildUpon().appendPath(GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_TABLE_NAME + uri.getPathSegments().get(1)).build(), null);
                    getContext().getContentResolver().notifyChange(GSTBillingContract.GSTBillingEntry.CONTENT_URI, null);
                }
                break;
            default:
                throw new UnsupportedOperationException("Unknown uri: " + uri);
        }
        return rowsUpdated;
    }
}
