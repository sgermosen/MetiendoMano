package com.vacuum.app.metquiz.Model;

import com.google.gson.annotations.Expose;
import com.google.gson.annotations.SerializedName;

import java.util.List;

public class Example {
    @SerializedName("exam_id")
    @Expose
    private String exam_id;

    @SerializedName("exam_name")
    @Expose
    private String examName;
    @SerializedName("start_date")
    @Expose
    private String startDate;
    @SerializedName("exam_duration")
    @Expose
    private String examDuration;
    @SerializedName("points")
    @Expose
    private Integer points;
    @SerializedName("QuestionModel")
    @Expose
    private List<QuestionModel> questionModel = null;

    public String getExamName() {
        return examName;
    }

    public void setExamName(String examName) {
        this.examName = examName;
    }

    public String getStartDate() {
        return startDate;
    }

    public void setStartDate(String startDate) {
        this.startDate = startDate;
    }

    public String getExamDuration() {
        return examDuration;
    }

    public void setExamDuration(String examDuration) {
        this.examDuration = examDuration;
    }

    public Integer getPoints() {
        return points;
    }

    public void setPoints(Integer points) {
        this.points = points;
    }

    public List<QuestionModel> getQuestionModel() {
        return questionModel;
    }

    public void setQuestionModel(List<QuestionModel> questionModel) {
        this.questionModel = questionModel;
    }

    public String getExam_id() {
        return exam_id;
    }

    public void setExam_id(String exam_id) {
        this.exam_id = exam_id;
    }
}