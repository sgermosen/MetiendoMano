package com.vacuum.app.metquiz.Model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

public class QuestionModel {

    @SerializedName("questions_id")
    @Expose
    private String questionsId;
    @SerializedName("question")
    @Expose
    private String question;

    @SerializedName("ans1")
    @Expose
    private String ans1;
    @SerializedName("ans2")
    @Expose
    private String ans2;
    @SerializedName("ans3")
    @Expose
    private String ans3;
    @SerializedName("ans4")
    @Expose
    private String ans4;

    @SerializedName("question_points")
    @Expose
    private String question_points;

    @SerializedName("correct_ans")
    @Expose
    private String correctAns;

    public String getQuestionsId() {
        return questionsId;
    }

    public void setQuestionsId(String questionsId) {
        this.questionsId = questionsId;
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

    public String getCorrectAns() {
        return correctAns;
    }

    public void setCorrectAns(String correctAns) {
        this.correctAns = correctAns;
    }


    public String getQuestion_points() {
        return question_points;
    }

    public void setQuestion_points(String question_points) {
        this.question_points = question_points;
    }
}