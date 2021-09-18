package com.vacuum.app.metquiz.Adapters;

import android.support.v7.widget.RecyclerView;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.ImageView;
import android.widget.TextView;

import com.bumptech.glide.Glide;
import com.squareup.picasso.Picasso;
import com.vacuum.app.metquiz.Model.Product;
import com.vacuum.app.metquiz.R;

import java.util.List;

public class ProductAdapter extends RecyclerView.Adapter<ProductAdapter.ProductViewHolder> {

    private final List<Product> list;

    public ProductAdapter(List<Product> list) {
        this.list = list;
    }

    @Override
    public ProductViewHolder onCreateViewHolder(ViewGroup parent, int viewType) {
        View view = LayoutInflater.from(parent.getContext()).inflate(R.layout.recyclerview_item, parent, false);
        return new ProductViewHolder(view);
    }

    @Override
    public void onBindViewHolder(ProductViewHolder holder, int position) {
        holder.bind(list.get(position));
    }

    @Override
    public int getItemCount() {
        return list.size();
    }

    static class ProductViewHolder extends RecyclerView.ViewHolder {

        private TextView question,total_correct_answers2,exam_start_date,degree,exam_name,correct_ans,ans1,ans3,ans2,ans4;

        public ProductViewHolder(View itemView) {
            super(itemView);
            question = itemView.findViewById(R.id.question);
            ans1 = itemView.findViewById(R.id.ans1);
            ans2 = itemView.findViewById(R.id.ans2);
            ans3 = itemView.findViewById(R.id.ans3);
            ans4 = itemView.findViewById(R.id.ans4);
            correct_ans = itemView.findViewById(R.id.correct_ans);
            exam_name = itemView.findViewById(R.id.exam_name);
            degree = itemView.findViewById(R.id.degree);
            exam_start_date = itemView.findViewById(R.id.exam_start_date);
            total_correct_answers2 = itemView.findViewById(R.id.total_correct_answers2);

        }

        public void bind(Product product) {
            question.setText("Question: "+product.getQuestion());
            ans1.setText("Answer1: "+product.getAns1());
            ans2.setText("Answer2: "+product.getAns2());
            ans3.setText("Answer3: "+product.getAns3());
            ans4.setText("Answer4: "+product.getAns4());
            correct_ans.setText("Correct: "+product.getCorrect_ans());
            exam_name.setText(product.getExam_name());
            degree.setText("Degree: "+product.getDegree());
            exam_start_date.setText("Date: "+product.getExam_start_date());
            total_correct_answers2.setText("Correct answers: "+String.valueOf(product.getTotal_correct_answers()));

        }
    }

}
