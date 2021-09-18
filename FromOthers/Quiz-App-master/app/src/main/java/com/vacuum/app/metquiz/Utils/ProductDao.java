package com.vacuum.app.metquiz.Utils;

import android.arch.persistence.room.Dao;
import android.arch.persistence.room.Delete;
import android.arch.persistence.room.Insert;
import android.arch.persistence.room.Query;
import android.arch.persistence.room.Update;

import com.vacuum.app.metquiz.Model.Product;

import java.util.List;

/**
 * Created by gonzalo on 7/14/17
 */

@Dao
public interface ProductDao {

    @Query("SELECT * FROM product")
    List<com.vacuum.app.metquiz.Model.Product> getAll();

    @Query("SELECT * FROM product WHERE question LIKE :question LIMIT 1")
    com.vacuum.app.metquiz.Model.Product findByName(String question);

    @Insert
    void insertAll(List<com.vacuum.app.metquiz.Model.Product> products);

    @Update
    void update(com.vacuum.app.metquiz.Model.Product product);

    @Delete
    void delete(Product product);


}
