package com.ldt.cinematicket.ui.inout;

import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.design.widget.TextInputEditText;
import android.support.design.widget.TextInputLayout;
import android.support.v4.app.Fragment;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.CheckBox;
import android.widget.Toast;

import com.facebook.AccessToken;
import com.facebook.CallbackManager;
import com.facebook.FacebookCallback;
import com.facebook.FacebookException;
import com.facebook.login.LoginManager;
import com.facebook.login.LoginResult;
import com.google.android.gms.auth.api.Auth;
import com.google.android.gms.auth.api.signin.GoogleSignIn;
import com.google.android.gms.auth.api.signin.GoogleSignInAccount;
import com.google.android.gms.auth.api.signin.GoogleSignInOptions;
import com.google.android.gms.common.ConnectionResult;
import com.google.android.gms.common.api.ApiException;
import com.google.android.gms.common.api.GoogleApiClient;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.AuthCredential;
import com.google.firebase.auth.AuthResult;
import com.google.firebase.auth.FacebookAuthProvider;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.auth.GoogleAuthProvider;
import com.google.firebase.firestore.DocumentReference;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.FirebaseFirestore;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.data.MyPrefs;
import com.ldt.cinematicket.model.UserInfo;
import com.ldt.cinematicket.ui.main.MainActivity;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;

import java.util.ArrayList;
import java.util.Arrays;
import java.util.Objects;

import butterknife.BindView;
import butterknife.ButterKnife;

/**
 * A simple {@link Fragment} subclass.
 */
public class LogIn extends SupportFragment implements View.OnClickListener {
    private static final String TAG="LogIn";
    private FirebaseAuth mAuth;
    private static final int RC_SIGN_IN = 9001;
    private View root;
    private GoogleApiClient mGoogleSignInClient;
    private CallbackManager mCallbackManager;
    MyPrefs myPrefs;
    FirebaseFirestore mDb;

    public LogIn() {
        // Required empty public constructor
    }

    @BindView(R.id.btn_sign_in) Button btnSignIn;
    @BindView(R.id.sign_up) View mSignUp;
    @BindView(R.id.btn_google) Button btnGoogle;
    @BindView(R.id.btn_facebook) Button btnFacebook;
    @BindView(R.id.edi_email) TextInputLayout ediEmail;
    @BindView(R.id.txt_email) TextInputEditText txtEmail;
    @BindView(R.id.edi_password) TextInputLayout ediPassword;
    @BindView(R.id.txt_password) TextInputEditText txtPassword;
    @BindView(R.id.chb_remember) CheckBox chbRemember;
    @BindView(R.id.forgot_password) View mForgot;

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container) {
        // Inflate the layout for this fragment

        return inflater.inflate(R.layout.log_in, container, false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        ButterKnife.bind(this,view);

        myPrefs = new MyPrefs(getContext());

        root = view;
        mForgot.setOnClickListener(this);
        chbRemember.setOnClickListener(this);
        btnSignIn.setOnClickListener(this);
        btnGoogle.setOnClickListener(this);
        btnFacebook.setOnClickListener(this);
        mSignUp.setOnClickListener(this);

        // Initialize Firebase Auth
        mAuth = FirebaseAuth.getInstance();
        mDb = getMainActivity().mDb;

        if(myPrefs.getIsRememberMe()){
            String[] acc = myPrefs.getAccount();
            txtEmail.setText(acc[0]);
            txtPassword.setText(acc[1]);
            chbRemember.setChecked(true);
        }
    }

    public static LogIn newInstance() {
        return new LogIn();
    }

    @Override
    public void onClick(View view) {
        switch (view.getId()) {
            case R.id.btn_sign_in:
                signIn();
                break;
            case R.id.sign_up:
                ((MainActivity)getActivity()).dismiss();
                ((MainActivity)getActivity()).presentFragment(LogUp.newInstance());
                break;
            case R.id.btn_google:
                googleSignIn();
                break;
            case R.id.btn_facebook:
                facebookSignIn();
                break;
            case R.id.forgot_password:
                ((MainActivity)getActivity()).presentFragment(ForgotPassword.newInstance());
                break;
        }
    }

    private void signIn() {
        String email = Objects.requireNonNull(ediEmail.getEditText()).getText().toString().trim();
        String password = Objects.requireNonNull(ediPassword.getEditText()).getText().toString().trim();
        String[] acc = {email,password};
        myPrefs.setAccount(acc);

        if(chbRemember.isChecked()){
            myPrefs.setIsRememberMe(true);
        }
        else{
            myPrefs.setIsRememberMe(false);
        }

        if(validateAccount(email,password)){
            mAuth.signInWithEmailAndPassword(email, password)
                    .addOnCompleteListener(getActivity(), new OnCompleteListener<AuthResult>() {
                        @Override
                        public void onComplete(@NonNull Task<AuthResult> task) {
                            if (task.isSuccessful()) {
                                // Sign in success
                                Log.d(TAG, "signInWithEmail:success");
                                Toast.makeText(getContext(), R.string.signin_success, Toast.LENGTH_LONG).show();
                                myPrefs.setIsSignIn(true);
                                ((MainActivity)getActivity()).restartHomeScreen();
                            } else {
                                // If sign in fails, display a message to the user.
                                Log.w(TAG, "signInWithEmail:failure", task.getException());
                                Toast.makeText(getContext(), R.string.signin_error,Toast.LENGTH_LONG).show();
                            }
                        }
                    });
        }
    }

    private void googleSignIn() {
        GoogleSignInOptions gso = new GoogleSignInOptions.Builder(GoogleSignInOptions.DEFAULT_SIGN_IN)
                .requestEmail()
                .requestProfile()
                .requestIdToken(getString(R.string.server_client_id))
                .build();

        mGoogleSignInClient = new GoogleApiClient.Builder(getActivity())
                .enableAutoManage(getActivity(), new GoogleApiClient.OnConnectionFailedListener() {
                    @Override
                    public void onConnectionFailed(@NonNull ConnectionResult connectionResult) {
                        Toast.makeText(getContext(),R.string.signin_error,Toast.LENGTH_LONG).show();
                    }
                })
                .addApi(Auth.GOOGLE_SIGN_IN_API,gso)
                .build();

        Intent signInIntent = Auth.GoogleSignInApi.getSignInIntent(mGoogleSignInClient);
        startActivityForResult(signInIntent, RC_SIGN_IN);
    }

    private void facebookSignIn(){
        mCallbackManager = CallbackManager.Factory.create();

        if (AccessToken.getCurrentAccessToken() != null) {
            LoginManager.getInstance().logOut();
        }

        LoginManager.getInstance().logInWithReadPermissions(this, Arrays.asList("email","public_profile"));
        LoginManager.getInstance().registerCallback(mCallbackManager,new FacebookCallback<LoginResult>() {
            @Override
            public void onSuccess(LoginResult loginResult) {
                Log.d("Success", "Login");
                handleFacebookAccessToken(loginResult.getAccessToken());
            }

            @Override
            public void onCancel() {
                Toast.makeText(getContext(), R.string.signin_cancel, Toast.LENGTH_LONG).show();
            }

            @Override
            public void onError(FacebookException exception) {
                Toast.makeText(getContext(), R.string.signin_error + exception.getMessage(), Toast.LENGTH_LONG).show();
            }
        });
    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, Intent data) {
        super.onActivityResult(requestCode, resultCode, data);

        // Result returned from launching the Intent from GoogleSignInApi.getSignInIntent(...);
        if (requestCode == RC_SIGN_IN) {
            Task<GoogleSignInAccount> task = GoogleSignIn.getSignedInAccountFromIntent(data);
            try {
                // Google Sign In was successful, authenticate with Firebase
                Toast.makeText(getContext(), R.string.signin_success, Toast.LENGTH_LONG).show();
                GoogleSignInAccount account = task.getResult(ApiException.class);
                firebaseAuthWithGoogle(account);
                mGoogleSignInClient.clearDefaultAccountAndReconnect();
            } catch (ApiException e) {
                // Google Sign In failed, update UI appropriately
                Log.w(TAG, "Google sign in failed", e);
                // ...
            }
        }
        else{
            mCallbackManager.onActivityResult(requestCode, resultCode, data);
        }
    }

    private boolean validateAccount(String email, String password){
        Boolean validate = true;

        if(email.isEmpty()){
            ediEmail.setError(getString(R.string.email_empty));
            validate = false;
        }
        else if(!isValidEmail(email)){
            ediEmail.setError(getString(R.string.email_invalid));
            validate = false;
        }
        else{   ediEmail.setError(null);        }

        if(password.isEmpty()){
            ediEmail.setError(null);
            ediPassword.setError(getString(R.string.password_empty));
            validate = false;
        }
        else if(password.length()<6){
            ediEmail.setError(null);
            ediPassword.setError(getString(R.string.password_length));
            validate = false;
        }
        else{   ediPassword.setError(null);    }

        return validate;
    }

    public boolean isValidEmail(String email) {
        if (email == null) {
            return false;
        } else {
            return android.util.Patterns.EMAIL_ADDRESS.matcher(email).matches();
        }
    }

    private void firebaseAuthWithGoogle(GoogleSignInAccount account) {
        AuthCredential credential = GoogleAuthProvider.getCredential(account.getIdToken(), null);
        mAuth.signInWithCredential(credential)
                .addOnCompleteListener(getActivity(), new OnCompleteListener<AuthResult>() {
                    @Override
                    public void onComplete(@NonNull Task<AuthResult> task) {
                        if (!task.isSuccessful()) {
                            // If sign in fails, display a message to the user.
                            Log.w(TAG, "signInWithCredential:failure", task.getException());
                            Toast.makeText(getContext(),"Authentication Failed",Toast.LENGTH_LONG).show();
                        }
                        else{
                            Log.d(TAG, "signInWithCredential:success");
                            myPrefs.setIsSignIn(true);
                            checkIfFirstTimeSignIn();
                        }
                    }
                });
    }

    private void handleFacebookAccessToken(AccessToken token) {
        Log.d(TAG, "handleFacebookAccessToken:" + token);

        AuthCredential credential = FacebookAuthProvider.getCredential(token.getToken());
        mAuth.signInWithCredential(credential)
                .addOnCompleteListener(getActivity(), new OnCompleteListener<AuthResult>() {
                    @Override
                    public void onComplete(@NonNull Task<AuthResult> task) {
                        if (task.isSuccessful()) {
                            // Sign in success, update UI with the signed-in user's information
                            Log.d(TAG, "signInWithCredential:success");
                            Toast.makeText(getContext(), R.string.signin_success,Toast.LENGTH_SHORT).show();
                            myPrefs.setIsSignIn(true);
                            checkIfFirstTimeSignIn();
                        } else {
                            // If sign in fails, display a message to the user.
                            Log.w(TAG, "signInWithCredential:failure", task.getException());
                            Toast.makeText(getContext(), R.string.signin_error,Toast.LENGTH_SHORT).show();
                        }
                    }
                });
    }

    private void addNewUser(UserInfo info, FirebaseUser user){
        String fullname = "";
        if(user.getDisplayName() != null && !user.getDisplayName().matches("")){
            fullname = user.getDisplayName();
        }

        if(user.getPhotoUrl() != null){
            info.setAvaUrl(user.getPhotoUrl().toString());
        }
        else{
            info.setAvaUrl("");
        }

        info.setUserType("");
        info.setId(user.getUid());
        info.setFullName(fullname);
        if(user.getEmail() == null){
            info.setEmail("");
        }
        else{
            info.setEmail(user.getEmail());
        }
        info.setBirthDay("");
        info.setGender("");
        info.setPhoneNumber("");
        info.setAddress("");
        info.setBalance(0);
        ArrayList<Integer> idTicket = new ArrayList<>();
        info.setIdTicket(idTicket);

        Log.d(TAG, "New User Created");

        sendUserInfo(info);
    }

    private void updateOldUser(UserInfo info, FirebaseUser user){
        String fullname = "";
        if(user.getDisplayName() != null && !user.getDisplayName().matches("")){
            fullname = user.getDisplayName();
        }

        if(user.getPhotoUrl() != null){
            info.setAvaUrl(user.getPhotoUrl().toString());
        }
        else{
            info.setAvaUrl("");
        }

        info.setFullName(fullname);
        if(user.getEmail() == null){
            info.setEmail("");
        }
        else{
            info.setEmail(user.getEmail());
        }

        Log.d(TAG, "User Updated");

        sendUserInfo(info);
    }

    private void sendUserInfo(UserInfo info){
        mDb.collection("user_info").document(info.getId())
                .set(info)
                .addOnSuccessListener(aVoid -> {
                    Log.w(TAG, "addUserToDatabase:success");
                })
                .addOnFailureListener(e -> {
                    Log.w(TAG, "addUserToDatabase:failure", e);
                });
    }

    private void checkIfFirstTimeSignIn(){
        FirebaseUser user = mAuth.getCurrentUser();

        DocumentReference docRef = mDb.collection("user_info").document(user.getUid());
        docRef.get().addOnCompleteListener(new OnCompleteListener<DocumentSnapshot>() {
            @Override
            public void onComplete(@NonNull Task<DocumentSnapshot> task) {
                if (task.isSuccessful()) {
                    DocumentSnapshot document = task.getResult();
                    if (document.exists()) {
                        Log.d(TAG, "DocumentSnapshot data: " + document.getData());
                        UserInfo info = new UserInfo();
                        info = document.toObject(UserInfo.class);
                        updateOldUser(info,user);
                        ((MainActivity)getActivity()).restartHomeScreen();
                    }
                    else {
                        Log.d(TAG, "No such document");
                        UserInfo info = new UserInfo();
                        addNewUser(info,user);
                        ((MainActivity)getActivity()).restartHomeScreen();
                    }
                } else {
                    Log.d(TAG, "get failed with ", task.getException());
                }
            }
        });
    }
}
