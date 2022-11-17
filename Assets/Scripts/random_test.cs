using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Varjo.XR;

public class random_test : MonoBehaviour
{

    public long id = 297;
    public GameObject y;
   
    
    // A list for found markers.
    private List<VarjoMarker> markers = new List<VarjoMarker>();
   


    private void OnEnable()
    {

      
        // Start rendering the video see-through image
        VarjoMixedReality.StartRender();
        // Enable Varjo Marker tracking.
        VarjoMarkers.EnableVarjoMarkers(true);
        y = GameObject.Find("y");
        


    }

    private void OnDisable()
    {
        // Disable Varjo Marker tracking.
       // VarjoMarkers.EnableVarjoMarkers(false);
    }


    void Update()
    {
        // Check if Varjo Marker tracking is enabled and functional.
        if (VarjoMarkers.IsVarjoMarkersEnabled())
        {
            // Get a list of markers with up-to-date data.
            VarjoMarkers.GetVarjoMarkers(out markers);

            // Loop through found markers and update gameobjects matching the marker ID in the array.
            foreach (var marker in markers)
            {
                


                    if (marker.id == id && marker.confidence > 0.97f)
                    {
                        Debug.Log("Position: "+ marker.pose.position);
                        Debug.Log("Orientation: " + marker.pose.rotation.eulerAngles);

                        y.transform.position = marker.pose.position;
                        y.transform.rotation = marker.pose.rotation;



                        //Debug.Log("Marker info: " + marker.id + " " + marker.timestamp);

                    }



            }

                

            


        }
    }

}

