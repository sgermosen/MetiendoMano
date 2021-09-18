package com.ldt.cinematicket.ui.widget;

import android.animation.Animator;
import android.animation.ValueAnimator;
import android.content.Context;
import android.graphics.Canvas;
import android.graphics.Color;
import android.graphics.Paint;
import android.support.annotation.Nullable;
import android.util.AttributeSet;
import android.view.View;
import android.widget.FrameLayout;

import com.ldt.cinematicket.util.BitmapEditor;

public class MotionLayout extends FrameLayout {
    public interface MotionListener {
        void EndMotion() ;
    }
    public MotionLayout(Context context) {
        super(context);
        init();
    }

    public MotionLayout(Context context, @Nullable AttributeSet attrs) {
        super(context, attrs);
        init();
    }

    private void init() {
        paint = new Paint(Paint.ANTI_ALIAS_FLAG);
    }
    private Paint paint;

    private boolean started = false;
    private View view;
    private int[] size = new int[2];
    private int[] pos= new int[2];
    private int[] ri_bo = new int[2];
    ValueAnimator va;
    MotionListener l;
    public void StartMotion(MotionListener l,View view,int[] pos) {
       if(!started) {
           this.l = l;
           started = true;
           getLocationInWindow(this.pos);

           this.pos[0] = pos[0] -this.pos[0];
           this.pos[1] = pos[1] -this.pos[1];
           size[0] = view.getWidth();
           size[1] =view.getHeight();
           ri_bo[0] = this.pos[0] + size[0];
           ri_bo[1] = this.pos[1] + size[1];
           this.view = view;
           value= 0;
           va = ValueAnimator.ofFloat(0,1);
           va.setInterpolator(BitmapEditor.getInterpolator(4));
           va.setDuration(600);
           va.addUpdateListener(new ValueAnimator.AnimatorUpdateListener() {
               @Override
               public void onAnimationUpdate(ValueAnimator valueAnimator) {
                   value =(float) valueAnimator.getAnimatedValue();
                   invalidate();
               }
           });
           va.addListener(new Animator.AnimatorListener() {
               @Override
               public void onAnimationStart(Animator animator) {

               }

               @Override
               public void onAnimationEnd(Animator animator) {
                   running = false;
                  endMotion();
               }

               @Override
               public void onAnimationCancel(Animator animator) {

               }

               @Override
               public void onAnimationRepeat(Animator animator) {

               }
           });
           running= true;
           va.start();
       }
    }
    private void endMotion() {
        if(l!=null) l.EndMotion();
    }
    private float value =0;
    private boolean running = false;
    public MotionLayout(Context context, @Nullable AttributeSet attrs, int defStyleAttr) {
        super(context, attrs, defStyleAttr);
    }
    @Override
    protected void dispatchDraw(Canvas canvas) {
        //TODO: Draw motion here
        if(!started) return;
        int c = (Color.BLACK & 0x00FFFFFF) | (((int)(0xbb*value)) << 24);
        canvas.drawColor(c);

  //      view.draw(canvas);
        int toLeft = getPaddingLeft();
        int toTop = getPaddingTop();
        int toRight = getWidth()-getPaddingRight();
        int toBottom = getHeight()-getPaddingBottom();
        paint.setColor(0xff1D202A);
        paint.setShadowLayer(20, 0, 0, (Color.BLACK & 0x00FFFFFF) | (((int)(0xaa*value)) << 24));

        // Important for certain APIs
        setLayerType(LAYER_TYPE_SOFTWARE, paint);
        paint.setStyle(Paint.Style.FILL);
        float value_x = (value>1) ? 1: value;
        float value_y = (value>1) ? (float) (1 +  value / (value + 75.0f)) : value;
        canvas.drawPath(BitmapEditor.RoundedRect(
                /* Left  */ pos[0] +(toLeft - pos[0])*(float)Math.pow(value_x,0.7),
                /* Top  */ pos[1] +(toTop -pos[1])*value_y*value_y,
                /* Right*/ ri_bo[0] +(toRight-ri_bo[0])*(float)Math.pow(value_x,0.7),
                /* Bottom */ri_bo[1] +(toBottom - ri_bo[1])*value_y*value_y,20,20,false
        ),paint);
        if(!running&&value==1)
        super.dispatchDraw(canvas);
    }
}
