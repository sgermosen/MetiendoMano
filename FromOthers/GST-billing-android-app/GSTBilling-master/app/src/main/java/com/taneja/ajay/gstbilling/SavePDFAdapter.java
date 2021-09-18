package com.taneja.ajay.gstbilling;

import android.content.Context;
import android.database.Cursor;
import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.taneja.ajay.gstbilling.data.GSTBillingContract;
import com.taneja.ajay.gstbilling.utils.PriceUtils;

/**
 * Created by Ajay on 8/28/2017.
 */

public class SavePDFAdapter extends RecyclerView.Adapter<SavePDFAdapter.SavePDFHolder> {

    private int totalQty;
    private float totalTaxableValue;
    private float totalSingleGst;
    private float totalAmount;

    private Context mContext;
    private Cursor mCursor;

    public SavePDFAdapter(Context mContext, Cursor mCursor) {
        this.mContext = mContext;
        this.mCursor = mCursor;

        totalQty = 0;
        totalTaxableValue = 0f;
        totalSingleGst = 0f;
        totalAmount = 0f;
    }

    public void swapCursor(Cursor newCursor){
        mCursor = newCursor;
        this.notifyDataSetChanged();
    }

    @Override
    public SavePDFHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(mContext).inflate(R.layout.single_item_pdf_layout, parent, false);
        return new SavePDFHolder(view);
    }

    @Override
    public void onBindViewHolder(SavePDFHolder holder, int position) {
        if(mCursor.moveToPosition(position)){

            int idValue = mCursor.getInt(mCursor.getColumnIndex(GSTBillingContract.GSTBillingCustomerEntry._ID));
            String itemDescriptionValue = mCursor.getString(mCursor.getColumnIndex(GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_COLUMN_ITEM_DESCRIPTION));
            float finalPriceValue = mCursor.getFloat(mCursor.getColumnIndex(GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_COLUMN_FINAL_PRICE));
            int quantityValue = mCursor.getInt(mCursor.getColumnIndex(GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_COLUMN_QUANTITY));
            int taxSlabValue = mCursor.getInt(mCursor.getColumnIndex(GSTBillingContract.GSTBillingCustomerEntry.SECONDARY_COLUMN_TAX_SLAB));

            PriceUtils priceUtils = new PriceUtils(finalPriceValue, quantityValue, taxSlabValue);
            float rateValue = priceUtils.getRate();
            float taxableValueValue = priceUtils.getTaxableValue();
            float singleGstValue = priceUtils.getSingleGst();

            holder.itemPdf.setText(itemDescriptionValue);
            holder.snoPdf.setText(String.format("%02d", idValue));
            holder.qtyPdf.setText(String.valueOf(quantityValue));
            holder.ratePdf.setText(String.format("%.2f", rateValue));
            holder.taxableValuePdf.setText(String.format("%.2f", taxableValueValue));
            holder.taxSlabPdf.setText(String.valueOf(taxSlabValue) + "%");
            holder.cgstPdf.setText(String.format("%.2f", singleGstValue));
            holder.sgstPdf.setText(String.format("%.2f", singleGstValue));

            totalQty += quantityValue;
            totalTaxableValue += taxableValueValue;
            totalSingleGst += singleGstValue;
            totalAmount += (finalPriceValue*quantityValue);

            if (position == getItemCount()-1){
                SavePDFActivity.printTotalDetails(totalQty, totalTaxableValue, totalSingleGst, totalAmount);
            }

        }
    }

    @Override
    public int getItemCount() {
        if(mCursor == null){
            return 0;
        }else {
            return mCursor.getCount();
        }
    }

    public class SavePDFHolder extends RecyclerView.ViewHolder {

        TextView snoPdf, itemPdf, ratePdf, qtyPdf, taxableValuePdf, taxSlabPdf, cgstPdf, sgstPdf;

        public SavePDFHolder(View itemView) {
            super(itemView);

            snoPdf = (TextView) itemView.findViewById(R.id.pdf_serial_number);
            itemPdf = (TextView) itemView.findViewById(R.id.pdf_item);
            ratePdf = (TextView) itemView.findViewById(R.id.pdf_rate);
            qtyPdf = (TextView) itemView.findViewById(R.id.pdf_qty);
            taxableValuePdf = (TextView) itemView.findViewById(R.id.pdf_taxable_value);
            taxSlabPdf = (TextView) itemView.findViewById(R.id.pdf_tax_slab);
            cgstPdf = (TextView) itemView.findViewById(R.id.pdf_cgst);
            sgstPdf = (TextView) itemView.findViewById(R.id.pdf_sgst);
        }
    }
}
