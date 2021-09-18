package com.vacuum.app.metquiz.Utils;

import android.widget.EditText;

import com.vacuum.app.metquiz.Model.Example;
import com.vacuum.app.metquiz.Model.QuestionModel;
import com.vacuum.app.metquiz.Model.User;

import okhttp3.ResponseBody;
import retrofit2.Call;
import retrofit2.http.Field;
import retrofit2.http.FormUrlEncoded;
import retrofit2.http.GET;
import retrofit2.http.POST;

/**
 * Created by Home on 3/27/2018.
 */

public interface RegisterAPI {

    @POST("metquiz/insert.php")
    @FormUrlEncoded
    Call<User> insertUser(@Field("card_id") String card_number,
                                   @Field("email") String email,
                                   @Field("password") String password,
                                   @Field("fname") String fname,
                                  @Field("lname") String lname,
                                  @Field("grade_id") int grade_id,
                                  @Field("division_id") int division_id);

    @POST("/metquiz/login.php")
    @FormUrlEncoded
    Call<User> loging_user(@Field("card_id") String card_id,
                           @Field("password") String password);



    @GET("/metquiz/getquestions.php")
    Call<Example> getQuestions();

    @POST("metquiz/publish.php")
    @FormUrlEncoded
    Call<ResponseBody> publish(@Field("total_points") int total_points,
                               @Field("correct_ans") int correct_ans,
                               @Field("wrong_ans") int wrong_ans,
                               @Field("student_id") int student_id,
                               @Field("division_id") int division_id,
                               @Field("exam_id") int exam_id);


}
