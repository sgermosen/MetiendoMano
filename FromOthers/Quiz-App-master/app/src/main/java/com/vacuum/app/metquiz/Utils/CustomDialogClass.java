package com.vacuum.app.metquiz.Utils;

import android.app.Activity;
import android.app.Dialog;
import android.content.Context;
import android.content.SharedPreferences;
import android.os.Bundle;
import android.view.KeyEvent;
import android.view.View;
import android.view.Window;
import android.view.inputmethod.EditorInfo;
import android.view.inputmethod.InputMethodManager;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;

import com.vacuum.app.metquiz.R;
import com.vacuum.app.metquiz.Splash.SplashScreen;

import static android.content.Context.MODE_PRIVATE;
import static com.vacuum.app.metquiz.Splash.SplashScreen.MY_PREFS_NAME;

public class CustomDialogClass extends Dialog{

    public Activity c;
    public Dialog d;
    EditText edittext_ip2;
    Button button_ip_2;
    SharedPreferences.Editor editor;

    Context mContext;
    public CustomDialogClass(Activity a) {
        super(a);
        // TODO Auto-generated constructor stub
        this.c = a;
    }

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        requestWindowFeature(Window.FEATURE_NO_TITLE);
        setContentView(R.layout.custom_dialog);
        edittext_ip2 = (EditText) findViewById(R.id.edittext_ip2);
        button_ip_2 = (Button) findViewById(R.id.button_ip_2);

        mContext = this.getContext();
        editor = mContext.getSharedPreferences(MY_PREFS_NAME, MODE_PRIVATE).edit();

        edittext_ip2.requestFocus();
        ((InputMethodManager)mContext.getSystemService(Context.INPUT_METHOD_SERVICE))
                .showSoftInput(edittext_ip2, InputMethodManager.SHOW_FORCED);
        edittext_ip2.setOnEditorActionListener(new EditText.OnEditorActionListener() {
            @Override
            public boolean onEditorAction(TextView v, int actionId, KeyEvent event) {
                if (actionId == EditorInfo.IME_ACTION_DONE) {
                    Done();
                    return true;
                }
                return false;
            }
        });
        button_ip_2.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                Done();
            }
        });
    }

    private void Done() {
        editor.putString("ip", "http://192.168."+ edittext_ip2.getText().toString()+ "/" );
        editor.apply();
        dismiss();
    }


}

