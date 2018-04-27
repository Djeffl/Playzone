package com.example.jeff.autocompletedemo

import android.content.Context
import android.graphics.Typeface
import android.view.View
import android.view.ViewGroup
import android.widget.ArrayAdapter
import android.widget.TextView
import android.view.LayoutInflater
import android.support.annotation.NonNull
import android.text.style.StyleSpan
import android.widget.Filter
import android.widget.Toast
import com.google.android.gms.common.data.DataBufferUtils
import com.google.android.gms.location.places.AutocompletePrediction
import com.google.android.gms.location.places.AutocompletePredictionBufferResponse
import com.google.android.gms.location.places.GeoDataClient
import com.google.android.gms.tasks.RuntimeExecutionException
import com.google.android.gms.tasks.Tasks
import java.util.concurrent.ExecutionException
import java.util.concurrent.TimeUnit
import java.util.concurrent.TimeoutException


class CustomAdapterLevel2(private val mContext: Context, private val mGeoDataClient: GeoDataClient) : ArrayAdapter<AutocompletePrediction>(mContext, android.R.layout.simple_expandable_list_item_2, android.R.id.text1) {
    private var mItems: List<AutocompletePrediction>? = null

    override fun getCount(): Int = mItems?.size ?: 0

    override fun getItem(position: Int): AutocompletePrediction = mItems!![position]

    override fun getView(position: Int, convertView: View?, parent: ViewGroup): View {
        val row = super.getView(position, convertView, parent)

        // Sets the primary and secondary text for a row.
        // Note that getPrimaryText() and getSecondaryText() return a CharSequence that may contain
        // styling based on the given CharacterStyle.

        val item: AutocompletePrediction = getItem(position)

        val textView1 = row.findViewById<View>(android.R.id.text1) as TextView
        val textView2 = row.findViewById<View>(android.R.id.text2) as TextView
        textView1.text = item.getPrimaryText(StyleSpan(Typeface.BOLD))
        textView2.text = item.getSecondaryText(StyleSpan(Typeface.BOLD))

        return row
    }

    override fun getFilter(): Filter {
        return mFilter
    }

    private val mFilter =  object : Filter() {
        override fun performFiltering(constraint: CharSequence?): Filter.FilterResults {
            val results = Filter.FilterResults()

            // We need a separate list to store the results, since
            // this is run asynchronously.
            var filterData: java.util.ArrayList<AutocompletePrediction>? = java.util.ArrayList()

            // Skip the autocomplete query if no constraints are given.
            if (constraint != null) {
                // Query the autocomplete API for the (constraint) search string.
                filterData = getAutocomplete(constraint)
            }

            results.values = filterData
            if (filterData != null) {
                results.count = filterData.size
            } else {
                results.count = 0
            }

            return results
        }

        override fun publishResults(constraint: CharSequence?, results: Filter.FilterResults?) {

            if (results != null && results.count > 0) {
                // The API returned at least one result, update the data.
                mItems = results.values as java.util.ArrayList<AutocompletePrediction>
                notifyDataSetChanged()
            } else {
                // The API did not return any results, invalidate the data set.
                notifyDataSetInvalidated()
            }
        }

        override fun convertResultToString(resultValue: Any): CharSequence {
            // Override this method to display a readable result in the AutocompleteTextView
            // when clicked.
            return if (resultValue is AutocompletePrediction) {
                resultValue.getFullText(null)
            } else {
                super.convertResultToString(resultValue)
            }
        }
    }

    /**
     * Submits an autocomplete query to the Places Geo Data Autocomplete API.
     * Results are returned as frozen AutocompletePrediction objects, ready to be cached.
     * Returns an empty list if no results were found.
     * Returns null if the API client is not available or the query did not complete
     * successfully.
     * This method MUST be called off the main UI thread, as it will block until data is returned
     * from the API, which may include a network request.
     *
     * @param constraint Autocomplete query string
     * @return Results from the autocomplete API or null if the query was not successful.
     * @see GeoDataClient#getAutocompletePredictions(String, LatLngBounds, AutocompleteFilter)
     * @see AutocompletePrediction#freeze()
     */
    private fun getAutocomplete(constraint: CharSequence):  ArrayList<AutocompletePrediction>?
    {
        // Submit the query to the autocomplete API and retrieve a PendingResult that will
        // contain the results when the query completes.
        val results = mGeoDataClient.getAutocompletePredictions(constraint.toString(), null, null)

        // This method should have been called off the main UI thread. Block and wait for at most
        // 60s for a result from the API.
        try {
            Tasks.await(results, 60, TimeUnit.SECONDS)
        } catch (e: ExecutionException) {
            e.printStackTrace()
        } catch (e: InterruptedException) {
            e.printStackTrace()
        } catch (e: TimeoutException) {
            e.printStackTrace()
        }


        try {
            val autocompletePredictions = results.result


            // Freeze the results immutable representation that can be stored safely.
            return DataBufferUtils.freezeAndClose(autocompletePredictions)
        } catch (e: RuntimeExecutionException) {
            // If the query did not complete successfully return null
            Toast.makeText(mContext, "Error contacting API: " + e.toString(),
                    Toast.LENGTH_SHORT).show()
            return null
        }
    }
    }

