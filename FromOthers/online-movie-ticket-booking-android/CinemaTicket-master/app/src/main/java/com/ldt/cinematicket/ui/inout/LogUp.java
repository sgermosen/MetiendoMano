package com.ldt.cinematicket.ui.inout;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.design.widget.BottomSheetDialog;
import android.support.design.widget.TextInputLayout;
import android.text.SpannableString;
import android.text.method.LinkMovementMethod;
import android.text.style.ClickableSpan;
import android.text.style.ForegroundColorSpan;
import android.text.style.UnderlineSpan;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.TextView;
import android.widget.Toast;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.AuthResult;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.firestore.FirebaseFirestore;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.data.MyPrefs;
import com.ldt.cinematicket.model.UserInfo;
import com.ldt.cinematicket.ui.main.MainActivity;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;

import java.util.ArrayList;
import java.util.Objects;

import butterknife.BindView;
import butterknife.ButterKnife;

public class LogUp extends SupportFragment implements View.OnClickListener{
    private static final String TAG="LogUp";
    private View root;
    FirebaseAuth mAuth;
    MyPrefs myPrefs;
    FirebaseFirestore mDb;

    public LogUp() {
        // Required empty public constructor
    }

    @BindView(R.id.edi_fullname) TextInputLayout ediFullname;
    @BindView(R.id.edi_email) TextInputLayout ediEmail;
    @BindView(R.id.edi_password) TextInputLayout ediPassword;
    @BindView(R.id.edi_retype) TextInputLayout ediRetype;
    @BindView(R.id.chb_terms_privacy) CheckBox chbConfirm;
    @BindView(R.id.txt_terms_privacy) TextView txtConfirm;
    @BindView(R.id.sign_in) View mSignIn;
    @BindView(R.id.btn_create) Button mCreate;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container) {
        // Inflate the layout for this fragment

        return inflater.inflate(R.layout.user_log_up, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        ButterKnife.bind(this,view);

        root = view;
        mSignIn.setOnClickListener(this);
        mCreate.setOnClickListener(this);

        focusOnTermsAndPrivacy();

        mAuth = FirebaseAuth.getInstance();
        mDb = getMainActivity().mDb;

        myPrefs = new MyPrefs(getContext());

    }

    public static LogUp newInstance() {
        return new LogUp();
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.btn_create:
                signUp();
                break;
            case R.id.sign_in:
                alertUser();
                break;
        }
    }

    private void signUp() {
        String fullname = Objects.requireNonNull(ediFullname.getEditText()).getText().toString().trim();
        String email = Objects.requireNonNull(ediEmail.getEditText()).getText().toString().trim();
        String password = Objects.requireNonNull(ediPassword.getEditText()).getText().toString().trim();
        String retype = Objects.requireNonNull(ediRetype.getEditText()).getText().toString().trim();

        if(validateInfo(fullname,email,password,retype)){
            mAuth.createUserWithEmailAndPassword(email, password)
                    .addOnCompleteListener(getActivity(), new OnCompleteListener<AuthResult>() {
                        @Override
                        public void onComplete(@NonNull Task<AuthResult> task) {
                            if (task.isSuccessful()) {
                                // Sign in success, update UI with the signed-in user's information
                                Log.d(TAG, "createUserWithEmail:success");
                                myPrefs.setIsSignIn(true);
                                Toast.makeText(getContext(), R.string.signup_success,Toast.LENGTH_SHORT).show();
                                addNewUser();
                                ((MainActivity)getActivity()).dismiss();
                                ((MainActivity)getActivity()).restartHomeScreen();
                            } else {
                                // If sign in fails, display a message to the user.
                                Log.w(TAG, "createUserWithEmail:failure", task.getException());
                                Toast.makeText(getContext(), "Authentication failed.",
                                        Toast.LENGTH_SHORT).show();
                            }
                        }
                    });
        }
    }

    private boolean validateInfo(String fullname, String email, String password, String retype) {
        Boolean validate = true;

        if(fullname.isEmpty()){
            ediFullname.setError(getString(R.string.fullname_empty));
            validate = false;
        }
        else{   ediFullname.setError(null);  }

        if(email.isEmpty()){
            ediEmail.setError(getString(R.string.email_empty));
            validate = false;
        }
        else if(!isValidEmail(email)){
            ediEmail.setError(getString(R.string.email_invalid));
            validate = false;
        }
        else{   ediEmail.setError(null);    }

        if(password.isEmpty()){
            ediPassword.setError(getString(R.string.password_empty));
            validate = false;
        }
        else if(password.length()<6){
            ediPassword.setError(getString(R.string.password_length));
            validate = false;
        }
        else{   ediPassword.setError(null); }

        if(retype.isEmpty()){
            ediRetype.setError(getString(R.string.retype_empty));
            validate = false;
        }
        else if(!retype.matches(password)){
            ediRetype.setError(getString(R.string.retype_match));
            validate = false;
        }
        else{   ediRetype.setError(null); }

        if(validate){
            if(!chbConfirm.isChecked()){
                Toast.makeText(getContext(),R.string.terms_confirm,Toast.LENGTH_LONG).show();
                validate = false;
            }
        }

        return validate;
    }

    public boolean isValidEmail(String email) {
        if (email == null) {
            return false;
        } else {
            return android.util.Patterns.EMAIL_ADDRESS.matcher(email).matches();
        }
    }

    private void alertUser(){
        BottomSheetDialog dialog = new BottomSheetDialog(getActivity());

        dialog.setContentView(R.layout.alert_layout);
        dialog.findViewById(R.id.comfirm).setOnClickListener(v -> {
            dialog.dismiss();
            getMainActivity().dismiss();
            ((MainActivity)getActivity()).presentFragment(LogIn.newInstance());
        });
        dialog.show();
    }

    private void focusOnTermsAndPrivacy(){
        SpannableString SpanString = new SpannableString(
                getString(R.string.terms_privacy));

        ClickableSpan terms = new ClickableSpan() {
            @Override
            public void onClick(View textView) {
                Toast.makeText(getContext(),R.string.is_updating,Toast.LENGTH_SHORT).show();
            }
        };

        ClickableSpan privacy = new ClickableSpan() {
            @Override
            public void onClick(View textView) {
                Toast.makeText(getContext(),R.string.is_updating,Toast.LENGTH_SHORT).show();
            }
        };

        SpanString.setSpan(terms, 41, 59, 0);
        SpanString.setSpan(privacy, 63, 81, 0);
        SpanString.setSpan(new ForegroundColorSpan(getResources().getColor(R.color.quite_red)), 41, 59, 0);
        SpanString.setSpan(new ForegroundColorSpan(getResources().getColor(R.color.quite_red)), 63, 81, 0);
        SpanString.setSpan(new UnderlineSpan(), 41, 59, 0);
        SpanString.setSpan(new UnderlineSpan(), 63, 81, 0);

        txtConfirm.setMovementMethod(LinkMovementMethod.getInstance());
        txtConfirm.setText(SpanString, TextView.BufferType.SPANNABLE);
        txtConfirm.setSelected(true);
    }

    private void addNewUser(){
        UserInfo info = new UserInfo();
        FirebaseUser user = mAuth.getCurrentUser();

        String fullname = Objects.requireNonNull(ediFullname.getEditText()).getText().toString().trim();
        String email = Objects.requireNonNull(ediEmail.getEditText()).getText().toString().trim();

        info.setAvaUrl("");
        info.setUserType("");
        info.setId(user.getUid());
        info.setFullName(fullname);
        info.setEmail(email);
        info.setBirthDay("");
        info.setGender("");
        info.setPhoneNumber("");
        info.setAddress("");
        info.setBalance(0);
        ArrayList<Integer> idTicket = new ArrayList<>();
        info.setIdTicket(idTicket);

        sendUserInfo(info);
    }

    private void sendUserInfo(UserInfo user){
        mDb.collection("user_info").document(user.getId())
                .set(user)
                .addOnSuccessListener(aVoid -> {
                    Log.w(TAG, "addUserToDatabase:success");
                })
                .addOnFailureListener(e -> {
                    Log.w(TAG, "addUserToDatabase:failure", e);
                });
    }
}
