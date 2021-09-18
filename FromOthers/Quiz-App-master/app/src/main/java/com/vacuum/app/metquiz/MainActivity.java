package com.vacuum.app.metquiz;

import android.arch.persistence.room.Room;
import android.content.ActivityNotFoundException;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.graphics.Typeface;
import android.net.Uri;
import android.os.Bundle;
import android.support.design.widget.NavigationView;
import android.support.design.widget.Snackbar;
import android.support.v4.app.Fragment;
import android.support.v4.app.FragmentTransaction;
import android.support.v4.view.GravityCompat;
import android.support.v4.widget.DrawerLayout;
import android.support.v7.app.ActionBarDrawerToggle;
import android.support.v7.app.AppCompatActivity;
import android.support.v7.widget.SearchView;
import android.support.v7.widget.Toolbar;
import android.text.Spannable;
import android.text.SpannableString;
import android.text.style.ForegroundColorSpan;
import android.view.Menu;
import android.view.MenuItem;
import android.view.SubMenu;
import android.view.View;
import android.widget.ImageView;
import android.widget.RelativeLayout;
import android.widget.TextView;
import android.widget.Toast;

import com.bumptech.glide.Glide;
import com.bumptech.glide.request.RequestOptions;
import com.vacuum.app.metquiz.Fragments.HomeFragment;
import com.vacuum.app.metquiz.Fragments.NotifyFragment;
import com.vacuum.app.metquiz.NavigationDrawer.AboutUsActivity;
import com.vacuum.app.metquiz.NavigationDrawer.CustomTypefaceSpan;
import com.vacuum.app.metquiz.NavigationDrawer.FavoriteFragment;
import com.vacuum.app.metquiz.NavigationDrawer.PrivacyPolicyActivity;
import com.vacuum.app.metquiz.NavigationDrawer.SettingsFragment;
import com.vacuum.app.metquiz.NavigationDrawer.TermsConditions;
import com.vacuum.app.metquiz.Splash.SplashScreen;
import com.vacuum.app.metquiz.Utils.MyDatabase;

import uk.co.chrisjenx.calligraphy.CalligraphyConfig;
import uk.co.chrisjenx.calligraphy.CalligraphyContextWrapper;
import static com.vacuum.app.metquiz.Splash.SplashScreen.MY_PREFS_NAME;


public class MainActivity extends AppCompatActivity {

    Context mContext;
    public Toolbar mToolbar;
    public ImageView notify_layout;
    public SearchView editsearch;
    public RelativeLayout red_badge;
    //=======================================================
    public static NavigationView navigationView;
    private DrawerLayout drawer;
    private View navHeader;
    private ImageView imgNavHeaderBg, imgProfile;
    private TextView txtName, txtWebsite;
    ActionBarDrawerToggle actionBarDrawerToggle;
    View parentLayout;
    // index to identify current nav menu item
    public static int navItemIndex = 0;

    // tags used to attach the fragments
    public static final String TAG_HOME = "home";
    public static final String TAG_browse = "browse";
    public static final String TAG_Barcodescanner = "barcodescanner";
    public static final String TAG_NOTIFICATIONS = "notifications";
    public static final String TAG_SETTINGS = "settings";
    public static final String TAG_QUESTIONS = "QUESTIONS";

    public static String CURRENT_TAG = TAG_HOME;
    // toolbar titles respected to selected nav menu item
    public static String[] activityTitles;

    // flag to load home fragment when user presses back key
    private boolean shouldLoadHomeFragOnBackPress = true;


    public static MainActivity INSTANCE;
    private static final String DATABASE_NAME = "MyDatabase";
    private static final String PREFERENCES = "RoomDemo.preferences";
    private static final String KEY_FORCE_UPDATE = "force_update";
    private MyDatabase database;
    public static MainActivity get() {
        return INSTANCE;
    }
    public MyDatabase getDB() {
        return database;
    }
    public boolean isForceUpdate() {
        return getSP().getBoolean(KEY_FORCE_UPDATE, true);
    }

    public void setForceUpdate(boolean force) {
        SharedPreferences.Editor edit = getSP().edit();
        edit.putBoolean(KEY_FORCE_UPDATE, force);
        edit.apply();
    }

    private SharedPreferences getSP() {
        return getSharedPreferences(PREFERENCES, MODE_PRIVATE);
    }


    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
        mContext = this.getApplicationContext();
        parentLayout = findViewById(android.R.id.content);
        mToolbar =  findViewById(R.id.toolbar);
        setSupportActionBar(mToolbar);
        editsearch =  findViewById(R.id.search_view);
        notify_layout =  findViewById(R.id.image);
        red_badge =  findViewById(R.id.red_badge);
        drawer =  findViewById(R.id.drawer_layout);
        navigationView =  findViewById(R.id.nav_view);
        // Navigation view header
        navHeader = navigationView.getHeaderView(0);
        txtName =  navHeader.findViewById(R.id.name);
        txtWebsite =  navHeader.findViewById(R.id.website);
        imgNavHeaderBg =  navHeader.findViewById(R.id.img_header_bg);
        imgProfile =  navHeader.findViewById(R.id.img_profile);



        // create database
        database = Room.databaseBuilder(getApplicationContext(), MyDatabase.class, DATABASE_NAME)
                .addMigrations(MyDatabase.MIGRATION_1_2)
                .build();

        INSTANCE = this;

        CalligraphyConfig.initDefault(new CalligraphyConfig.Builder()
                .setDefaultFontPath("fonts/airbnb.ttf")
                .setFontAttrId(R.attr.fontPath)
                .build()
        );

        notify_layout.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                //Toast.makeText(MainActivity.this, "Notify", Toast.LENGTH_LONG).show();
                //red_badge.setVisibility(View.GONE);
                NotifyFragment notifyFragment = new NotifyFragment();
                getSupportFragmentManager().beginTransaction()
                        .replace(R.id.container, notifyFragment,TAG_NOTIFICATIONS)
                        .commitAllowingStateLoss();
                navItemIndex = 3;
                CURRENT_TAG = TAG_NOTIFICATIONS;
                selectNavMenu();
                setToolbarTitle();
            }
        });
        //================================================

        // load toolbar titles from string resources
        activityTitles = getResources().getStringArray(R.array.nav_item_activity_titles);


        Snackbar.make(parentLayout, "Rate our app", Snackbar.LENGTH_LONG)
                        .setAction("Rate", new View.OnClickListener() {
                            @Override
                            public void onClick(View view) {
                                Rate_us();
                            }
                        }).show();

        loadNavHeader();
        setUpNavigationView();
        if (savedInstanceState == null) {
            navItemIndex = 0;
            CURRENT_TAG = TAG_HOME;
            loadHomeFragment();
            actionBarDrawerToggle.syncState();
        }

        //==================================================
        Menu m = navigationView.getMenu();
        for (int i=0;i<m.size();i++) {
            MenuItem mi = m.getItem(i);

            //for aapplying a font to subMenu ...
            SubMenu subMenu = mi.getSubMenu();
            if (subMenu!=null && subMenu.size() >0 ) {
                for (int j=0; j <subMenu.size();j++) {
                    MenuItem subMenuItem = subMenu.getItem(j);
                    applyFontToMenuItem(subMenuItem);
                }
            }
            applyFontToMenuItem(mi);
        }
    }
    @Override
    public boolean onCreateOptionsMenu(Menu menu) {
        if (navItemIndex == 2) {
            getMenuInflater().inflate(R.menu.favourite, menu);
        }else if (navItemIndex == 3) {
            getMenuInflater().inflate(R.menu.notifications, menu);
        }else {
            getMenuInflater().inflate(R.menu.menu_menu, menu);
        }

        //============================================================
        for(int i=0;i<menu.size();i++)
        {
            MenuItem mi = menu.getItem(i);
            applyFontToMenuItem(mi);
        }

        /*Menu menu1 = navigationView.getMenu();
        Drawable yourdrawable = menu1.getItem(4).getIcon(); // change 0 with 1,2 ...
        yourdrawable.mutate();
        yourdrawable.setColorFilter(getResources().getColor(R.color.premium), PorterDuff.Mode.SRC_ATOP);*/
        return true;
    }
    @Override
    public boolean onOptionsItemSelected(MenuItem item) {
        // Handle item selection
        int id = item.getItemId();

        if (id == R.id.log_out) {
            log_out();
        }
        if (id == R.id.action_mark_all_read) {
            Toast.makeText(getApplicationContext(), "All notifications marked as read!", Toast.LENGTH_LONG).show();
        }
        if (id == R.id.action_clear_notifications) {
            Toast.makeText(getApplicationContext(), "Clear all notifications!", Toast.LENGTH_LONG).show();
        }

        if (id == R.id.about) {
            startActivity(new Intent(MainActivity.this, AboutUsActivity.class));
            drawer.closeDrawers();
        }
        if (id == R.id.settings) {
            navItemIndex = 4;
            CURRENT_TAG = TAG_SETTINGS;
            loadHomeFragment();
        }
        if (id == R.id.Notification) {
            navItemIndex = 3;
            CURRENT_TAG = TAG_NOTIFICATIONS;
            loadHomeFragment();
        }
        return super.onOptionsItemSelected(item);
    }
    @Override
    public void onBackPressed() {
        if (!editsearch.isIconified()) {
            editsearch.onActionViewCollapsed();
            loadHomeFragment();
        }

        if (drawer.isDrawerOpen(GravityCompat.START)) {
            drawer.closeDrawers();
            return;
        }


        if (shouldLoadHomeFragOnBackPress) {
            // checking if user is on other navigation menu
            // rather than home
            if (navItemIndex != 0) {
                navItemIndex = 0;
                CURRENT_TAG = TAG_HOME;
                loadHomeFragment();
                return ;
            }
        }
        if (CURRENT_TAG == TAG_HOME) {
            super.onBackPressed();
        }

    }



    //====================================================================================
    //====================================================================================
    //====================================================================================

    private void loadNavHeader() {
        // name, website

        SharedPreferences prefs = getSharedPreferences(MY_PREFS_NAME, MODE_PRIVATE);
        String name = prefs.getString("fname", "@mohamedebrahim96");//"No name defined" is the default value.
        String email = prefs.getString("email", "ebrahimm132@gmail.com");
        String imageurl = prefs.getString("imageurl", "https://avatars2.githubusercontent.com/u/16405013?s=460&v=4");
        String cover = prefs.getString("cover", "defaultStringIfNothingFound");


        txtName.setText(name);
        txtWebsite.setText(email);

        RequestOptions options = RequestOptions
                .circleCropTransform()
                .placeholder(R.drawable.thin128);
        RequestOptions options2 = new RequestOptions()
                .placeholder(R.drawable.nav);
        Glide.with(this).load(imageurl)
                    .thumbnail(0.5f)
                    .apply(options)
                    .into(imgProfile);
        Glide.with(this)
                //.load(urlNavHeaderBg)
                .load(cover)
                .apply(options2)
                .into(imgNavHeaderBg);
        navigationView.getMenu().getItem(3).setActionView(R.layout.menu_dot);
        navigationView.setItemIconTintList(null);

    }

    /***
     * Returns respected fragment that user
     * selected from navigation menu
     */
    private void loadHomeFragment() {
        // selecting appropriate nav menu item
        selectNavMenu();

        // set toolbar title
        setToolbarTitle();

        // if user select the current navigation menu again, don't do anything
        // just close the navigation drawer
        if (getSupportFragmentManager().findFragmentByTag(CURRENT_TAG) != null) {
            drawer.closeDrawers();

            // show or hide the fab button
            return;
        }

        // Sometimes, when fragment has huge data, screen seems hanging
        // when switching between navigation menus
        // So using runnable, the fragment is loaded with cross fade effect
        // This effect can be seen in GMail app

                // update the favourite content by replacing fragments
                Fragment fragment = getHomeFragment();
                FragmentTransaction fragmentTransaction = getSupportFragmentManager().beginTransaction();
                fragmentTransaction.setCustomAnimations(android.R.anim.fade_in,
                        android.R.anim.fade_out);
                fragmentTransaction.replace(R.id.container, fragment, CURRENT_TAG);
                fragmentTransaction.commitAllowingStateLoss();


        //Closing drawer on item click
        drawer.closeDrawers();

        // refresh toolbar menu
        invalidateOptionsMenu();
    }

    private Fragment getHomeFragment() {
        switch (navItemIndex) {
            case 0:
                // home
                HomeFragment homeFragment = new HomeFragment();
                return homeFragment;
            case 1:
                // photos
                HomeFragment homeFragment2 = new HomeFragment();
                return homeFragment2;
            case 2:
                // movies fragment
                FavoriteFragment favoriteFragment = new FavoriteFragment();
                return favoriteFragment;
            case 3:
                // notifications fragment
                NotifyFragment notificationsFragment = new NotifyFragment();
                return notificationsFragment;

            case 4:
                // settings fragment
                SettingsFragment settingsFragment = new SettingsFragment();
                return settingsFragment;
            default:
                return new HomeFragment();
        }
    }

    public  void setToolbarTitle() {
        getSupportActionBar().setTitle(activityTitles[navItemIndex]);
    }

    public  void selectNavMenu() {
        navigationView.getMenu().getItem(navItemIndex).setChecked(true);
    }

    public void setUpNavigationView() {
        //Setting Navigation View Item Selected Listener to handle the item click of the navigation menu
        navigationView.setNavigationItemSelectedListener(new NavigationView.OnNavigationItemSelectedListener() {

            // This method will trigger on item Click of navigation menu
            @Override
            public boolean onNavigationItemSelected(MenuItem menuItem) {

                //Check to see which item was being clicked and perform appropriate action
                switch (menuItem.getItemId()) {
                    //Replacing the favourite content with ContentFragment Which is our Inbox View;
                    case R.id.nav_home:
                        navItemIndex = 0;
                        CURRENT_TAG = TAG_HOME;
                        break;
                    case R.id.nav_browse:
                        navItemIndex = 1;
                        CURRENT_TAG = TAG_browse;
                        break;
                    case R.id.nav_favourite:
                        navItemIndex = 2;
                        CURRENT_TAG = TAG_Barcodescanner;
                        break;
                    case R.id.nav_notifications:
                        navItemIndex = 3;
                        CURRENT_TAG = TAG_NOTIFICATIONS;
                        break;
                    case R.id.nav_settings:
                        navItemIndex = 4;
                        CURRENT_TAG = TAG_SETTINGS;
                        break;
                    case R.id.nav_about_us:
                        // launch new intent instead of loading fragment
                        startActivity(new Intent(MainActivity.this, AboutUsActivity.class));
                        drawer.closeDrawers();
                        return true;
                    case R.id.Terms_Conditions:
                        // launch new intent instead of loading fragment
                        startActivity(new Intent(MainActivity.this, TermsConditions.class));
                        overridePendingTransition(android.R.anim.fade_in, android.R.anim.fade_out);
                        drawer.closeDrawers();
                        return true;
                    case R.id.nav_privacy_policy:
                        // launch new intent instead of loading fragment
                        startActivity(new Intent(MainActivity.this, PrivacyPolicyActivity.class));
                        overridePendingTransition(R.anim.fade_in, R.anim.fade_out);
                        drawer.closeDrawers();
                        return true;
                    case R.id.log_out:
                        // launch new intent instead of loading fragment
                        log_out();
                        drawer.closeDrawers();
                        return true;
                    case R.id.gopremium:
                        Toast.makeText(mContext, "Go Premium", Toast.LENGTH_SHORT).show();
                        drawer.closeDrawers();
                        return true;
                    case R.id.rateus:
                        Rate_us();
                        drawer.closeDrawers();
                        return true;
                    default:
                        navItemIndex = 0;
                }

                //Checking if the item is in checked state or not, if not make it in checked state
                if (menuItem.isChecked()) {
                    menuItem.setChecked(false);
                } else {
                    menuItem.setChecked(true);
                }
                menuItem.setChecked(true);

                loadHomeFragment();

                return true;
            }
        });

        actionBarDrawerToggle = new ActionBarDrawerToggle(this, drawer, mToolbar, R.string.openDrawer, R.string.closeDrawer) {

            @Override
            public void onDrawerClosed(View drawerView) {
                // Code here will be triggered once the drawer closes as we dont want anything to happen so we leave this blank
                super.onDrawerClosed(drawerView);
            }

            @Override
            public void onDrawerOpened(View drawerView) {
                // Code here will be triggered once the drawer open as we dont want anything to happen so we leave this blank
                super.onDrawerOpened(drawerView);
            }
        };

        //Setting the actionbarToggle to drawer layout
        drawer.setDrawerListener(actionBarDrawerToggle);
        actionBarDrawerToggle.syncState();

        //calling sync state is necessary or else your hamburger icon wont show up
    }

    private void log_out() {
        SharedPreferences preferences = getSharedPreferences(MY_PREFS_NAME, MODE_PRIVATE);
        preferences.edit().remove("card_id").commit();
        startActivity(new Intent(MainActivity.this, SplashScreen.class));
    }

    private void applyFontToMenuItem(MenuItem mi) {

        if(mi.getItemId() != R.id.gopremium){
            Typeface font = Typeface.createFromAsset(getAssets(), "fonts/airbnb.ttf");
            SpannableString mNewTitle = new SpannableString(mi.getTitle());
            mNewTitle.setSpan(new CustomTypefaceSpan("" , font), 0 , mNewTitle.length(),  Spannable.SPAN_INCLUSIVE_INCLUSIVE);
            mi.setTitle(mNewTitle);
        }else{
            SpannableString s = new SpannableString(mi.getTitle());
            s.setSpan(new ForegroundColorSpan(getResources().getColor(R.color.premium)), 0, s.length(), 0);
            mi.setTitle(s);
        }

    }


    private void Rate_us() {
        Uri uri = Uri.parse("market://details?id=" + mContext.getPackageName());
        Intent goToMarket = new Intent(Intent.ACTION_VIEW, uri);
        // To count with Play market backstack, After pressing back button,
        // to taken back to our application, we need to add following flags to intent.
        goToMarket.addFlags(Intent.FLAG_ACTIVITY_NO_HISTORY |
                Intent.FLAG_ACTIVITY_NEW_DOCUMENT |
                Intent.FLAG_ACTIVITY_MULTIPLE_TASK);
        try {
            startActivity(goToMarket);
        } catch (ActivityNotFoundException e) {
            startActivity(new Intent(Intent.ACTION_VIEW,
                    Uri.parse("http://play.google.com/store/apps/details?id=" + mContext.getPackageName())));
        }
    }
    @Override
    protected void attachBaseContext(Context newBase) {
        super.attachBaseContext(CalligraphyContextWrapper.wrap(newBase));
    }
}
