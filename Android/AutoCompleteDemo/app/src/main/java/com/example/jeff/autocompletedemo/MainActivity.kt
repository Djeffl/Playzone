package com.example.jeff.autocompletedemo

import android.support.v7.app.AppCompatActivity
import android.os.Bundle
import android.view.View
import android.widget.ArrayAdapter
import android.widget.AutoCompleteTextView
import android.widget.Toast
import com.google.android.gms.location.places.Places
import kotlinx.android.synthetic.main.activity_main.*

class MainActivity : AppCompatActivity() {

    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        var autoCompleteTextView:AutoCompleteTextView = findViewById(R.id.autoCompleteTextView)



        //Example one
        val colors = arrayOf(
                "Red","Green","Blue","Maroon","Magenta",
                "Gold","GreenYellow"
        )


        // Initialize a new array adapter object
        //aval adapter = ArrayAdapter<String>(
        //        this, // Context
        //        android.R.layout.simple_dropdown_item_1line, // Layout
        //       colors // Array
        //)

        // Set the AutoCompleteTextView adapter
        //autoCompleteTextView.setAdapter(adapter)


        // Auto complete threshold
        // The minimum number of characters to type to show the drop down
        //autoCompleteTextView.threshold = 1



        // Set a dismiss listener for auto complete text view
        //autoCompleteTextView.setOnDismissListener {
        //            Toast.makeText(applicationContext,"Suggestion closed.",Toast.LENGTH_SHORT).show()
        //        }


        // Set a click listener for root layout
        //root_layout.setOnClickListener{
        //    val text = autoCompleteTextView.text
        //    Toast.makeText(applicationContext,"Inputted : $text",Toast.LENGTH_SHORT).show()
        //}


        // Set a focus change listener for auto complete text view
        //autoCompleteTextView.onFocusChangeListener = View.OnFocusChangeListener { view, b ->
        //    if (b) {
                // Display the suggestion dropdown on focus
        //        autoCompleteTextView.showDropDown()
        //    }
        //}

        // Example 2

        //var items : ArrayList<String> = arrayListOf("Jeff", "Deff", "Kotlin", "John", "Doe")
        //autoCompleteTextView.setAdapter(CustomAdapter(context = this, textViewResourceId = android.R.layout.simple_dropdown_item_1line, items = items))

        // Example 3
        var mGeoDataClient = Places.getGeoDataClient(this)
        autoCompleteTextView.setAdapter(CustomAdapterLevel2(this, mGeoDataClient))
    }
}
