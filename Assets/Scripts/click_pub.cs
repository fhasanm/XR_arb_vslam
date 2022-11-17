using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using Int32 = RosMessageTypes.Std.Int32Msg;
public class click_pub : MonoBehaviour
{
   
    ROSConnection ros;
    public string topicName;
    Int32 marker_id;
    bool markerid_received;

  
    


    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 1f;
    
    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        topicName = "marker_id";
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<Int32>(topicName);
        marker_id = new Int32();

     

    }

    // Update is called once per frame
    void Update()
    {
       //markerid_received = FindObjectOfType<point_sub>().points_sent;


        if (markerid_received)
        {
            marker_id.data = 0;//FindObjectOfType<point_sub>().marker_id;



            timeElapsed += Time.deltaTime;

            if (timeElapsed > publishMessageFrequency)
            {
              
                   
                
                ros.Publish(topicName, marker_id);

                //Debug.Log("Pose sent");

                timeElapsed = 0;
            }
        }
    }

  
}



/*
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using Polygon = RosMessageTypes.Geometry.PolygonMsg;
using Point = RosMessageTypes.Geometry.Point32Msg;
public class click_pub : MonoBehaviour
{
    Vector3[] click_sample;
    ROSConnection ros;
    public string topicName = "clicks";
    Polygon polygon;
    Point[] points;
    
    bool clicks_received;
    Vector3[] clicks;

    

    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency = 1f;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;
    
    
    // Start is called before the first frame update
    void Start()
    {
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<Polygon>(topicName);
        points = new Point[5];
        clicks = new Vector3[5];
        click_sample = new Vector3[5];
        click_sample[0] = new Vector3(0.42484f, 0.56301f, 1.18667f);
        click_sample[1] = new Vector3(6.84631f, 0.52358f, -0.27154f);
        click_sample[2] = new Vector3(4.71131f, 0.61129f, -9.61000f);
        click_sample[3] = new Vector3(-1.22434f, 0.73679f, -8.76754f);
        click_sample[4] = new Vector3(-0.58210f, 1.04284f, 0.04372f);
        




    }

    // Update is called once per frame
    void Update()
    {
        clicks_received = (bool)FindObjectOfType<marker_detect>().clickedmarkers_sent;
        if (clicks_received)
        {
            //Debug.Log("Clicked Markers:");

            for (var i = 0; i < 5; i++)
            {



                clicks[i] = (Vector3)FindObjectOfType<marker_detect>().MarkerPose[i];

                    //Debug.Log(clicks[i]);
                

            }

            timeElapsed += Time.deltaTime;

            if (timeElapsed > publishMessageFrequency)
            {

                for (int i = 0; i < 5; i++)
                {


                    var unity_coordinates = clicks[i];
                    var ros_coordinates = Unity2Ros(unity_coordinates);
                    points[i] = ros_coordinates;


                    //polygon.points[i].y = ros_coordinates.y;
                    //polygon.points[i].z = ros_coordinates.z;

                }
                polygon = new Polygon(points);
                ros.Publish(topicName, polygon);



                timeElapsed = 0;
            }
        }
    }

    RosMessageTypes.Geometry.Point32Msg Unity2Ros(Vector3 vector3)
    {
        return new RosMessageTypes.Geometry.Point32Msg((float)vector3.z, -(float)vector3.x, (float)vector3.y);
    }
}


*/