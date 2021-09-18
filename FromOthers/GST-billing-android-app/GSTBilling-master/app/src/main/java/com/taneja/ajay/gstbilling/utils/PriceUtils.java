package com.taneja.ajay.gstbilling.utils;

/**
 * Created by Ajay on 7/24/2017.
 */

public class PriceUtils {

    private float finalPrice;
    private int quantity;

    private float tax;
    private float rate;
    private float taxableValue;
    private float singleGst;

    public PriceUtils(float finalPrice, int quantity, int taxSlab) {
        this.finalPrice = finalPrice;
        this.quantity = quantity;

        tax = ((float)taxSlab)/100;
    }

    public float getRate(){
        rate = finalPrice/(tax+1);
        rate = ((float)Math.round(rate*100))/100;
        return rate;
    }

    public float getTaxableValue(){
        taxableValue = rate * quantity;
        return taxableValue;
    }

    public float getSingleGst(){
        singleGst = taxableValue*(tax/2);
        singleGst = ((float)Math.round(singleGst*100))/100;
        return singleGst;
    }

}
