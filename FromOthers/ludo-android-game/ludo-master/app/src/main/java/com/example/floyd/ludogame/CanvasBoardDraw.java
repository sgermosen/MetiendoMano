package com.example.floyd.ludogame;

import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.util.AttributeSet;
import android.view.View;

public class CanvasBoardDraw extends View{

    private Paint paint, paint_path, paint_home_path, paintframe, paint_big_circle, p1, p2, p3, p4;
    public int hgt, width, top, bottom, d, i;

    public CanvasBoardDraw(Context c, AttributeSet attrs) {

        super(c, attrs);

    }

    @Override
    protected void onDraw(Canvas canvas) {
        super.onDraw(canvas);

        assign(canvas);
        maker(canvas);
        bigcircle(canvas);

    }

    private void assign(Canvas canvas1) {

        width = canvas1.getWidth();
        hgt = canvas1.getHeight();
        d = width / 15;
        top = (hgt - width) / 2;
        bottom = (hgt + width) / 2;


        p1 = new Paint();
        p2 = new Paint();
        p3 = new Paint();
        p4 = new Paint();
        p1.setColor(Color.RED);
        p2.setColor(Color.GREEN);
        p3.setColor(Color.YELLOW);
        p4.setColor(Color.BLUE);

        paint = new Paint();
        paint.setStrokeWidth(2f);
        paint.setColor(Color.BLACK);
        paint.setStyle(Paint.Style.STROKE);

        paint_path = new Paint();
        paint_path.setColor(Color.LTGRAY);
        paint_path.setStyle(Paint.Style.FILL);

        paint_home_path = new Paint();
        paint_home_path.setColor(Color.TRANSPARENT);
        paint_home_path.setStyle(Paint.Style.FILL);

        paintframe = new Paint();
        paintframe.setAlpha(16);
        paintframe.setStyle(Paint.Style.FILL);

        paint_big_circle = new Paint();
        paint_big_circle.setColor(Color.LTGRAY);
        paint_big_circle.setStyle(Paint.Style.FILL);

    }

    private void maker(Canvas canvas1) {

        canvas1.drawRect(0, top, width, bottom, paintframe);

        canvas1.drawRect(8, top + 8, 6 * d - 8, top + 6 * d - 8, p1);//box1-frame--top-left
        canvas1.drawRect(9 * d + 8, top + 8, width - 8, top + 6 * d - 8, p2);//box2-frame--top-right
        canvas1.drawRect(8, top + 9 * d + 8, 6 * d - 8, bottom - 8, p3);//box3-frame--bottom-left
        canvas1.drawRect(9 * d + 8, top + 9 * d + 8, width - 8, bottom - 8, p4);//box4-frame--bottom-right
        canvas1.drawRect(8, top + 8, 6 * d - 8, top + 6 * d - 8, paint);//box1-frame--top-left
        canvas1.drawRect(9 * d + 8, top + 8, width - 8, top + 6 * d - 8, paint);//box2-frame--top-right
        canvas1.drawRect(8, top + 9 * d + 8, 6 * d - 8, bottom - 8, paint);//box3-frame--bottom-left
        canvas1.drawRect(9 * d + 8, top + 9 * d + 8, width - 8, bottom - 8, paint);//box4-frame--bottom-right


        for (i = 0; i <= 5; i++) {

            canvas1.drawRect(i * d, top + 6 * d, d + i * d, top + 7 * d, paint_path);//left-top
            canvas1.drawRect(i * d, top + 8 * d, d + i * d, top + 9 * d, paint_path);//left-bottom
            canvas1.drawRect(9 * d + i * d, top + 6 * d, 10 * d + i * d, top + 7 * d, paint_path);//right-top
            canvas1.drawRect(9 * d + i * d, top + 8 * d, 10 * d + i * d, top + 9 * d, paint_path);//right-bottom

            canvas1.drawRect(i * d, top + 6 * d, d + i * d, top + 7 * d, paint);//left-top
            canvas1.drawRect(i * d, top + 8 * d, d + i * d, top + 9 * d, paint);//left-bottom
            canvas1.drawRect(9 * d + i * d, top + 6 * d, 10 * d + i * d, top + 7 * d, paint);//right-top
            canvas1.drawRect(9 * d + i * d, top + 8 * d, 10 * d + i * d, top + 9 * d, paint);//right-bottom

            canvas1.drawRect(6 * d, top + i * d, 7 * d, top + d + i * d, paint_path);//top-left
            canvas1.drawRect(8 * d, top + i * d, 9 * d, top + d + i * d, paint_path);//top-right
            canvas1.drawRect(6 * d, top + 9 * d + i * d, 7 * d, top + 10 * d + i * d, paint_path);//bottom-left
            canvas1.drawRect(8 * d, top + 9 * d + i * d, 9 * d, top + 10 * d + i * d, paint_path);//bottom-right

            canvas1.drawRect(6 * d, top + i * d, 7 * d, top + d + i * d, paint);//top-left
            canvas1.drawRect(8 * d, top + i * d, 9 * d, top + d + i * d, paint);//top-right
            canvas1.drawRect(6 * d, top + 9 * d + i * d, 7 * d, top + 10 * d + i * d, paint);//bottom-left
            canvas1.drawRect(8 * d, top + 9 * d + i * d, 9 * d, top + 10 * d + i * d, paint);//bottom-right

        }

        canvas1.drawRect(0, top + 7 * d, d, top + 8 * d, paint_path);//left-center
        canvas1.drawRect(7 * d, top+ 14*d, 8 * d, top + 15*d, paint_path);//bottom-center
        canvas1.drawRect(14*d, top + 7 * d, 15*d, top + 8 * d, paint_path);//right-center
        canvas1.drawRect(7 * d, top, 8 * d, top + d, paint_path);//top-center

        canvas1.drawRect(0, top + 7 * d, d, top + 8 * d, paint);//left-center
        canvas1.drawRect(7 * d, top+ 14*d, 8 * d, top + 15*d,paint);//bottom-center
        canvas1.drawRect(14*d, top + 7 * d, 15*d, top + 8 * d, paint);//right-center
        canvas1.drawRect(7 * d, top, 8 * d, top + d, paint);//top-center

        for (i = 0; i <= 4; i++) {

            canvas1.drawRect(d + i * d, top + 7 * d, 2 * d + i * d, top + 8 * d, paint_home_path);//left-home-line
            canvas1.drawRect(7 * d, top + 9 * d + i * d, 8 * d, top + 10 * d + i * d, paint_home_path);//bottom-home-line
            canvas1.drawRect(9 * d + i * d, top + 7 * d, 10 * d + i * d, top + 8 * d, paint_home_path);//right-home-line
            canvas1.drawRect(7 * d, top + d + i * d, 8 * d, top + 2 * d + i * d, paint_home_path);//top-home-line

            canvas1.drawRect(d + i * d, top + 7 * d, 2 * d + i * d, top + 8 * d, paint);//left-home-line
            canvas1.drawRect(7 * d, top + 9 * d + i * d, 8 * d, top + 10 * d + i * d, paint);//bottom-home-line
            canvas1.drawRect(9 * d + i * d, top + 7 * d, 10 * d + i * d, top + 8 * d, paint);//right-home-line
            canvas1.drawRect(7 * d, top + d + i * d, 8 * d, top + 2 * d + i * d, paint);//top-home-line

        }

    }

    private void bigcircle(Canvas can) {

        //Board-one
        can.drawCircle(3 * d, top + 3 * d, 3 * d - 20, paint_big_circle);
        can.drawCircle(3 * d, top + 3 * d, 3 * d - 20, paint);

        //Board-two
        can.drawCircle(12 * d, top + 3 * d, 3 * d - 20, paint_big_circle);
        can.drawCircle(12 * d, top + 3 * d, 3 * d - 20, paint);

        //Board-three
        can.drawCircle(3 * d, bottom - 3 * d, 3 * d - 20, paint_big_circle);
        can.drawCircle(3 * d, bottom - 3 * d, 3 * d - 20, paint);

        //Board-four
        can.drawCircle(12 * d, bottom - 3 * d, 3 * d - 20, paint_big_circle);
        can.drawCircle(12 * d, bottom - 3 * d, 3 * d - 20, paint);

    }

}