package com.ldt.cinematicket.ui.main.book;

import android.os.Bundle;
import android.support.annotation.NonNull;
import android.support.annotation.Nullable;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.TextView;

import com.ldt.cinematicket.R;
import com.ldt.cinematicket.model.Ticket;
import com.ldt.cinematicket.ui.widget.fragmentnavigationcontroller.SupportFragment;

import butterknife.BindView;
import butterknife.ButterKnife;
import butterknife.OnClick;

public class PurchaseScreen extends SupportFragment {
    public static PurchaseScreen newInstance(Ticket t) {
        PurchaseScreen p = new PurchaseScreen();
        p.mTicket = t;
        return p;
    }
    @OnClick(R.id.back_button)
    void back() {
        getMainActivity().dismiss();
    }
    Ticket mTicket;
    @Nullable
    @Override
    protected View onCreateView(LayoutInflater inflater, ViewGroup container) {
        return inflater.inflate(R.layout.ticket_print,container,false);
    }

    @Override
    public void onViewCreated(@NonNull View view, @Nullable Bundle savedInstanceState) {
        super.onViewCreated(view, savedInstanceState);
        ButterKnife.bind(this,view);
        setContent();
    }
    @BindView(R.id.user) TextView mUser;
    @BindView(R.id.movie) TextView mMovie;
    @BindView(R.id.cinema) TextView mCinema;
    @BindView(R.id.room) TextView mRoom;
    @BindView(R.id.seat) TextView mSeat;
    @BindView(R.id.date) TextView mDate;
    @BindView(R.id.time) TextView mTime;
    @BindView(R.id.price) TextView mPrice;
    void setContent() {
        mUser.setText(getMainActivity().user.getDisplayName());
        mMovie.setText(mTicket.getMovieName());
        mCinema.setText(mTicket.getCinemaName());
        mRoom.setText(mTicket.getRoom()+"");
        mSeat.setText(mTicket.getSeat());
        mDate.setText(mTicket.getDate());
        mTime.setText(mTicket.getTime());
        mPrice.setText(mTicket.getPrice()+"");
    }
}
