using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Varjo.XR;

public class marker_detect : MonoBehaviour
{

    
    public long[] id;
    public int id_msg;
    public GameObject test_object;
    GameObject marker_frame;
    // A list for found markers.
    private List<VarjoMarker> markers = new List<VarjoMarker>();
    
    public bool markerid_sent;
    bool executed_once;



    private void OnEnable()
    {
        id = new long[5];
        id[0] = 301;
        id[1] = 302;
        id[2] = 304;
        id[3] = 305;
        id[4] = 306;

        // Start rendering the video see-through image
        VarjoMixedReality.StartRender();
        // Enable Varjo Marker tracking.
        VarjoMarkers.EnableVarjoMarkers(true);
        //y = GameObject.Find("y");
        VarjoMixedReality.EnableDepthEstimation();

        marker_frame = GameObject.Find("marker_frame");




    }

    private void OnDisable()
    {
        // Disable Varjo Marker tracking.
        VarjoMarkers.EnableVarjoMarkers(false);
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

                markerid_sent = false;

                if (marker.id == id[0])//&& marker.confidence > 0.97f)
                {

                    if (!executed_once && marker.confidence > 0.97f)
                    {
                        test_object.transform.position = marker.pose.position;
                        test_object.transform.rotation = marker.pose.rotation;
                        executed_once = true;



                    }
                    markerid_sent = true;
                    id_msg = (int)marker.id;

                    marker_frame.transform.position = marker.pose.position;
                    marker_frame.transform.rotation = marker.pose.rotation;


                    var error = marker.pose.position - test_object.transform.position;
                    error.x = Mathf.Abs(error.x);
                    error.y = Mathf.Abs(error.y);
                    error.z = Mathf.Abs(error.z);
                    /*Debug.Log("Drift: " + error.ToString("F5"));
                    Debug.Log("Marker pos: " + marker.pose.position);*/

                }



                if (marker.id == id[1] || marker.id == id[2] || marker.id == id[3] || marker.id == id[4])
                {

                    markerid_sent = true;
                    id_msg = (int)marker.id;

                    marker_frame.transform.position = marker.pose.position;
                    marker_frame.transform.rotation = marker.pose.rotation;



                }

                




            }






        }
    }

}

/*
 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Varjo.XR;

public class marker_detect : MonoBehaviour
{
    [Serializable]
    public struct given_markers
    {
        public long id;
        public GameObject marker_obj;
        
    }
    // An public array for all the tracked objects. 
    public given_markers[] given_marker;
    public Vector3[] MarkerPose;
    // A list for found markers.
    private List<VarjoMarker> markers = new List<VarjoMarker>();
    public bool clickedmarkers_sent;

    public GameObject test;


    private void OnEnable()
    {
        
        given_marker = new given_markers[5];
        MarkerPose = new Vector3[5];
        // Start rendering the video see-through image
        VarjoMixedReality.StartRender();
        // Enable Varjo Marker tracking.
        VarjoMarkers.EnableVarjoMarkers(true);

        //Instantiate 
        for (var i = 0; i < given_marker.Length; i++)
        {
            given_marker[i].marker_obj = Instantiate(GameObject.Find("marker_obj"), Vector3.zero, Quaternion.identity);
            given_marker[i].marker_obj.transform.SetParent(GameObject.Find("XRRig").transform);

        }

        //marker ids

        given_marker[0].id = 294;
        given_marker[1].id = 295;
        given_marker[2].id = 296;
        given_marker[3].id = 297;
        given_marker[4].id = 298;  
    }

    private void OnDisable()
    {
        // Disable Varjo Marker tracking.
        VarjoMarkers.EnableVarjoMarkers(false);
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
                for (var i = 0; i < given_marker.Length; i++)
                {
                    
                    
                    if (marker.id == given_marker[i].id && marker.confidence>0.97f)
                    {
                        given_marker[i].marker_obj.transform.position = marker.pose.position;
                        
                        //Debug.Log("Marker info: " + marker.id + " " + marker.timestamp);
                        
                    }
                    
                    
                    
                }
            }
           
            if (given_marker[4].marker_obj.transform.position != Vector3.zero && clickedmarkers_sent == false && VarjoMarkers.GetVarjoMarkerCount()>5)
            {
                for (var i = 0; i < 5; i++)
                {
                    
                    MarkerPose[i] = given_marker[i].marker_obj.transform.position;
                    Debug.Log(MarkerPose[i].ToString("F5"));
                    test.transform.position = MarkerPose[4];
                    
                }
                clickedmarkers_sent = false;
                //drift is creating a big problem
               
            }
            
           
        }
    }
    
}


*/
