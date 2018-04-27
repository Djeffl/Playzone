package com.example.jeff.autocompletedemo

import android.content.Context
import android.widget.ArrayAdapter
import android.view.LayoutInflater
import android.widget.Filter
import android.widget.Filterable


class CustomAdapter: ArrayAdapter<String>, Filterable {
    private lateinit var mItems: ArrayList<String>
    private var layoutInflater: LayoutInflater

    constructor(context: Context, textViewResourceId: Int, items: List<String>): super(context, textViewResourceId, items) {

        this.mItems = ArrayList(items.size)
        this.mItems.addAll(items)
        this.layoutInflater = getContext().getSystemService(Context.LAYOUT_INFLATER_SERVICE) as LayoutInflater
    }

    private val mFilter = object : Filter() {
        override fun convertResultToString(resultValue: Any): String {
            return (resultValue as String)
        }

        override fun performFiltering(constraint: CharSequence?): Filter.FilterResults {
            val results = Filter.FilterResults()

            if (constraint != null) {
                val suggestions = ArrayList<String>()
                for (item in mItems) {
                    // Note: change the "contains" to "startsWith" if you only want starting matches
                    if (item.toLowerCase().contains(constraint.toString().toLowerCase())) {
                        suggestions.add(item)
                    }
                }

                results.values = suggestions
                results.count = suggestions.size
            }

            return results
        }

        override fun publishResults(constraint: CharSequence, results: Filter.FilterResults?) {
            clear()
            if (results != null && results.count > 0) {
                // we have filtered results
                addAll(results.values as ArrayList<String>)
            } else {
                // no filter, add entire original list back in
                addAll(mItems)
            }
            notifyDataSetChanged()
        }
    }


    override fun getFilter(): Filter {
        return mFilter //super.getFilter()
    }

}