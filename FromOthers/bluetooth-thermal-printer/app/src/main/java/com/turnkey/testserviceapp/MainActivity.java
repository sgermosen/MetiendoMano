package com.turnkey.testserviceapp;

import android.app.Activity;
import android.bluetooth.BluetoothAdapter;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.AsyncTask;
import android.os.Handler;
import android.os.Message;
import android.support.v7.app.AppCompatActivity;
import android.os.Bundle;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.Toast;

import com.lvrenyang.io.IOCallBack;
import java.lang.ref.WeakReference;

public class MainActivity extends AppCompatActivity {

    private Handler mHandler; // Our main handler that will receive callback notifications
    // #defines for identifying shared types between calling functions
    private final static int REQUEST_ENABLE_BT = 1; // used to identify adding bluetooth names

    private static String TAG = "MAIN_ACTIVITY";
    private Activity activity;
    private Button btnConnect;

    private String name = "MTP-II";
    private String mac_address = "02:15:44:31:49:05";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);

        //Get the activity
        this.activity = this;

        //Button from the XML view
        btnConnect = findViewById(R.id.btnConnect);

        //Start the Init Work Service Async task
        new InitWorkService().execute();

        //Set onClickListener for test print button
        btnConnect.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                try {
                    //Check if name and address are set
                    if (name != "null" && mac_address != "null" && mac_address.contains(":")) {

                        if (!WorkService.workThread.isConnected()) {
                            WorkService.workThread.connectBt(mac_address);
                            //Sleep for 3 seconds
                            try {
                                Thread.sleep(3000);
                            } catch (Exception e) {
                            }
                        }
                        //Check if connected
                        if (WorkService.workThread.isConnected()) {
                            //Collect data in background Thread
                            new PrintData().execute();
                        } else {
                            Toast.makeText(activity, Global.toast_notconnect, Toast.LENGTH_SHORT).show();
                        }
                    } else {
                        Toast.makeText(activity, "Please setup printer first!", Toast.LENGTH_LONG).show();
                    }
                }
                catch(Exception e){
                    Log.e(TAG, e.getMessage(), e.fillInStackTrace());}
            }
        });
    }


    /**
     * Background Async Task
     * */
    private class InitWorkService extends AsyncTask<String, String, String> {
        @Override
        protected void onPreExecute() {
            super.onPreExecute();
        }
        protected String doInBackground(String... args){
            try{
                WorkService.cb = new IOCallBack() {
                    public void OnOpen() {
                        if (null != mHandler) {
                            Message msg = mHandler.obtainMessage(Global.MSG_IO_ONOPEN);
                            mHandler.sendMessage(msg);
                        }
                    }
                    public void OnClose() {
                        if (null != mHandler) {
                            Message msg = mHandler.obtainMessage(Global.MSG_IO_ONCLOSE);
                            mHandler.sendMessage(msg);
                        }
                    }
                };
            }
            catch(Exception e){
                Log.e(TAG, e.getMessage(), e.fillInStackTrace());
            }
            return null;
        }
        protected void onPostExecute(String file_url){
            try {
                mHandler = new MHandler(MainActivity.this);
                WorkService.addHandler(mHandler);

                if (null == WorkService.workThread) {
                    Intent intent = new Intent(activity, WorkService.class);
                    startService(intent);
                }
            }
            catch (Exception e) {
                Log.e(TAG, e.getMessage(), e.fillInStackTrace());
                Toast.makeText(activity, "Unable to initiate the WorkService!", Toast.LENGTH_LONG).show();
            }
        }
    }

    /**
     * Background Async Task
     * */
    class PrintData extends AsyncTask<String, String, String> {
        @Override
        protected void onPreExecute() {
            super.onPreExecute();
        }
        protected String doInBackground(String... args){
            try{
                int nTextAlign=1;
                String text = "Test message!\r\n\r\n\r\n";
                String encoding = "UTF-8";
                byte[] hdrBytes = {0x1c, 0x26, 0x1b, 0x39, 0x01};

                Bundle dataAlign = new Bundle();
                Bundle dataTextOut = new Bundle();
                Bundle dataHdr = new Bundle();

                dataHdr.putByteArray(Global.BYTESPARA1, hdrBytes);
                dataHdr.putInt(Global.INTPARA1, 0);
                dataHdr.putInt(Global.INTPARA2, hdrBytes.length);

                dataAlign.putInt(Global.INTPARA1, nTextAlign);

                dataTextOut.putString(Global.STRPARA1, text);
                dataTextOut.putString(Global.STRPARA2, encoding);

                WorkService.workThread.handleCmd(Global.CMD_POS_WRITE,dataHdr);
                WorkService.workThread.handleCmd(Global.CMD_POS_SALIGN,dataAlign);
                WorkService.workThread.handleCmd(Global.CMD_POS_STEXTOUT,dataTextOut);
            }catch(Exception e){
                Log.e(TAG, e.getMessage(), e.fillInStackTrace());
            }
            return null;
        }
        protected void onPostExecute(String file_url){}
    }


    @Override
    protected void onUserLeaveHint()
    {
        Log.d("onUserLeaveHint","Home button pressed");
        super.onUserLeaveHint();
        //Unregister bluetooth receiver
        try{unregisterReceiver(bluetoothReceiver);}catch(Exception e){}
        //Disconnect bt connection
        try{WorkService.workThread.disconnectBt();}catch(Exception e){}
        // remove the handler
        try{WorkService.delHandler(mHandler);}catch(Exception e){}
        mHandler = null;
    }

    /**
     * Broadcast receiver for bluetooth state changes
     */
    private final BroadcastReceiver bluetoothReceiver = new BroadcastReceiver() {
        @Override
        public void onReceive(Context context, Intent intent)
        {
            final String action = intent.getAction();
            if (action.equals(BluetoothAdapter.ACTION_STATE_CHANGED))
            {
                final int state = intent.getIntExtra(BluetoothAdapter.EXTRA_STATE,BluetoothAdapter.ERROR);
                switch (state)
                {
                    case BluetoothAdapter.STATE_OFF:
//                        closeConnection();//Close on going connection and disable button
                        Intent enableBtIntent = new Intent(BluetoothAdapter.ACTION_REQUEST_ENABLE);
                        startActivityForResult(enableBtIntent, REQUEST_ENABLE_BT);
                        break;
                    case BluetoothAdapter.STATE_ON:
                        break;
                }
            }
        }
    };

    private static class MHandler extends Handler {
        WeakReference<MainActivity> mActivity;
        MHandler(MainActivity activity) {
            mActivity = new WeakReference<>(activity);
        }
        @Override
        public void handleMessage(Message msg) {
            MainActivity theActivity = mActivity.get();
            switch (msg.what) {

                case Global.CMD_POS_STEXTOUTRESULT:
                case Global.CMD_POS_WRITERESULT: {
                    int result = msg.arg1;
                    Toast.makeText(
                            theActivity,
                            (result == 1) ? Global.toast_success
                                    : Global.toast_fail, Toast.LENGTH_SHORT).show();
                    Log.v(TAG, "Result: " + result);
                    break;
                }

            }
        }
    }


}
