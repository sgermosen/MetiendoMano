package com.ldt.cinematicket.ui.widget;

import android.content.Context;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.PorterDuff;
import android.graphics.PorterDuffXfermode;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.util.AttributeSet;
import android.widget.LinearLayout;

import com.ldt.cinematicket.util.BitmapEditor;

public class RoundedFrameLayout extends LinearLayout {
    private static final String TAG = "RoundedFrameLayout";
    public RoundedFrameLayout(@NonNull Context context) {
        super(context);
    }

    public RoundedFrameLayout(@NonNull Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
    }

    public RoundedFrameLayout(@NonNull Context context, @Nullable AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
    }


    protected void _dispatchDraw(Canvas canvas) {
        Bitmap bitmap = Bitmap.createBitmap(canvas.getWidth(),canvas.getHeight(),Bitmap.Config.ARGB_8888);
        Canvas c = new Canvas(bitmap);
        super.dispatchDraw(c);
        Bitmap bitmap1 =  Bitmap.createBitmap(canvas.getWidth(),canvas.getHeight(),Bitmap.Config.ARGB_8888);
        c.setBitmap(bitmap1);
        Paint p = new Paint(Paint.ANTI_ALIAS_FLAG);
        p.setColor(0xff1ED760);
        p.setStyle(Paint.Style.FILL);
      //  p.setStrokeWidth(20);

   //    c.drawColor(Color.BLACK);
        c.drawPath(BitmapEditor.RoundedRect(0,0,canvas.getWidth(),canvas.getHeight(),canvas.getWidth()/16,canvas.getWidth()/16,false),p);
        p.setXfermode(new PorterDuffXfermode(PorterDuff.Mode.SRC_IN));
        c.drawBitmap(bitmap,0,0,p);
        canvas.drawBitmap(bitmap1,0,0,null);
    }

    @Override
    protected void onSizeChanged(int w, int h, int oldw, int oldh) {
        super.onSizeChanged(w, h, oldw, oldh);
        if(bitmap==null) {
            bitmap = Bitmap.createBitmap(getWidth(),getHeight(), Bitmap.Config.ARGB_8888);
            c = new Canvas(bitmap);
            p = new Paint(Paint.ANTI_ALIAS_FLAG);
            p.setColor(0xff1ED760);
            p.setStyle(Paint.Style.STROKE);
            p.setStrokeWidth(getWidth()/16);

            //    c.drawColor(Color.BLACK);
            p.setXfermode(new PorterDuffXfermode(PorterDuff.Mode.DST_OUT));
        }
        else {
            c.setBitmap(null);
            bitmap.recycle();
            bitmap = Bitmap.createBitmap(getWidth(),getHeight(), Bitmap.Config.ARGB_8888);
            c.setBitmap(bitmap);
        }
    }
Bitmap bitmap;
    Canvas c;
    Paint p;
    // @Override
    protected void dispatchDraw(Canvas canvas) {
     //   Log.d(TAG, "dispatchDraw");
        if(bitmap==null) {
            bitmap = Bitmap.createBitmap(getWidth(), getHeight(), Bitmap.Config.ARGB_8888);
            c = new Canvas(bitmap);
            p = new Paint(Paint.ANTI_ALIAS_FLAG);
            p.setColor(0xff1ED760);
            p.setStyle(Paint.Style.STROKE);
            p.setStrokeWidth(getWidth()/16);

            //    c.drawColor(Color.BLACK);
            p.setXfermode(new PorterDuffXfermode(PorterDuff.Mode.DST_OUT));
        }
        else
        bitmap.eraseColor(0);
        super.dispatchDraw(c);
       // Bitmap bitmap1 =  Bitmap.createBitmap(canvas.getWidth(),canvas.getHeight(),Bitmap.Config.ARGB_8888);
       // c.setBitmap(bitmap1);

        c.drawPath(BitmapEditor.RoundedRect(-getWidth()/32,-getWidth()/32,getWidth()+getWidth()/32,getHeight()+getWidth()/32,getWidth()/16,getWidth()/16,false),p);

     //   c.drawBitmap(bitmap,0,0,p);
       canvas.drawBitmap(bitmap,0,0,null);
    }
}
