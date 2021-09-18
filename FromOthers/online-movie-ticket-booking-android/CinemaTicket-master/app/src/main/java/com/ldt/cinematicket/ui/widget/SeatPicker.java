package com.ldt.cinematicket.ui.widget;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Paint;
import android.graphics.Path;
import android.support.annotation.Nullable;
import android.support.v4.content.ContextCompat;
import android.util.AttributeSet;
import android.view.View;

public class SeatPicker extends View {
    public SeatPicker(Context context) {
        super(context);
    }

    public SeatPicker(Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
    }
    private void drawCurvedArrow(Canvas canvas, int x1, int y1, int x2, int y2, int curveRadius, int color, int lineWidth) {

        Paint paint  = new Paint();
        paint.setAntiAlias(true);
        paint.setStyle(Paint.Style.STROKE);
        paint.setStrokeWidth(lineWidth);
        paint.setColor(color);

        final Path path = new Path();
        int midX            = x1 + ((x2 - x1) / 2);
        int midY            = y1 + ((y2 - y1) / 2);
        float xDiff         = midX - x1;
        float yDiff         = midY - y1;
        double angle        = (Math.atan2(yDiff, xDiff) * (180 / Math.PI)) - 90;
        double angleRadians = Math.toRadians(angle);
        float pointX        = (float) (midX + curveRadius * Math.cos(angleRadians));
        float pointY        = (float) (midY + curveRadius * Math.sin(angleRadians));

        path.moveTo(x1, y1);
        path.cubicTo(x1,y1,pointX, pointY, x2, y2);
        canvas.drawPath(path, paint);
        paint.setTextAlign(Paint.Align.CENTER);
        //paint.setFakeBoldText(true);
        paint.setStyle(Paint.Style.FILL_AND_STROKE);
        paint.setStrokeWidth(lineWidth/5);
        paint.setTextSize(lineWidth*3);
        canvas.drawText("Screen",(x2+x1)/2,y1+y1/2,paint);
    }
    public SeatPicker(Context context, @Nullable AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
    }
    @Override
    protected void onDraw(Canvas canvas) {
    drawCurvedArrow(canvas,20,100,getWidth()-20,100,30,0xffFAA600,10);

    }
}
