using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using Int32 = RosMessageTypes.Std.Int32Msg;
public class markerid_pub : MonoBehaviour
{

    ROSConnection ros;
    public string topicName;
    Int32 marker_id;
    bool markerid_received;


    // Publish the cube's position and rotation every N seconds
    public float publishMessageFrequency;

    // Used to determine how much time has elapsed since the last message was published
    private float timeElapsed;


    // Start is called before the first frame update
    void Start()
    {
        topicName = "marker_id";
        publishMessageFrequency = 0.5f;
        ros = ROSConnection.GetOrCreateInstance();
        ros.RegisterPublisher<Int32>(topicName);
        marker_id = new Int32();



    }

    // Update is called once per frame
    void Update()
    {
        markerid_received = FindObjectOfType<marker_detect>().markerid_sent;

       
        if (markerid_received)
        {
            marker_id.data = FindObjectOfType<marker_detect>().id_msg;
            


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