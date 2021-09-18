package com.ldt.cinematicket.ui.main.root;

import android.support.annotation.NonNull;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;

import com.google.android.gms.tasks.OnCompleteListener;
import com.google.android.gms.tasks.Task;
import com.google.firebase.firestore.FirebaseFirestore;
import com.google.firebase.firestore.QueryDocumentSnapshot;
import com.google.firebase.firestore.QuerySnapshot;

public class FireBaseActivity extends AppCompatActivity {
    private static final String TAG ="FireBaseActivity";

    // Access a Cloud Firestore instance from your Activity
    public FirebaseFirestore mDb = FirebaseFirestore.getInstance();

}
