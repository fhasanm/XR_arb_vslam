/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class transform : MonoBehaviour
{
    //bool transform_done;
    Vector3 test_marker;
    Quaternion test_marker_rotation;

    public GameObject test;
    GameObject Unity;
    GameObject ROS;
    bool pose_received;
    private float timeElapsed;
    Vector3[] points;
    Vector3 ros_pos;

    bool points_received;


    void Start()
    {
  

        test_marker = new Vector3(0.045f, -0.345f, 3.218f);
        test_marker_rotation = new Quaternion(0.99021002f, 0.03735733f, 0.07529494f, 0.11144154f);

        Unity = GameObject.Find("XRRig");
        ROS = GameObject.Find("ROSRF");
        points = new Vector3[5];
        
        

    }
    


    void Update()
    {

        points_received = true; // FindObjectOfType<point_sub>().points_sent;

        if (points_received)
        {
            for (var i = 0; i < 5; i++)
            {

                if (i < 4)
                {
                    points[i] = (Vector3)FindObjectOfType<point_sub>().marker_coordinates[i];
                    //Debug.Log(markercoordinates[i]);



                }
                else
                {
                    ros_pos = (Vector3)FindObjectOfType<point_sub>().marker_coordinates[i];
                    

                }

            }
        }
        
        pose_received = (bool)FindObjectOfType<marker_detect>().pose_sent;

        if (pose_received && points_received)
        {
            Vector3 nx = points[1] - points[0];
            Vector3 nz = points[0] - points[3];
            Vector3 ny = Vector3.Cross(nz, nx);
            Quaternion ros_rot = Quaternion.LookRotation(ny, Vector3.up);
            //unity_trans and unity_rot is basically rosposes of the marker
              
            Vector3 unity_pos = FindObjectOfType<marker_detect>().pose_pos;
            Quaternion unity_rot = FindObjectOfType<marker_detect>().pose_rot;
            Vector3 translation = unity_pos - ros_pos;
            Vector3 rotation = unity_rot.eulerAngles - ros_rot.eulerAngles;

            ROS.transform.position = translation;
            ROS.transform.rotation = Quaternion.FromToRotation(unity_rot.eulerAngles, ros_rot.eulerAngles);
            Debug.Log("translation: " + translation);
            Debug.Log("rotation: " + rotation);


            
            
            test.transform.SetParent(ROS.transform, false);
            //Test cube is set in ROS frame
            test.transform.localPosition = test_marker;
            test.transform.localRotation = Quaternion.identity;
            Debug.Log("Test cube in ROS frame: " + test.transform.localPosition.ToString("F9"));
            test.transform.SetParent(Unity.transform);
            Debug.Log("Test cube is set in Unity frame");
            Debug.Log("Test cube in Unity: " + test.transform.position.ToString("F9"));
            var error = test.transform.position - (Vector3)FindObjectOfType<marker_detect>().pose_pos;
            Debug.Log("Error :" + error.ToString("F9"));

        }



    }
    
}*/


/*
 
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using Transform = RosMessageTypes.Geometry.TransformMsg;

public class transform : MonoBehaviour
{
    //bool transform_done;
    Vector3 test_marker;
    
    public GameObject test;
    GameObject Unity;
    bool pose_received;
    private float timeElapsed;
     
    
    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<Transform>("transform", callback);
      
        test_marker = new Vector3(0.045f, -0.345f, 3.218f);
        
        Unity = GameObject.Find("XRRig");

    }
    

    void callback(Transform transform)
    {
        
        
            //Ros and Unity coordinates are different,use saved page from rossharp to understand 
            //the transfomration
            

            var ros_trans = transform.translation;
            var ros_rot = transform.rotation;
            Vector3 unity_trans = transRos2Unity(ros_trans);
            Quaternion unity_rot = rotRos2Unity(ros_rot);
            pose_received = (bool)FindObjectOfType<marker_detect>().pose_sent;


        if (pose_received)
        {
            //unity_trans and unity_rot is basically rosposes of the marker
            Vector3 unitypos = FindObjectOfType<marker_detect>().pose_pos;
            Quaternion unityor = FindObjectOfType<marker_detect>().pose_rot;

            Vector3 translation = unitypos - unity_trans;
            Vector3 rotation = unityor.eulerAngles - unity_rot.eulerAngles;

            this.transform.Translate(translation - this.transform.position);
            this.transform.Rotate(rotation - this.transform.rotation.eulerAngles);
            Debug.Log("translation: " + translation);
            Debug.Log("rotation: " + rotation);




            test.transform.SetParent(this.transform, false);
            //Test cube is set in ROS frame
            test.transform.localPosition = test_marker ;
            
            Debug.Log("Test cube in ROS frame: " + test.transform.localPosition.ToString("F9"));
            test.transform.SetParent(Unity.transform, false);
            Debug.Log("Test cube is set in Unity frame");
            Debug.Log("Test cube in Unity: " + test.transform.position.ToString("F9"));
            var error = test.transform.position - (Vector3)FindObjectOfType<marker_detect>().pose_pos;
            //Debug.Log("Error :" + error.ToString("F9"));

        }

        
        
        
        
        //transform_done = true;

          



            


        

    }

   
     
    Vector3 transRos2Unity(RosMessageTypes.Geometry.Vector3Msg transform)
    {
        return new Vector3(-(float)transform.y, (float)transform.z, (float)transform.x);
    }
    
    Quaternion rotRos2Unity(RosMessageTypes.Geometry.QuaternionMsg transform)
    {
        return new Quaternion((float)transform.y, -(float)transform.z, -(float)transform.x, (float)transform.w);
    }
}

*/

