package com.ldt.cinematicket.ui.inout;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.design.widget.TextInputLayout;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.Toast;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.FirebaseAuth;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.ui.main.MainActivity;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;

import java.util.Objects;

import butterknife.BindView;
import butterknife.ButterKnife;

public class ForgotPassword extends SupportFragment implements View.OnClickListener{
    private static final String TAG="ForgotPassword";
    FirebaseAuth mAuth;
    private View root;

    @BindView(R.id.edi_email) TextInputLayout ediEmail;
    @BindView(R.id.btn_send) Button btnSend;
    @BindView(R.id.btn_back) ImageView btnBack;

    public ForgotPassword() {
        // Required empty public constructor
    }

    public static ForgotPassword newInstance(){
        return new ForgotPassword();
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container) {
        return inflater.inflate(R.layout.forgot_password,container,false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        ButterKnife.bind(this,view);

        root = view;
        btnSend.setOnClickListener(this);
        btnBack.setOnClickListener(this);
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.btn_send:
                sendPasswordResetEmail();
                break;
            case R.id.btn_back:
                ((MainActivity)getActivity()).dismiss();
                break;
        }
    }

    private void sendPasswordResetEmail(){
        String email = Objects.requireNonNull(ediEmail.getEditText()).getText().toString().trim();

        if(email.isEmpty()){
            ediEmail.setError(getString(R.string.email_empty));
        }
        else{
            ediEmail.setError(null);
            mAuth = FirebaseAuth.getInstance();
            mAuth.sendPasswordResetEmail(email)
                    .addOnCompleteListener(new OnCompleteListener<Void>() {
                        @Override
                        public void onComplete(@NonNull Task<Void> task) {
                            if (task.isSuccessful()) {
                                Log.d(TAG, "Email sent.");
                                Toast.makeText(getContext(),R.string.mail_sent,Toast.LENGTH_LONG).show();
                                ((MainActivity)getActivity()).dismiss();
                            }
                            else{
                                Toast.makeText(getContext(),R.string.mail_fail,Toast.LENGTH_LONG).show();
                            }
                        }
                    });
        }
    }
}
