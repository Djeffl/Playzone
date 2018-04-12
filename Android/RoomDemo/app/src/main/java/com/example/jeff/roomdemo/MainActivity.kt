package com.example.jeff.roomdemo

import android.os.Bundle
import android.support.v7.app.AppCompatActivity
import android.util.Log
import android.widget.TextView
import com.example.jeff.roomdemo.db.AppDatabase
import com.example.jeff.roomdemo.db.entities.Todo
import com.example.jeff.roomdemo.utils.DbWorkerThread

class MainActivity : AppCompatActivity() {
    private var mDb: AppDatabase? = null
    private lateinit var mDbWorkerThread: DbWorkerThread

    private lateinit var mTxt : TextView

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        mTxt = findViewById(R.id.txt)

        mDbWorkerThread = DbWorkerThread("dbWorkerThread")
        mDbWorkerThread.start()

        mDb = AppDatabase.getInstance(this)

        val task = Runnable {
            //val id = mDb?.todoDao()?.insertTask(Todo(0, "Task 1"))

            //Log.w("V1", id.toString())

            val todo = mDb?.todoDao()?.findTaskById(1)

            mTxt.text = todo?.name
        }
        mDbWorkerThread.postTask(task)
    }

}
