package com.ldt.cinematicket.ui.main.root;

import android.net.Uri;
import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.support.design.widget.CoordinatorLayout;
import android.support.design.widget.FloatingActionButton;
import android.support.v4.app.Fragment;
import android.support.v7.widget.SwitchCompat;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.bumptech.glide.Glide;
import com.google.firebase.auth.FirebaseAuth;
import com.google.firebase.auth.FirebaseUser;
import com.ldt.cinematicket.R;
import com.ldt.cinematicket.data.MyPrefs;
import com.ldt.cinematicket.ui.inout.LogIn;
import com.ldt.cinematicket.ui.main.MainActivity;
import com.ldt.cinematicket.ui.main.admin.DashBoard;
import com.ldt.cinematicket.ui.main.root.ProfileDetail;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnCheckedChanged;
import butterknife.OnClick;

public class ProfileTab extends Fragment {
    private static final String TAG ="ProfileTab";
    MyPrefs myPrefs;
    FirebaseAuth mAuth;
    FirebaseUser user;

    public static ProfileTab newInstance() {
        ProfileTab fragment = new ProfileTab();
        return fragment;
    }
    // Profile Detail
    @BindView(R.id.profile_detail_panel) View mProfileDetailPanel;
    @BindView(R.id.profile_detail) View mProfileDetail;
    @BindView(R.id.next_profile_detail) View mProfileDetailNext;

    // Control Center
    @BindView(R.id.control_center_panel) View mControlCenterPanel;
    @BindView(R.id.control_center) View mControlCenter;
    @BindView(R.id.next_control_center) View mControlCenterNext;

    // Theme
    @BindView(R.id.theme_panel) View mThemePanel;
    @BindView(R.id.theme) View mTheme;
    @BindView(R.id.switch_theme) SwitchCompat mThemeSwitch;

    @BindView(R.id.avatar) ImageView mAvatarView;
    @BindView(R.id.user_id) TextView mDisplayName;
    @BindView(R.id.txt_suggest) TextView mSuggestion;
    @BindView(R.id.sign_in) FloatingActionButton mAddAccountButton;

    @BindView(R.id.btn_sign_out) Button btnSignout;

    @OnClick(R.id.control_center_panel)
    void goToControlCenter() {
        ((MainActivity)getActivity()).presentFragment(DashBoard.newInstance());
    }

    @OnClick(R.id.profile_detail_panel)
    void goToInfoDetail() {
        ((MainActivity)getActivity()).presentFragment(ProfileDetail.newInstance());
    }

    @OnClick(R.id.theme_panel)
    void changeTheme() {
        mThemeSwitch.setChecked(!mThemeSwitch.isChecked());
    }

    @OnCheckedChanged(R.id.switch_theme)
    void updateTheme() {
        boolean isDark = mThemeSwitch.isChecked();
        Toast.makeText(getContext(),"Theme switched to "+((isDark)? "Dark Mode":"Light Mode"),Toast.LENGTH_SHORT).show();
    }

    @OnClick({R.id.avatar,R.id.sign_in})
    void avatarClick() {
        if(!myPrefs.getIsSignIn()){
            ((MainActivity)getActivity()).presentFragment(LogIn.newInstance());
        }
        else{
            Toast.makeText(getContext(),"Not yet",Toast.LENGTH_LONG).show();
        }
    }

    @OnClick({R.id.btn_sign_out})
    void signOut() {
        mAuth.signOut();
        myPrefs.setIsSignIn(false);
        myPrefs.setIsAdmin(false);
        getUserInfo();
        updateSignInOutUI();
        ((MainActivity)getActivity()).restartHomeScreen();
    }

    @Nullable
    @Override
    public View onCreateView(@NonNull LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        return inflater.inflate(R.layout.profile_tab,container,false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this,view);

        myPrefs = new MyPrefs(getContext());

        mAuth = FirebaseAuth.getInstance();

        getUserInfo();
        updateSignInOutUI();
    }

    private void getUserInfo(){
        user = ((MainActivity)getActivity()).user;
        if (user != null) {
            // Name, email address, and profile photo Url
            String name = user.getDisplayName();
            String email = user.getEmail();
            Uri photoUrl = user.getPhotoUrl();

            if(photoUrl == null){
                Glide.with(this)
                        .load(R.drawable.movie_pop_corn)
                        .into(mAvatarView);
            }
            else{
                Glide.with(this)
                        .load(Uri.parse(photoUrl.toString()))
                        .into(mAvatarView);
            }


            if(name == null || name.matches("")){
                mDisplayName.setText(email);
            }
            else{
                mDisplayName.setText(name);
            }
        }
        else{
            mDisplayName.setText("@anonymous");
            Glide.with(this)
                    .load(R.drawable.movie_pop_corn)
                    .into(mAvatarView);
        }
    }

    private void updateSignInOutUI(){
        if(myPrefs.getIsSignIn()){
            mAddAccountButton.hide();
            mSuggestion.setVisibility(View.GONE);
            btnSignout.setVisibility(View.VISIBLE);
            mProfileDetail.setVisibility(View.VISIBLE);
            mProfileDetailNext.setVisibility(View.VISIBLE);
            if(!myPrefs.getIsAdmin()){
                mControlCenter.setVisibility(View.GONE);
                mControlCenterNext.setVisibility(View.GONE);
            }
            else{
                mControlCenter.setVisibility(View.VISIBLE);
                mControlCenterNext.setVisibility(View.VISIBLE);
            }
        }
        else{
            mAddAccountButton.show();
            mSuggestion.setVisibility(View.VISIBLE);
            btnSignout.setVisibility(View.GONE);
            mProfileDetail.setVisibility(View.GONE);
            mProfileDetailNext.setVisibility(View.GONE);
            mControlCenter.setVisibility(View.GONE);
            mControlCenterNext.setVisibility(View.GONE);
        }
    }
}
