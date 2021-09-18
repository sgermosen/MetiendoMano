package com.ldt.cinematicket.ui.main.root;

import android.app.DatePickerDialog;
import android.app.Dialog;
import android.content.DialogInterface;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.design.widget.BottomSheetDialog;
import android.support.design.widget.TextInputEditText;
import android.support.design.widget.TextInputLayout;
import android.support.v7.app.AlertDialog;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.DatePicker;
import android.widget.ImageView;
import android.widget.RadioButton;
import android.widget.RadioGroup;
import android.widget.TextView;
import android.widget.Toast;

import com.bumptech.glide.Glide;
import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.OnSuccessListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.google.firebase.firestore.DocumentReference;
import com.google.firebase.firestore.DocumentSnapshot;
import com.google.firebase.firestore.FirebaseFirestore;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.UserInfo;
import com.ldt.cinematicket.ui.main.MainActivity;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.PresentStyle;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;
import com.makeramen.roundedimageview.RoundedImageView;

import java.util.Calendar;
import java.util.Objects;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class ProfileDetail extends SupportFragment {
    private static final String TAG="ProfileDetail";
    FirebaseAuth mAuth;
    FirebaseFirestore mDb;
    FirebaseUser user;
    UserInfo info;

    public static ProfileDetail newInstance() {
        return new ProfileDetail();
    }

    //My Account
    @BindView(R.id.avatar) RoundedImageView mAvatar;

    @BindView(R.id.edi_balance) TextView ediBalance;

    @BindView(R.id.edi_fullname) TextInputLayout ediFullname;
    @BindView(R.id.txt_fullname) TextInputEditText txtFullname;

    @BindView(R.id.edi_email) TextInputLayout ediEmail;
    @BindView(R.id.txt_email) TextInputEditText txtEmail;

    @BindView(R.id.txt_birthday) TextView txtBirthday;

    @BindView(R.id.rad_group_gender) RadioGroup radGroupGender;
    @BindView(R.id.rad_male) RadioButton radMale;
    @BindView(R.id.rad_female) RadioButton radFemale;
    @BindView(R.id.rad_other) RadioButton radOther;

    //Contact
    @BindView(R.id.edi_phonenumber) TextInputLayout ediPhoneNumber;
    @BindView(R.id.txt_phonenumber) TextInputEditText txtPhoneNumber;

    @BindView(R.id.edi_address) TextInputLayout ediAddress;
    @BindView(R.id.txt_address) TextInputEditText txtAddress;

    //Button
    @BindView(R.id.btn_save) Button btnSave;
    @BindView(R.id.btn_back) ImageView btnBack;

    @OnClick(R.id.txt_birthday)
    void setBirthday() {
        Calendar newCalendar = Calendar.getInstance();
        DatePickerDialog datePickerDialog = new DatePickerDialog(getContext(),
                new DatePickerDialog.OnDateSetListener() {
                    @Override
                    public void onDateSet(DatePicker datePicker, int year, int month, int day) {
                        String str_day = Integer.toString(day);
                        String str_month = Integer.toString(month + 1);
                        if (1 == str_day.length()) {
                            str_day = "0" + str_day;
                        }
                        if (1 == str_month.length()) {
                            str_month = "0" + str_month;
                        }
                        txtBirthday.setText(str_day + "/" + str_month + "/" + year);
                    }
                },newCalendar.get(Calendar.YEAR), newCalendar.get(Calendar.MONTH),  newCalendar.get(Calendar.DAY_OF_MONTH));
        datePickerDialog.getDatePicker().setMaxDate(System.currentTimeMillis());
        datePickerDialog.show();
    }

    @OnClick(R.id.btn_save)
    void saveInfo() {
        confirmProfileChange();    }

    @OnClick(R.id.btn_back)
    void back() {
        alertUser();
    }

    private void alertUser(){
        BottomSheetDialog dialog = new BottomSheetDialog(getActivity());

        dialog.setContentView(R.layout.alert_layout);
        dialog.findViewById(R.id.comfirm).setOnClickListener(v -> { dialog.dismiss(); getMainActivity().dismiss();});
        dialog.show();
    }

    @Nullable
    @Override
    protected View onCreateView(LayoutInflater inflater, ViewGroup container) {

        return inflater.inflate(R.layout.profile_detail,container,false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this,view);

        mAuth = FirebaseAuth.getInstance();
        user = mAuth.getCurrentUser();

        mDb = getMainActivity().mDb;
        info = new UserInfo();
        getUserInfo();
    }

    @Override
    public int getPresentTransition() {
        return PresentStyle.SLIDE_LEFT;
    }

    private void updateUserInfo(){
        String fullname = Objects.requireNonNull(ediFullname.getEditText()).getText().toString().trim();
        String email = Objects.requireNonNull(ediEmail.getEditText()).getText().toString().trim();

        String birthDay = Objects.requireNonNull(txtBirthday.getText().toString().trim());
        String gender;
        if(radMale.isChecked()){
            gender = "male";
        }
        else if(radFemale.isChecked()){
            gender = "female";
        }
        else{
            gender = "other";
        }
        String phoneNumber = Objects.requireNonNull(ediPhoneNumber.getEditText()).getText().toString().trim();
        String address = Objects.requireNonNull(ediAddress.getEditText()).getText().toString().trim();

        info.setId(user.getUid());
        info.setFullName(fullname);
        info.setEmail(email);
        info.setBirthDay(birthDay);
        info.setGender(gender);
        info.setPhoneNumber(phoneNumber);
        info.setAddress(address);

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

    private void getUserInfo(){
        String id = user.getUid();

        DocumentReference userGet = mDb.collection("user_info").document(id);
        userGet.get()
                .addOnSuccessListener(new OnSuccessListener<DocumentSnapshot>() {
                    @Override
                    public void onSuccess(DocumentSnapshot documentSnapshot) {
                        info = documentSnapshot.toObject(UserInfo.class);
                        updateProfileUI();
                    }
                });
    }

    private void updateProfileUI(){
        if(info.getAvaUrl().matches("")){
            Glide.with(this)
                    .load(R.drawable.movie_pop_corn)
                    .into(mAvatar);
        }
        else{
            Glide.with(this)
                    .load(Uri.parse(info.getAvaUrl()))
                    .into(mAvatar);
        }
        ediBalance.setText(Integer.toString(info.getBalance()));
        txtFullname.setText(info.getFullName());
        txtEmail.setText(info.getEmail());
        txtBirthday.setText(info.getBirthDay());
        if(info.getGender().matches("male")){
            radMale.toggle();
        }
        else if(info.getGender().matches("female")){
            radFemale.toggle();
        }
        else if(info.getGender().matches("other")){
            radOther.toggle();
        }
        txtPhoneNumber.setText(info.getPhoneNumber());
        txtAddress.setText(info.getAddress());
    }

    private void confirmProfileChange(){
        AlertDialog builder = new AlertDialog.Builder(getContext()).create(); //Use context
        builder.setTitle(R.string.confirm_dialog_title);
        builder.setMessage(getString(R.string.confirm_dialog_description));
        builder.setButton(Dialog.BUTTON_POSITIVE,getString(R.string.confirm_dialog_yes), new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                updateUserInfo();
                Toast.makeText(getContext(),R.string.profile_updated,Toast.LENGTH_SHORT).show();
                ((MainActivity)getActivity()).dismiss();
            }
        });
        builder.setButton(Dialog.BUTTON_NEGATIVE,getString(R.string.confirm_dialog_cancel), new DialogInterface.OnClickListener() {
            public void onClick(DialogInterface dialog, int which) {
                dialog.cancel();
            }
        });
        builder.show();
    }
}
