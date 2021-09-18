package com.ldt.cinematicket.ui.inout;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v7.app.AppCompatActivity;

import com.ldt.cinematicket.R;
import com.ldt.cinematicket.ui.main.MainActivity;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.FragmentNavigationController;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.PresentStyle;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;


public class InOutActivity extends AppCompatActivity {
    FragmentNavigationController mNavigationController;
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.log_in_out_activity);
        mNavigationController = FragmentNavigationController.navigationController(getSupportFragmentManager(),R.id.container);
        mNavigationController.setPresentStyle(PresentStyle.SLIDE_UP);
        mNavigationController.setDuration(250);
        mNavigationController.presentFragment(LogIn.newInstance());
    }

    private boolean isNavigationControllerInit() {
        return null!= mNavigationController;
    }
    public void presentFragment(SupportFragment fragment) {
        if(isNavigationControllerInit()) {
//            Random r = new Random();
//            mNavigationController.setPresentStyle(r.nextInt(39)+1); //exclude NONE present style
            mNavigationController.presentFragment(fragment, true);

        }
    }
    public void dismiss() {
        if(isNavigationControllerInit())
            mNavigationController.dismissFragment();
    }

    public void presentFragment(SupportFragment fragment, boolean animated) {
        if(isNavigationControllerInit()) {
            mNavigationController.presentFragment(fragment,animated);
        }
    }
    public void dismiss(boolean animated) {
        if(isNavigationControllerInit()) {
            mNavigationController.dismissFragment(animated);
        }
    }

    @Override
    public void onBackPressed() {
        if(!(isNavigationControllerInit() && mNavigationController.dismissFragment(true)))
            super.onBackPressed();
    }


}
