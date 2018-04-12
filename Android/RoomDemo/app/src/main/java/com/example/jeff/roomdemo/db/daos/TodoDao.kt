package com.example.jeff.roomdemo.db.daos

import android.arch.persistence.room.*
import com.example.jeff.roomdemo.db.entities.Todo

@Dao
interface TodoDao {

    @Query("select * from Todo")
    fun getAllTasks(): List<Todo>

    @Query("select * from Todo where id = :id")
    fun findTaskById(id: Long): Todo

    @Insert
    fun insertTask(task: Todo): Long

    @Update
    fun updateTask(task: Todo)

    @Delete
    fun deleteTask(task: Todo)
}
