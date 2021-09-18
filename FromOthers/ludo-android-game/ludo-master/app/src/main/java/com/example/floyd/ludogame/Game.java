package com.example.floyd.ludogame;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.RelativeLayout;
import android.widget.ImageView;

import java.util.Random;

public class Game extends Activity implements View.OnClickListener {

    public int height, width, top, bottom, d, n, PlayerNo;
    public int x1 = 1, x2 = 1, x3 = 1, x4 = 1, x5 = 14, x6 = 14, x7 = 14, x8 = 14, x9 = 27, x10 = 27, x11 = 27, x12 = 27, x13 = 40, x14 = 40, x15 = 40, x16 = 40;
    public CanvasBoardDraw db;
    public ImageView iv, iv1, iv2, iv3, iv4, iv5, iv6, iv7, iv8, iv9, iv10, iv11, iv12, iv13, iv14, iv15, iv16;
    public int n1, n2, n3, n4;
    public int extraN;
    public int r1, r2, r3, r4 = 0, b1, b2, b3, b4 = 0, g1, g2, g3, g4 = 0, y1, y2, y3, y4 = 0;
    public int r, b, g, y = 0;
    public ImageView player1, player2, player3, player4;

    @Override
    protected void onCreate(Bundle savedInstanceState) {

        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_game);
        db = (CanvasBoardDraw) findViewById(R.id.custom_canvas_1);
        assign();
        Fill();
        Fill1(iv1);
        Fill1(iv2);
        Fill1(iv3);
        Fill1(iv4);
        Fill2(iv5);
        Fill2(iv6);
        Fill2(iv7);
        Fill2(iv8);
        Fill3(iv9);
        Fill3(iv10);
        Fill3(iv11);
        Fill3(iv12);
        Fill4(iv13);
        Fill4(iv14);
        Fill4(iv15);
        Fill4(iv16);
        StartGame();

    }

    public void assign() {

        height = getResources().getDisplayMetrics().heightPixels;
        width = getResources().getDisplayMetrics().widthPixels;
        top = (height - width) / 2;
        bottom = top + width / 2;
        d = width / 15;

        player1 = (ImageView) findViewById(R.id.player1);
        player2 = (ImageView) findViewById(R.id.player2);
        player3 = (ImageView) findViewById(R.id.player3);
        player4 = (ImageView) findViewById(R.id.player4);

        iv = (ImageView) findViewById(R.id.roll);

        iv1 = (ImageView) findViewById(R.id.red1);
        iv2 = (ImageView) findViewById(R.id.red2);
        iv3 = (ImageView) findViewById(R.id.red3);
        iv4 = (ImageView) findViewById(R.id.red4);
        iv5 = (ImageView) findViewById(R.id.green1);
        iv6 = (ImageView) findViewById(R.id.green2);
        iv7 = (ImageView) findViewById(R.id.green3);
        iv8 = (ImageView) findViewById(R.id.green4);
        iv9 = (ImageView) findViewById(R.id.blue1);
        iv10 = (ImageView) findViewById(R.id.blue2);
        iv11 = (ImageView) findViewById(R.id.blue3);
        iv12 = (ImageView) findViewById(R.id.blue4);
        iv13 = (ImageView) findViewById(R.id.yellow1);
        iv14 = (ImageView) findViewById(R.id.yellow2);
        iv15 = (ImageView) findViewById(R.id.yellow3);
        iv16 = (ImageView) findViewById(R.id.yellow4);

    }

    public void Fill() {

        iv.getLayoutParams().height = 3 * d;
        iv.getLayoutParams().width = 3 * d;
        RelativeLayout.LayoutParams mParams = (RelativeLayout.LayoutParams) iv.getLayoutParams();
        mParams.leftMargin = 6 * d;
        mParams.topMargin = top + 6 * d;
        iv.setLayoutParams(mParams);

    }

    public void Fill1(ImageView v) {

        switch (v.getId()) {

            case R.id.red1:
                iv1.getLayoutParams().height = d;
                iv1.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams1 = (RelativeLayout.LayoutParams) iv1.getLayoutParams();
                mParams1.leftMargin = 3 * d / 2;
                mParams1.topMargin = top + 3 * d / 2;
                iv1.setLayoutParams(mParams1);
                r1 = 0;x1=1;
                break;
            case R.id.red2:
                iv2.getLayoutParams().height = d;
                iv2.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams2 = (RelativeLayout.LayoutParams) iv2.getLayoutParams();
                mParams2.leftMargin = 2 * d + 3 * d / 2;
                mParams2.topMargin = top + 3 * d / 2;
                iv2.setLayoutParams(mParams2);
                r2 = 0;x2=1;
                break;
            case R.id.red3:
                iv3.getLayoutParams().height = d;
                iv3.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams3 = (RelativeLayout.LayoutParams) iv3.getLayoutParams();
                mParams3.leftMargin = 3 * d / 2;
                mParams3.topMargin = 2 * d + top + 3 * d / 2;
                iv3.setLayoutParams(mParams3);
                r3 = 0;x3=1;
                break;
            case R.id.red4:
                iv4.getLayoutParams().height = d;
                iv4.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams4 = (RelativeLayout.LayoutParams) iv4.getLayoutParams();
                mParams4.leftMargin = 2 * d + 3 * d / 2;
                mParams4.topMargin = 2 * d + top + 3 * d / 2;
                iv4.setLayoutParams(mParams4);
                r4 = 0;x4=1;
                break;
        }

    }

    public void Fill2(ImageView v) {

        switch (v.getId()) {

            case R.id.green1:
                iv5.getLayoutParams().height = d;
                iv5.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams1 = (RelativeLayout.LayoutParams) iv5.getLayoutParams();
                mParams1.leftMargin = 9 * d + 3 * d / 2;
                mParams1.topMargin = top + 3 * d / 2;
                iv5.setLayoutParams(mParams1);
                g1 = 0;x5=14;
                break;
            case R.id.green2:
                iv6.getLayoutParams().height = d;
                iv6.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams2 = (RelativeLayout.LayoutParams) iv6.getLayoutParams();
                mParams2.leftMargin = 11 * d + 3 * d / 2;
                mParams2.topMargin = top + 3 * d / 2;
                iv6.setLayoutParams(mParams2);
                g2 = 0;x6=14;
                break;
            case R.id.green3:
                iv7.getLayoutParams().height = d;
                iv7.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams3 = (RelativeLayout.LayoutParams) iv7.getLayoutParams();
                mParams3.leftMargin = 9 * d + 3 * d / 2;
                mParams3.topMargin = 2 * d + top + 3 * d / 2;
                iv7.setLayoutParams(mParams3);
                g3 = 0;x7=14;
                break;
            case R.id.green4:
                iv8.getLayoutParams().height = d;
                iv8.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams4 = (RelativeLayout.LayoutParams) iv8.getLayoutParams();
                mParams4.leftMargin = 11 * d + 3 * d / 2;
                mParams4.topMargin = 2 * d + top + 3 * d / 2;
                iv8.setLayoutParams(mParams4);
                g4 = 0;x8=14;
                break;
        }
    }

    public void Fill3(ImageView v) {

        switch (v.getId()) {

            case R.id.blue1:
                iv9.getLayoutParams().height = d;
                iv9.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams1 = (RelativeLayout.LayoutParams) iv9.getLayoutParams();
                mParams1.leftMargin = 9 * d + 3 * d / 2;
                mParams1.topMargin = 9 * d + top + 3 * d / 2;
                iv9.setLayoutParams(mParams1);
                b1 = 0;x9=27;
                break;
            case R.id.blue2:
                iv10.getLayoutParams().height = d;
                iv10.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams2 = (RelativeLayout.LayoutParams) iv10.getLayoutParams();
                mParams2.leftMargin = 11 * d + 3 * d / 2;
                mParams2.topMargin = 9 * d + top + 3 * d / 2;
                iv10.setLayoutParams(mParams2);
                b2 = 0;x10=27;
                break;
            case R.id.blue3:
                iv11.getLayoutParams().height = d;
                iv11.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams3 = (RelativeLayout.LayoutParams) iv11.getLayoutParams();
                mParams3.leftMargin = 9 * d + 3 * d / 2;
                mParams3.topMargin = 11 * d + top + 3 * d / 2;
                iv11.setLayoutParams(mParams3);
                b3 = 0;x11=27;
                break;
            case R.id.blue4:
                iv12.getLayoutParams().height = d;
                iv12.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams4 = (RelativeLayout.LayoutParams) iv12.getLayoutParams();
                mParams4.leftMargin = 11 * d + 3 * d / 2;
                mParams4.topMargin = 11 * d + top + 3 * d / 2;
                iv12.setLayoutParams(mParams4);
                b4 = 0;x12=27;
                break;
        }
    }

    public void Fill4(ImageView v) {

        switch (v.getId()) {
            case R.id.yellow1:
                iv13.getLayoutParams().height = d;
                iv13.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams1 = (RelativeLayout.LayoutParams) iv13.getLayoutParams();
                mParams1.leftMargin = 3 * d / 2;
                mParams1.topMargin = 9 * d + top + 3 * d / 2;
                iv13.setLayoutParams(mParams1);
                y1 = 0;x13=40;
                break;
            case R.id.yellow2:
                iv14.getLayoutParams().height = d;
                iv14.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams2 = (RelativeLayout.LayoutParams) iv14.getLayoutParams();
                mParams2.leftMargin = 2 * d + 3 * d / 2;
                mParams2.topMargin = 9 * d + top + 3 * d / 2;
                iv14.setLayoutParams(mParams2);
                y2 = 0;x14=40;
                break;
            case R.id.yellow3:
                iv15.getLayoutParams().height = d;
                iv15.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams3 = (RelativeLayout.LayoutParams) iv15.getLayoutParams();
                mParams3.leftMargin = 3 * d / 2;
                mParams3.topMargin = 11 * d + top + 3 * d / 2;
                iv15.setLayoutParams(mParams3);
                y3 = 0;x15=40;
                break;
            case R.id.yellow4:
                iv16.getLayoutParams().height = d;
                iv16.getLayoutParams().width = d;
                RelativeLayout.LayoutParams mParams4 = (RelativeLayout.LayoutParams) iv16.getLayoutParams();
                mParams4.leftMargin = 2 * d + 3 * d / 2;
                mParams4.topMargin = 11 * d + top + 3 * d / 2;
                iv16.setLayoutParams(mParams4);
                y4 = 0;x16=40;
                break;
        }
    }

    @Override
    public void onClick(View v) {

        v.bringToFront();

        RelativeLayout.LayoutParams mP = (RelativeLayout.LayoutParams) v.getLayoutParams();
        switch (v.getId()) {

            case R.id.red1:

                if (r1 == 0) {
                    mP.leftMargin = d;
                    mP.topMargin = top + 6 * d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    r1 = 1;
                } else x1 = PositionOf(x1, iv1);
                checkPosition(iv1);
                break;

            case R.id.red2:

                if (r2 == 0) {
                    mP.leftMargin = d;
                    mP.topMargin = top + 6 * d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    r2 = 1;
                } else x2 = PositionOf(x2, iv2);
                checkPosition(iv2);
                break;

            case R.id.red3:

                if (r3 == 0) {
                    mP.leftMargin = d;
                    mP.topMargin = top + 6 * d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    r3 = 1;
                } else x3 = PositionOf(x3, iv3);
                checkPosition(iv4);
                break;

            case R.id.red4:

                if (r4 == 0) {
                    mP.leftMargin = d;
                    mP.topMargin = top + 6 * d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    r4 = 1;
                } else x4 = PositionOf(x4, iv4);
                checkPosition(iv4);
                break;

            case R.id.green1:

                if (g1 == 0) {
                    mP.leftMargin = 8 * d;
                    mP.topMargin = top + d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    g1 = 1;
                } else x5 = PositionOf(x5, iv5);
                checkPosition(iv5);
                break;

            case R.id.green2:

                if (g2 == 0) {
                    mP.leftMargin = 8 * d;
                    mP.topMargin = top + d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    g2 = 1;
                } else x6 = PositionOf(x6, iv6);
                checkPosition(iv6);
                break;

            case R.id.green3:

                if (g3 == 0) {
                    mP.leftMargin = 8 * d;
                    mP.topMargin = top + d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    g3 = 1;
                } else x7 = PositionOf(x7, iv7);
                checkPosition(iv7);
                break;

            case R.id.green4:

                if (g4 == 0) {
                    mP.leftMargin = 8 * d;
                    mP.topMargin = top + d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    g4 = 1;
                } else x8 = PositionOf(x8, iv8);
                checkPosition(iv8);
                break;

            case R.id.blue1:

                if (b1 == 0) {
                    mP.leftMargin = 13 * d;
                    mP.topMargin = top + 8 * d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    b1 = 1;
                } else x9 = PositionOf(x9, iv9);
                checkPosition(iv9);
                break;

            case R.id.blue2:

                if (b2 == 0) {
                    mP.leftMargin = 13 * d;
                    mP.topMargin = top + 8 * d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    b2 = 1;
                } else x10 = PositionOf(x10, iv10);
                checkPosition(iv10);
                break;

            case R.id.blue3:

                if (b3 == 0) {
                    mP.leftMargin = 13 * d;
                    mP.topMargin = top + 8 * d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    b3 = 1;
                } else x11 = PositionOf(x11, iv11);
                checkPosition(iv11);
                break;

            case R.id.blue4:

                if (b4 == 0) {
                    mP.leftMargin = 13 * d;
                    mP.topMargin = top + 8 * d;
                    v.setLayoutParams(mP);
                    b4 = 1;
                } else x12 = PositionOf(x12, iv12);
                checkPosition(iv12);
                break;

            case R.id.yellow1:

                if (y1 == 0) {
                    mP.leftMargin = 6 * d;
                    mP.topMargin = top + 13 * d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    y1 = 1;
                } else x13 = PositionOf(x13, iv13);
                checkPosition(iv13);
                break;

            case R.id.yellow2:

                if (y2 == 0) {
                    mP.leftMargin = 6 * d;
                    mP.topMargin = top + 13 * d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    y2 = 1;
                } else x14 = PositionOf(x14, iv14);
                checkPosition(iv14);
                break;

            case R.id.yellow3:

                if (y3 == 0) {
                    mP.leftMargin = 6 * d;
                    mP.topMargin = top + 13 * d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    y3 = 1;
                } else x15 = PositionOf(x15, iv15);
                checkPosition(iv15);
                break;

            case R.id.yellow4:

                if (y4 == 0) {
                    mP.leftMargin = 6 * d;
                    mP.topMargin = top + 13 * d;
                    v.setLayoutParams(mP);
                    SetDiceClickable();
                    y4 = 1;
                } else x16 = PositionOf(x16, iv16);
                checkPosition(iv16);
                break;

            case R.id.roll:
                n = GenerateRandom();
                RollDice(n);
//                Toast.makeText(getApplicationContext(), n + "was obtained", Toast.LENGTH_SHORT).show();
                int pt = PlayerNo;
                SetClickableFalse();
                iv.setClickable(false);
                if (PlayerNo % 4 == 1) {
                    if (r1 == 1) {
                        iv1.setOnClickListener(this);
                    }
                    if (r2 == 1) {
                        iv2.setOnClickListener(this);
                    }
                    if (r3 == 1) {
                        iv3.setOnClickListener(this);
                    }
                    if (r4 == 1) {
                        iv4.setOnClickListener(this);
                    }
                    if (n == 1) {
                        r = 1;
                        iv1.setOnClickListener(this);
                        iv2.setOnClickListener(this);
                        iv3.setOnClickListener(this);
                        iv4.setOnClickListener(this);
                    } else if (n == 6 && r == 0) {
                        PlayerNo++;
                    } else if (n == 2 || n == 3 || n == 4 || n == 5) {
                        PlayerNo++;
                    }

                    n1 = x1 - 51 + n;
                    n2 = x2 - 51 + n;
                    n3 = x3 - 51 + n;
                    n4 = x4 - 51 + n;
                    if (n1 == 6) {
                        iv1.setVisibility(View.INVISIBLE);
                        x1 = 0;
                        SetDiceClickable();
                        PlayerNo = pt;
                    }
                    if (n2 == 6) {
                        iv2.setVisibility(View.INVISIBLE);
                        x2 = 0;
                        SetDiceClickable();
                        PlayerNo = pt;
                    }
                    if (n3 == 6) {
                        iv3.setVisibility(View.INVISIBLE);
                        x3 = 0;
                        SetDiceClickable();
                        PlayerNo = pt;
                    }
                    if (n4 == 6) {
                        iv4.setVisibility(View.INVISIBLE);
                        x4 = 0;
                        SetDiceClickable();
                        PlayerNo = pt;
                    }
                    if (n1 > 5 || iv1.getVisibility() == View.INVISIBLE)
                        iv1.setClickable(false);
                    if (n2 > 5 || iv2.getVisibility() == View.INVISIBLE)
                        iv2.setClickable(false);
                    if (n3 > 5 || iv3.getVisibility() == View.INVISIBLE)
                        iv3.setClickable(false);
                    if (n4 > 5 || iv4.getVisibility() == View.INVISIBLE)
                        iv4.setClickable(false);
                    if (!iv1.isClickable() && !iv2.isClickable() && !iv3.isClickable() && !iv4.isClickable() || r == 0) {
                        SetDiceClickable();
                    }
                } else if (PlayerNo % 4 == 2) {
                    if (g1 == 1) {
                        iv5.setOnClickListener(this);
                    }
                    if (g2 == 1) {
                        iv6.setOnClickListener(this);
                    }
                    if (g3 == 1) {
                        iv7.setOnClickListener(this);
                    }
                    if (g4 == 1) {
                        iv8.setOnClickListener(this);
                    }
                    if (n == 1) {
                        g = 1;
                        iv5.setOnClickListener(this);
                        iv6.setOnClickListener(this);
                        iv7.setOnClickListener(this);
                        iv8.setOnClickListener(this);
                    } else if (n == 6 && g == 0) {
                        PlayerNo++;
                    } else if (n == 2 || n == 3 || n == 4 || n == 5) {
                        PlayerNo++;
                    }

                    n1 = n + x5 - 12;
                    n2 = n + x6 - 12;
                    n3 = n + x7 - 12;
                    n4 = n + x8 - 12;
                    if (n1 == 6 && iv5.getTop() < top + 7 * d && iv5.getLeft() == 7 * d) {
                        iv5.setVisibility(View.INVISIBLE);
                        x5 = 0;
                        SetDiceClickable();
                        PlayerNo = pt;
                    }
                    if (n2 == 6 && iv6.getTop() < top + 7 * d && iv6.getLeft() == 7 * d) {
                        iv6.setVisibility(View.INVISIBLE);
                        x6 = 0;
                        SetDiceClickable();
                        PlayerNo = pt;
                    }
                    if (n3 == 6 && iv7.getTop() < top + 7 * d && iv7.getLeft() == 7 * d) {
                        iv7.setVisibility(View.INVISIBLE);
                        x7 = 0;
                        SetDiceClickable();
                        PlayerNo = pt;
                    }
                    if (n4 == 6 && iv8.getTop() < top + 7 * d && iv8.getLeft() == 7 * d) {
                        iv8.setVisibility(View.INVISIBLE);
                        x8 = 0;
                        SetDiceClickable();
                        PlayerNo = pt;
                    }

                    if (n1 > 5 && iv5.getTop() < top + 7 * d && iv5.getLeft() == 7 * d || iv5.getVisibility() == View.INVISIBLE)
                        iv5.setClickable(false);
                    if (n2 > 5 && iv6.getTop() < top + 7 * d && iv6.getLeft() == 7 * d || iv6.getVisibility() == View.INVISIBLE)
                        iv6.setClickable(false);
                    if (n3 > 5 && iv7.getTop() < top + 7 * d && iv7.getLeft() == 7 * d || iv7.getVisibility() == View.INVISIBLE)
                        iv7.setClickable(false);
                    if (n4 > 5 && iv8.getTop() < top + 7 * d && iv8.getLeft() == 7 * d || iv8.getVisibility() == View.INVISIBLE)
                        iv8.setClickable(false);
                    if (!iv5.isClickable() && !iv6.isClickable() && !iv7.isClickable() && !iv8.isClickable() || g == 0) {
                        SetDiceClickable();
                    }
                } else if (PlayerNo % 4 == 3) {
                    if (b1 == 1) {
                        iv9.setOnClickListener(this);
                    }
                    if (b2 == 1) {
                        iv10.setOnClickListener(this);
                    }
                    if (b3 == 1) {
                        iv11.setOnClickListener(this);
                    }
                    if (b4 == 1) {
                        iv12.setOnClickListener(this);
                    }
                    if (n == 1) {
                        b = 1;
                        iv9.setOnClickListener(this);
                        iv10.setOnClickListener(this);
                        iv11.setOnClickListener(this);
                        iv12.setOnClickListener(this);
                    } else if (n == 6 && b == 0) {
                        PlayerNo++;
                    } else if (n == 2 || n == 3 || n == 4 || n == 5) {
                        PlayerNo++;
                    }
                    n1 = n + x9 - 25;
                    n2 = n + x10 - 25;
                    n3 = n + x11 - 25;
                    n4 = n + x12 - 25;
                    if (n1 == 6 && iv9.getLeft() > 7 * d && iv9.getTop() == top + 7 * d) {
                        iv9.setVisibility(View.INVISIBLE);
                        PlayerNo = pt;
                        x9 = 0;
                        SetDiceClickable();
                    }
                    if (n2 == 6 && iv10.getLeft() > 7 * d && iv10.getTop() == top + 7 * d) {
                        iv10.setVisibility(View.INVISIBLE);
                        x10 = 0;
                        PlayerNo = pt;
                        SetDiceClickable();
                    }
                    if (n3 == 6 && iv11.getLeft() > 7 * d && iv11.getTop() == top + 7 * d) {
                        iv11.setVisibility(View.INVISIBLE);
                        PlayerNo = pt;
                        x11 = 0;
                        SetDiceClickable();
                    }
                    if (n4 == 6 && iv12.getLeft() > 7 * d && iv12.getTop() == top + 7 * d) {
                        iv12.setVisibility(View.INVISIBLE);
                        PlayerNo = pt;
                        x12 = 0;
                        SetDiceClickable();
                    }

                    if (n1 > 5 && iv9.getLeft() > 7 * d && iv9.getTop() == top + 7 * d || iv9.getVisibility() == View.INVISIBLE)
                        iv9.setClickable(false);
                    if (n2 > 5 && iv10.getLeft() > 7 * d && iv10.getTop() == top + 7 * d || iv10.getVisibility() == View.INVISIBLE)
                        iv10.setClickable(false);
                    if (n3 > 5 && iv11.getLeft() > 7 * d && iv11.getTop() == top + 7 * d || iv11.getVisibility() == View.INVISIBLE)
                        iv11.setClickable(false);
                    if (n4 > 5 && iv12.getLeft() > 7 * d && iv12.getTop() == top + 7 * d || iv12.getVisibility() == View.INVISIBLE)
                        iv12.setClickable(false);
                    if (!iv9.isClickable() && !iv10.isClickable() && !iv11.isClickable() && !iv12.isClickable() || b == 0) {
                        SetDiceClickable();
                    }
                } else if (PlayerNo % 4 == 0) {
                    if (y1 == 1) {
                        iv13.setOnClickListener(this);
                    }
                    if (y2 == 1) {
                        iv14.setOnClickListener(this);
                    }
                    if (y3 == 1) {
                        iv15.setOnClickListener(this);
                    }
                    if (y4 == 1) {
                        iv16.setOnClickListener(this);
                    }
                    if (n == 1) {
                        y = 1;
                        iv13.setOnClickListener(this);
                        iv14.setOnClickListener(this);
                        iv15.setOnClickListener(this);
                        iv16.setOnClickListener(this);
                    } else if (n == 6 && y == 0) {
                        PlayerNo++;
                    } else if (n == 2 || n == 3 || n == 4 || n == 5) {
                        PlayerNo++;
                    }
                    n1 = n + x13 - 38;
                    n2 = n + x14 - 38;
                    n3 = n + x15 - 38;
                    n4 = n + x16 - 38;
                    if (n1 == 6 && iv13.getTop() > top + 6 * d && iv13.getLeft() == 7 * d) {
                        iv13.setVisibility(View.INVISIBLE);
                        PlayerNo = pt;
                        x13 = 0;
                        SetDiceClickable();
                    }
                    if (n2 == 6 && iv14.getTop() > top + 6 * d && iv14.getLeft() == 7 * d) {
                        iv14.setVisibility(View.INVISIBLE);
                        PlayerNo = pt;
                        x14 = 0;
                        SetDiceClickable();
                    }
                    if (n3 == 6 && iv15.getTop() > top + 6 * d && iv15.getLeft() == 7 * d) {
                        iv15.setVisibility(View.INVISIBLE);
                        PlayerNo = pt;
                        x15 = 0;
                        SetDiceClickable();
                    }
                    if (n4 == 6 && iv16.getTop() > top + 6 * d && iv16.getLeft() == 7 * d) {
                        iv16.setVisibility(View.INVISIBLE);
                        PlayerNo = pt;
                        x16 = 0;
                        SetDiceClickable();
                    }

                    if (n1 > 5 && iv13.getTop() > top + 6 * d && iv13.getLeft() == 7 * d || iv13.getVisibility() == View.INVISIBLE)
                        iv13.setClickable(false);
                    if (n2 > 5 && iv14.getTop() > top + 6 * d && iv14.getLeft() == 7 * d || iv14.getVisibility() == View.INVISIBLE)
                        iv14.setClickable(false);
                    if (n3 > 5 && iv15.getTop() > top + 6 * d && iv15.getLeft() == 7 * d || iv15.getVisibility() == View.INVISIBLE)
                        iv15.setClickable(false);
                    if (n4 > 5 && iv16.getTop() > top + 6 * d && iv16.getLeft() == 7 * d || iv16.getVisibility() == View.INVISIBLE)
                        iv16.setClickable(false);
                    if (!iv13.isClickable() && !iv14.isClickable() && !iv15.isClickable() && !iv16.isClickable() || y == 0) {
                        SetDiceClickable();
                    }
                }

                break;
        }

        if (iv1.getVisibility()==View.INVISIBLE && iv2.getVisibility()==View.INVISIBLE &&
                iv3.getVisibility()==View.INVISIBLE && iv4.getVisibility()==View.INVISIBLE
                ){
            Intent in=new Intent(Game.this, GameOverRed.class);
            startActivity(in);
        }
        if (iv5.getVisibility()==View.INVISIBLE && iv6.getVisibility()==View.INVISIBLE &&
                iv7.getVisibility()==View.INVISIBLE && iv8.getVisibility()==View.INVISIBLE
                ){
            Intent in=new Intent(Game.this, GameOverGreen.class);
            startActivity(in);
        }
        if (iv9.getVisibility()==View.INVISIBLE && iv10.getVisibility()==View.INVISIBLE &&
                iv11.getVisibility()==View.INVISIBLE && iv12.getVisibility()==View.INVISIBLE
                ){
            //game won by playerthree
            Intent in=new Intent(Game.this, GameOverBlue.class);
            startActivity(in);
        }
        if (iv13.getVisibility()==View.INVISIBLE && iv14.getVisibility()==View.INVISIBLE &&
                iv15.getVisibility()==View.INVISIBLE && iv16.getVisibility()==View.INVISIBLE
                ){
            //game won by playerfour
            Intent in=new Intent(Game.this, GameOverYellow.class);
            startActivity(in);
        }
    }

    private void SetDiceClickable() {

        SetClickableFalse();
        iv.setClickable(true);
        iv.setOnClickListener(this);
        iv.setImageResource(R.drawable.icon);

        if (PlayerNo % 4 == 1) {
            player1.setImageResource(R.drawable.circle1);
            player2.setImageResource(R.drawable.circle);
            player3.setImageResource(R.drawable.circle);
            player4.setImageResource(R.drawable.circle);
        } else if (PlayerNo % 4 == 2) {
            player1.setImageResource(R.drawable.circle);
            player2.setImageResource(R.drawable.circle2);
            player3.setImageResource(R.drawable.circle);
            player4.setImageResource(R.drawable.circle);
        } else if (PlayerNo % 4 == 3) {
            player1.setImageResource(R.drawable.circle);
            player2.setImageResource(R.drawable.circle);
            player3.setImageResource(R.drawable.circle3);
            player4.setImageResource(R.drawable.circle);
        } else if (PlayerNo % 4 == 0) {
            player1.setImageResource(R.drawable.circle);
            player2.setImageResource(R.drawable.circle);
            player3.setImageResource(R.drawable.circle);
            player4.setImageResource(R.drawable.circle4);
        }
    }

    public void SetClickableFalse() {
        iv1.setClickable(false);
        iv2.setClickable(false);
        iv3.setClickable(false);
        iv4.setClickable(false);
        iv5.setClickable(false);
        iv6.setClickable(false);
        iv7.setClickable(false);
        iv8.setClickable(false);
        iv9.setClickable(false);
        iv10.setClickable(false);
        iv11.setClickable(false);
        iv12.setClickable(false);
        iv13.setClickable(false);
        iv14.setClickable(false);
        iv15.setClickable(false);
        iv16.setClickable(false);
    }

    public void RollDice(int x) {
        if (x == 1) {
            iv.setImageResource(R.drawable.one);
        }
        if (x == 2) {
            iv.setImageResource(R.drawable.two);
        }
        if (x == 3) {
            iv.setImageResource(R.drawable.three);
        }
        if (x == 4) {
            iv.setImageResource(R.drawable.four);
        }
        if (x == 5) {
            iv.setImageResource(R.drawable.five);
        }
        if (x == 6) {
            iv.setImageResource(R.drawable.six);
        }
    }

    public void StartGame() {
        PlayerNo = 1;
        SetDiceClickable();
    }

    public int GenerateRandom() {
        Random randy = new Random();
        n = randy.nextInt(6) + 1;
        return n;
    }

    private int PositionOf(int x, ImageView ivx) {

        SetDiceClickable();

        RelativeLayout.LayoutParams mParams = (RelativeLayout.LayoutParams) ivx.getLayoutParams();
        x = x + n;
        int a = ivx.getId();
        if (a == R.id.green1 || a == R.id.green2 || a == R.id.green3 || a == R.id.green4
                || a == R.id.blue1 || a == R.id.blue2 || a == R.id.blue3 || a == R.id.blue4
                || a == R.id.yellow1 || a == R.id.yellow2 || a == R.id.yellow3 || a == R.id.yellow4) {
            if (x > 52)
                x = x - 52;
        }
        if (a == R.id.red1 || a == R.id.red2 || a == R.id.red3 || a == R.id.red4) {
            extraN = x - 51;
            if (x > 51) {
                switch (extraN) {
                    case 1:
                        mParams.leftMargin = d;
                        mParams.topMargin = top + 7 * d;
                        break;
                    case 2:
                        mParams.leftMargin = 2 * d;
                        mParams.topMargin = top + 7 * d;
                        break;
                    case 3:
                        mParams.leftMargin = 3 * d;
                        mParams.topMargin = top + 7 * d;
                        break;
                    case 4:
                        mParams.leftMargin = 4 * d;
                        mParams.topMargin = top + 7 * d;
                        break;
                    case 5:
                        mParams.leftMargin = 5 * d;
                        mParams.topMargin = top + 7 * d;
                        break;
                }
                ivx.setLayoutParams(mParams);
            } else
                TheCase(x, ivx);

        } else if (a == R.id.green1 || a == R.id.green2 || a == R.id.green3 || a == R.id.green4) {
            if (mParams.leftMargin <= 7 * d && x > 12 && x < 24) {
                extraN = x - 12;
                switch (extraN) {
                    case 1:
                        mParams.leftMargin = 7 * d;
                        mParams.topMargin = top + d;
                        break;
                    case 2:
                        mParams.leftMargin = 7 * d;
                        mParams.topMargin = top + 2 * d;
                        break;
                    case 3:
                        mParams.leftMargin = 7 * d;
                        mParams.topMargin = top + 3 * d;
                        break;
                    case 4:
                        mParams.leftMargin = 7 * d;
                        mParams.topMargin = top + 4 * d;
                        break;
                    case 5:
                        mParams.leftMargin = 7 * d;
                        mParams.topMargin = top + 5 * d;
                        break;
                }
                ivx.setLayoutParams(mParams);
            } else
                TheCase(x, ivx);

        } else if (a == R.id.blue1 || a == R.id.blue2 || a == R.id.blue3 || a == R.id.blue4) {
            extraN = x - 25;
            if (mParams.topMargin <= top + 7 * d && x > 25) {
                switch (extraN) {
                    case 1:
                        mParams.leftMargin = 13 * d;
                        mParams.topMargin = top + 7 * d;
                        break;
                    case 2:
                        mParams.leftMargin = 12 * d;
                        mParams.topMargin = top + 7 * d;
                        break;
                    case 3:
                        mParams.leftMargin = 11 * d;
                        mParams.topMargin = top + 7 * d;
                        break;
                    case 4:
                        mParams.leftMargin = 10 * d;
                        mParams.topMargin = top + 7 * d;
                        break;
                    case 5:
                        mParams.leftMargin = 9 * d;
                        mParams.topMargin = top + 7 * d;
                        break;
                }
                ivx.setLayoutParams(mParams);
            } else
                TheCase(x, ivx);
        } else if (a == R.id.yellow1 || a == R.id.yellow2 || a == R.id.yellow3 || a == R.id.yellow4) {
            extraN = x - 38;
            if (mParams.leftMargin >= 7 * d && x > 38) {
                switch (extraN) {
                    case 1:
                        mParams.leftMargin = 7 * d;
                        mParams.topMargin = top + 13 * d;
                        break;
                    case 2:
                        mParams.leftMargin = 7 * d;
                        mParams.topMargin = top + 12 * d;
                        break;
                    case 3:
                        mParams.leftMargin = 7 * d;
                        mParams.topMargin = top + 11 * d;
                        break;
                    case 4:
                        mParams.leftMargin = 7 * d;
                        mParams.topMargin = top + 10 * d;
                        break;
                    case 5:
                        mParams.leftMargin = 7 * d;
                        mParams.topMargin = top + 9 * d;
                        break;
                }
                ivx.setLayoutParams(mParams);
            } else
                TheCase(x, ivx);
        }
        return x;

    }

    public void TheCase(int p, ImageView pp) {
        RelativeLayout.LayoutParams mParams = (RelativeLayout.LayoutParams) pp.getLayoutParams();
        switch (p) {
            case 1:
                mParams.leftMargin = d;
                mParams.topMargin = top + 6 * d;
                break;
            case 2:
                mParams.leftMargin = 2 * d;
                mParams.topMargin = top + 6 * d;
                break;
            case 3:
                mParams.leftMargin = 3 * d;
                mParams.topMargin = top + 6 * d;
                break;
            case 4:
                mParams.leftMargin = 4 * d;
                mParams.topMargin = top + 6 * d;
                break;
            case 5:
                mParams.leftMargin = 5 * d;
                mParams.topMargin = top + 6 * d;
                break;
            case 6:
                mParams.leftMargin = 6 * d;
                mParams.topMargin = top + 5 * d;
                break;
            case 7:
                mParams.leftMargin = 6 * d;
                mParams.topMargin = top + 4 * d;
                break;
            case 8:
                mParams.leftMargin = 6 * d;
                mParams.topMargin = top + 3 * d;
                break;
            case 9:
                mParams.leftMargin = 6 * d;
                mParams.topMargin = top + 2 * d;
                break;
            case 10:
                mParams.leftMargin = 6 * d;
                mParams.topMargin = top + d;
                break;
            case 11:
                mParams.leftMargin = 6 * d;
                mParams.topMargin = top;
                break;
            case 12:
                mParams.leftMargin = 7 * d;
                mParams.topMargin = top;
                break;
            case 13:
                mParams.leftMargin = 8 * d;
                mParams.topMargin = top;
                break;
            case 14:
                mParams.leftMargin = 8 * d;
                mParams.topMargin = top + d;
                break;
            case 15:
                mParams.leftMargin = 8 * d;
                mParams.topMargin = top + 2 * d;
                break;
            case 16:
                mParams.leftMargin = 8 * d;
                mParams.topMargin = top + 3 * d;
                break;
            case 17:
                mParams.leftMargin = 8 * d;
                mParams.topMargin = top + 4 * d;
                break;
            case 18:
                mParams.leftMargin = 8 * d;
                mParams.topMargin = top + 5 * d;
                break;
            case 19:
                mParams.leftMargin = 9 * d;
                mParams.topMargin = top + 6 * d;
                break;
            case 20:
                mParams.leftMargin = 10 * d;
                mParams.topMargin = top + 6 * d;
                break;
            case 21:
                mParams.leftMargin = 11 * d;
                mParams.topMargin = top + 6 * d;
                break;
            case 22:
                mParams.leftMargin = 12 * d;
                mParams.topMargin = top + 6 * d;
                break;
            case 23:
                mParams.leftMargin = 13 * d;
                mParams.topMargin = top + 6 * d;
                break;
            case 24:
                mParams.leftMargin = 14 * d;
                mParams.topMargin = top + 6 * d;
                break;
            case 25:
                mParams.leftMargin = 14 * d;
                mParams.topMargin = top + 7 * d;
                break;
            case 26:
                mParams.leftMargin = 14 * d;
                mParams.topMargin = top + 8 * d;
                break;
            case 27:
                mParams.leftMargin = 13 * d;
                mParams.topMargin = top + 8 * d;
                break;
            case 28:
                mParams.leftMargin = 12 * d;
                mParams.topMargin = top + 8 * d;
                break;
            case 29:
                mParams.leftMargin = 11 * d;
                mParams.topMargin = top + 8 * d;
                break;
            case 30:
                mParams.leftMargin = 10 * d;
                mParams.topMargin = top + 8 * d;
                break;
            case 31:
                mParams.leftMargin = 9 * d;
                mParams.topMargin = top + 8 * d;
                break;
            case 32:
                mParams.leftMargin = 8 * d;
                mParams.topMargin = top + 9 * d;
                break;
            case 33:
                mParams.leftMargin = 8 * d;
                mParams.topMargin = top + 10 * d;
                break;
            case 34:
                mParams.leftMargin = 8 * d;
                mParams.topMargin = top + 11 * d;
                break;
            case 35:
                mParams.leftMargin = 8 * d;
                mParams.topMargin = top + 12 * d;
                break;
            case 36:
                mParams.leftMargin = 8 * d;
                mParams.topMargin = top + 13 * d;
                break;
            case 37:
                mParams.leftMargin = 8 * d;
                mParams.topMargin = top + 14 * d;
                break;
            case 38:
                mParams.leftMargin = 7 * d;
                mParams.topMargin = top + 14 * d;
                break;
            case 39:
                mParams.leftMargin = 6 * d;
                mParams.topMargin = top + 14 * d;
                break;
            case 40:
                mParams.leftMargin = 6 * d;
                mParams.topMargin = top + 13 * d;
                break;
            case 41:
                mParams.leftMargin = 6 * d;
                mParams.topMargin = top + 12 * d;
                break;
            case 42:
                mParams.leftMargin = 6 * d;
                mParams.topMargin = top + 11 * d;
                break;
            case 43:
                mParams.leftMargin = 6 * d;
                mParams.topMargin = top + 10 * d;
                break;
            case 44:
                mParams.leftMargin = 6 * d;
                mParams.topMargin = top + 9 * d;
                break;
            case 45:
                mParams.leftMargin = 5 * d;
                mParams.topMargin = top + 8 * d;
                break;
            case 46:
                mParams.leftMargin = 4 * d;
                mParams.topMargin = top + 8 * d;
                break;
            case 47:
                mParams.leftMargin = 3 * d;
                mParams.topMargin = top + 8 * d;
                break;
            case 48:
                mParams.leftMargin = 2 * d;
                mParams.topMargin = top + 8 * d;
                break;
            case 49:
                mParams.leftMargin = d;
                mParams.topMargin = top + 8 * d;
                break;
            case 50:
                mParams.leftMargin = 0;
                mParams.topMargin = top + 8 * d;
                break;
            case 51:
                mParams.leftMargin = 0;
                mParams.topMargin = top + 7 * d;
                break;
            case 52:
                mParams.leftMargin = 0;
                mParams.topMargin = top + 6 * d;
                break;
        }
        pp.setLayoutParams(mParams);
    }

    public void checkPosition(ImageView v) {
        RelativeLayout.LayoutParams mP = (RelativeLayout.LayoutParams) v.getLayoutParams();
        int b = v.getId();

        if (b == R.id.red1 || b == R.id.red2 || b == R.id.red3 || b == R.id.red4) {
            int lm1 = iv5.getLeft();
            int tm1 = iv5.getTop();
            if (lm1 == mP.leftMargin && tm1 == mP.topMargin) {
                Fill2(iv5);
            }

            int lm2 = iv6.getLeft();
            int tm2 = iv6.getTop();
            if (lm2 == mP.leftMargin && tm2 == mP.topMargin) {
                Fill2(iv6);
            }

            int lm3 = iv7.getLeft();
            int tm3 = iv7.getTop();
            if (lm3 == mP.leftMargin && tm3 == mP.topMargin) {
                Fill2(iv7);
            }

            int lm4 = iv8.getLeft();
            int tm4 = iv8.getTop();
            if (lm4 == mP.leftMargin && tm4 == mP.topMargin) {
                Fill2(iv8);
            }

            int lm5 = iv9.getLeft();
            int tm5 = iv9.getTop();
            if (lm5 == mP.leftMargin && tm5 == mP.topMargin) {
                Fill3(iv9);
            }

            int lm6 = iv10.getLeft();
            int tm6 = iv10.getTop();
            if (lm6 == mP.leftMargin && tm6 == mP.topMargin) {
                Fill3(iv10);
            }

            int lm7 = iv11.getLeft();
            int tm7 = iv11.getTop();
            if (lm7 == mP.leftMargin && tm7 == mP.topMargin) {
                Fill3(iv11);
            }

            int lm8 = iv12.getLeft();
            int tm8 = iv12.getTop();
            if (lm8 == mP.leftMargin && tm8 == mP.topMargin) {
                Fill3(iv12);
            }

            int lm9 = iv13.getLeft();
            int tm9 = iv13.getTop();
            if (lm9 == mP.leftMargin && tm9 == mP.topMargin) {
                Fill4(iv13);
            }

            int lm10 = iv14.getLeft();
            int tm10 = iv14.getTop();
            if (lm10 == mP.leftMargin && tm10 == mP.topMargin) {
                Fill4(iv14);
            }

            int lm11 = iv15.getLeft();
            int tm11 = iv15.getTop();
            if (lm11 == mP.leftMargin && tm11 == mP.topMargin) {
                Fill4(iv15);
            }

            int lm12 = iv16.getLeft();
            int tm12 = iv16.getTop();
            if (lm12 == mP.leftMargin && tm12 == mP.topMargin) {
                Fill4(iv16);
            }

        }
        if (b == R.id.green1 || b == R.id.green2 || b == R.id.green3 || b == R.id.green4) {
            int lm1 = iv1.getLeft();
            int tm1 = iv1.getTop();
            if (lm1 == mP.leftMargin && tm1 == mP.topMargin) {
                Fill1(iv1);
            }

            int lm2 = iv2.getLeft();
            int tm2 = iv2.getTop();
            if (lm2 == mP.leftMargin && tm2 == mP.topMargin) {
                Fill1(iv2);
            }

            int lm3 = iv3.getLeft();
            int tm3 = iv3.getTop();
            if (lm3 == mP.leftMargin && tm3 == mP.topMargin) {
                Fill1(iv3);
            }

            int lm4 = iv4.getLeft();
            int tm4 = iv4.getTop();
            if (lm4 == mP.leftMargin && tm4 == mP.topMargin) {
                Fill1(iv4);
            }

            int lm5 = iv9.getLeft();
            int tm5 = iv9.getTop();
            if (lm5 == mP.leftMargin && tm5 == mP.topMargin) {
                Fill3(iv9);
            }

            int lm6 = iv10.getLeft();
            int tm6 = iv10.getTop();
            if (lm6 == mP.leftMargin && tm6 == mP.topMargin) {
                Fill3(iv10);
            }

            int lm7 = iv11.getLeft();
            int tm7 = iv11.getTop();
            if (lm7 == mP.leftMargin && tm7 == mP.topMargin) {
                Fill3(iv11);
            }

            int lm8 = iv12.getLeft();
            int tm8 = iv12.getTop();
            if (lm8 == mP.leftMargin && tm8 == mP.topMargin) {
                Fill3(iv12);
            }

            int lm9 = iv13.getLeft();
            int tm9 = iv13.getTop();
            if (lm9 == mP.leftMargin && tm9 == mP.topMargin) {
                Fill4(iv13);
            }

            int lm10 = iv14.getLeft();
            int tm10 = iv14.getTop();
            if (lm10 == mP.leftMargin && tm10 == mP.topMargin) {
                Fill4(iv14);
            }

            int lm11 = iv15.getLeft();
            int tm11 = iv15.getTop();
            if (lm11 == mP.leftMargin && tm11 == mP.topMargin) {
                Fill4(iv15);
            }

            int lm12 = iv16.getLeft();
            int tm12 = iv16.getTop();
            if (lm12 == mP.leftMargin && tm12 == mP.topMargin) {
                Fill4(iv16);
            }

        }
        if (b == R.id.blue1 || b == R.id.blue2 || b == R.id.blue3 || b == R.id.blue4) {
            int lm1 = iv1.getLeft();
            int tm1 = iv1.getTop();
            if (lm1 == mP.leftMargin && tm1 == mP.topMargin) {
                Fill1(iv1);
            }

            int lm2 = iv2.getLeft();
            int tm2 = iv2.getTop();
            if (lm2 == mP.leftMargin && tm2 == mP.topMargin) {
                Fill1(iv2);
            }

            int lm3 = iv3.getLeft();
            int tm3 = iv3.getTop();
            if (lm3 == mP.leftMargin && tm3 == mP.topMargin) {
                Fill1(iv3);
            }

            int lm4 = iv4.getLeft();
            int tm4 = iv4.getTop();
            if (lm4 == mP.leftMargin && tm4 == mP.topMargin) {
                Fill1(iv4);
            }

            int lm5 = iv5.getLeft();
            int tm5 = iv5.getTop();
            if (lm5 == mP.leftMargin && tm5 == mP.topMargin) {
                Fill2(iv5);
            }

            int lm6 = iv6.getLeft();
            int tm6 = iv6.getTop();
            if (lm6 == mP.leftMargin && tm6 == mP.topMargin) {
                Fill2(iv6);
            }

            int lm7 = iv7.getLeft();
            int tm7 = iv7.getTop();
            if (lm7 == mP.leftMargin && tm7 == mP.topMargin) {
                Fill2(iv7);
            }

            int lm8 = iv8.getLeft();
            int tm8 = iv8.getTop();
            if (lm8 == mP.leftMargin && tm8 == mP.topMargin) {
                Fill2(iv8);
            }

            int lm9 = iv13.getLeft();
            int tm9 = iv13.getTop();
            if (lm9 == mP.leftMargin && tm9 == mP.topMargin) {
                Fill4(iv13);
            }

            int lm10 = iv14.getLeft();
            int tm10 = iv14.getTop();
            if (lm10 == mP.leftMargin && tm10 == mP.topMargin) {
                Fill4(iv14);
            }

            int lm11 = iv15.getLeft();
            int tm11 = iv15.getTop();
            if (lm11 == mP.leftMargin && tm11 == mP.topMargin) {
                Fill4(iv15);
            }

            int lm12 = iv16.getLeft();
            int tm12 = iv16.getTop();
            if (lm12 == mP.leftMargin && tm12 == mP.topMargin) {
                Fill4(iv16);
            }


        }
        if (b == R.id.yellow1 || b == R.id.yellow2 || b == R.id.yellow3 || b == R.id.yellow4) {
            int lm1 = iv1.getLeft();
            int tm1 = iv1.getTop();
            if (lm1 == mP.leftMargin && tm1 == mP.topMargin) {
                Fill1(iv1);
            }

            int lm2 = iv2.getLeft();
            int tm2 = iv2.getTop();
            if (lm2 == mP.leftMargin && tm2 == mP.topMargin) {
                Fill1(iv2);
            }

            int lm3 = iv3.getLeft();
            int tm3 = iv3.getTop();
            if (lm3 == mP.leftMargin && tm3 == mP.topMargin) {
                Fill1(iv3);
            }

            int lm4 = iv4.getLeft();
            int tm4 = iv4.getTop();
            if (lm4 == mP.leftMargin && tm4 == mP.topMargin) {
                Fill1(iv4);
            }

            int lm5 = iv5.getLeft();
            int tm5 = iv5.getTop();
            if (lm5 == mP.leftMargin && tm5 == mP.topMargin) {
                Fill2(iv5);
            }

            int lm6 = iv6.getLeft();
            int tm6 = iv6.getTop();
            if (lm6 == mP.leftMargin && tm6 == mP.topMargin) {
                Fill2(iv6);
            }

            int lm7 = iv7.getLeft();
            int tm7 = iv7.getTop();
            if (lm7 == mP.leftMargin && tm7 == mP.topMargin) {
                Fill2(iv7);
            }

            int lm8 = iv8.getLeft();
            int tm8 = iv8.getTop();
            if (lm8 == mP.leftMargin && tm8 == mP.topMargin) {
                Fill2(iv8);
            }
            int lm9 = iv9.getLeft();
            int tm9 = iv9.getTop();
            if (lm9 == mP.leftMargin && tm9 == mP.topMargin) {
                Fill3(iv9);
            }

            int lm10 = iv10.getLeft();
            int tm10 = iv10.getTop();
            if (lm10 == mP.leftMargin && tm10 == mP.topMargin) {
                Fill3(iv10);
            }

            int lm11 = iv11.getLeft();
            int tm11 = iv11.getTop();
            if (lm11 == mP.leftMargin && tm11 == mP.topMargin) {
                Fill3(iv11);
            }

            int lm12 = iv12.getLeft();
            int tm12 = iv12.getTop();
            if (lm12 == mP.leftMargin && tm12 == mP.topMargin) {
                Fill3(iv12);
            }
        }
    }

}
