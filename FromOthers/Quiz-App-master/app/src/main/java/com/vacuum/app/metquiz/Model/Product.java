package com.vacuum.app.metquiz.Model;

import android.arch.persistence.room.ColumnInfo;
import android.arch.persistence.room.Entity;
import android.arch.persistence.room.PrimaryKey;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

/**
 * Created by gonzalo on 7/14/17
 */
@Entity
public class Product {

    @PrimaryKey(autoGenerate = true)
    private int uid;

    @ColumnInfo(name = "question")
    private String question;

    @ColumnInfo(name = "ans1")
    private String ans1;

    @ColumnInfo(name = "ans2")
    private String ans2;

    @ColumnInfo(name = "ans3")
    private String ans3;

    @ColumnInfo(name = "ans4")
    private String ans4;


    @ColumnInfo(name = "correct_ans")
    private String correct_ans;


    @ColumnInfo(name = "exam_start_date")
    private String exam_start_date;

    @SerializedName("exam_name")
    @Expose
    private String exam_name;

    @SerializedName("degree")
    @Expose
    private String degree;

    @ColumnInfo(name = "total_correct_answers")
    private int total_correct_answers;

    public int getUid() {
        return uid;
    }

    public void setUid(int uid) {
        this.uid = uid;
    }


    public String getQuestion() {
        return question;
    }

    public void setQuestion(String question) {
        this.question = question;
    }

    public String getAns1() {
        return ans1;
    }

    public void setAns1(String ans1) {
        this.ans1 = ans1;
    }

    public String getAns2() {
        return ans2;
    }

    public void setAns2(String ans2) {
        this.ans2 = ans2;
    }

    public String getAns3() {
        return ans3;
    }

    public void setAns3(String ans3) {
        this.ans3 = ans3;
    }

    public String getAns4() {
        return ans4;
    }

    public void setAns4(String ans4) {
        this.ans4 = ans4;
    }

    public String getCorrect_ans() {
        return correct_ans;
    }

    public void setCorrect_ans(String correct_ans) {
        this.correct_ans = correct_ans;
    }

    public String getExam_start_date() {
        return exam_start_date;
    }

    public void setExam_start_date(String exam_start_date) {
        this.exam_start_date = exam_start_date;
    }

    public String getExam_name() {
        return exam_name;
    }

    public void setExam_name(String exam_name) {
        this.exam_name = exam_name;
    }

    public String getDegree() {
        return degree;
    }

    public void setDegree(String degree) {
        this.degree = degree;
    }

    public int getTotal_correct_answers() {
        return total_correct_answers;
    }

    public void setTotal_correct_answers(int total_correct_answers) {
        this.total_correct_answers = total_correct_answers;
    }
}