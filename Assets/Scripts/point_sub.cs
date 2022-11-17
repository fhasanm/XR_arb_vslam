using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using Polygon = RosMessageTypes.Geometry.PolygonMsg;
using Int32 = RosMessageTypes.Std.Int32Msg;
using Int32MultiArray = RosMessageTypes.Std.Int32MultiArrayMsg;

public class point_sub : MonoBehaviour
{

    /* public static Vector3 Unity2Ros(this Vector3 vector3)
     {
         return new Vector3(vector3.z, -vector3.x, vector3.y);
     }

     public static Vector3 Ros2UnityScale(this Vector3 vector3)
     {
         return new Vector3(vector3.y, vector3.z, vector3.x);
     }

     public static Vector3 Unity2RosScale(this Vector3 vector3)
     {
         return new Vector3(vector3.z, vector3.x, vector3.y);
     }

     public static Quaternion Ros2Unity(this Quaternion quaternion)
     {
         return new Quaternion(quaternion.y, -quaternion.z, -quaternion.x, quaternion.w);
     }

     public static Quaternion Unity2Ros(this Quaternion quaternion)
     {
         return new Quaternion(-quaternion.z, quaternion.x, -quaternion.y, quaternion.w);
     }*/

    public Vector3[] data;
    public int[] intensity;
    public bool data_sent;
    public int data_count;
    bool executed_once;
    
    



    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<Polygon>("unity_points", point_callback);
        ROSConnection.GetOrCreateInstance().Subscribe<Int32>("unity_count", count_callback);
        ROSConnection.GetOrCreateInstance().Subscribe<Int32MultiArray>("unity_int", int_callback);

        //data = new Vector3[4];
    }

    void count_callback(Int32 msg)
    {
        data_count = msg.data;
        Debug.Log("data count: "+data_count);
        if (!executed_once)
        {
            data = new Vector3[data_count];
            intensity = new int[data_count];
            executed_once = true;
        }
        
      

    }
    
    void int_callback(Int32MultiArray msg)
    {
        if (data_count>0)
        {
            for (int i = 0; i < data_count; i++)
            {
                intensity[i] = msg.data[i];
            }
        }
    }



    void point_callback(Polygon polygon)
    {
        
        if (executed_once)
        {
            //Ros and Unity coordinates are different,use saved page from rossharp to understand
            //the transfomration
            for (int i = 0; i < data_count; i++)
            {
                //after the marker coordinates are received, convert them from ROS to UNITY coordinates
                //and save them in the array marker_coordinates



                var ros_coordinates = polygon.points[i];
                var unity_pos = Ros2Unity(ros_coordinates);
                //data[i].x = unity_pos.x;
                //data[i].y = unity_pos.y;
                //data[i].z = unity_pos.z;
                data[i] = new Vector3(unity_pos.x, unity_pos.y, unity_pos.z);
                Debug.Log(data[i]);

                /*for (int j = 0; j < data.Count; j++)
                {
                    Debug.Log(data[j]);
                }*/
                if (i == data_count - 1)
                {
                    data_sent = true;
                }
                
            }
        }

        

      /*  if (data.Count == 4)
        {
            data_sent = true;
        }*/

    }


    Vector3 Ros2Unity(RosMessageTypes.Geometry.Point32Msg vector3)
    {
        return new Vector3(vector3.x, vector3.y, vector3.z);
    }
}

/*using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.Robotics.ROSTCPConnector;
using Polygon = RosMessageTypes.Geometry.PolygonMsg;

public class point_sub : MonoBehaviour
{

    public Vector3[] data;
    public bool data_sent;


    void Start()
    {
        ROSConnection.GetOrCreateInstance().Subscribe<Polygon>("unity_data", callback);
        data = new Vector3[4];
    }


    void callback(Polygon polygon)
    {


        //Ros and Unity coordinates are different,use saved page from rossharp to understand
        //the transfomration
        for (int i = 0; i < 4; i++)
        {
            //after the marker coordinates are received, convert them from ROS to UNITY coordinates
            //and save them in the array marker_coordinates



            var ros_coordinates = polygon.points[i];
            var unity_pos = Ros2Unity(ros_coordinates);
            data[i].x = unity_pos.x;
            data[i].y = unity_pos.y;
            data[i].z = unity_pos.z;







        }

        if (data[3] != Vector3.zero)
        {
            data_sent = true;
        }

    }


    *//*    Vector3 Ros2Unity(RosMessageTypes.Geometry.Point32Msg vector3)
        {
            return new Vector3(-vector3.y, vector3.z, vector3.x);
        }*//*
    Vector3 Ros2Unity(RosMessageTypes.Geometry.Point32Msg vector3)
    {
        return new Vector3(vector3.x, vector3.y, vector3.z);
    }

    *//*
      public static Vector3 Unity2Ros(this Vector3 vector3)
        {
            return new Vector3(vector3.z, -vector3.x, vector3.y);
        }

        public static Vector3 Ros2UnityScale(this Vector3 vector3)
        {
            return new Vector3(vector3.y, vector3.z, vector3.x);
        }

        public static Vector3 Unity2RosScale(this Vector3 vector3)
        {
            return new Vector3(vector3.z, vector3.x, vector3.y);
        }

        public static Quaternion Ros2Unity(this Quaternion quaternion)
        {
            return new Quaternion(quaternion.y, -quaternion.z, -quaternion.x, quaternion.w);
        }

        public static Quaternion Unity2Ros(this Quaternion quaternion)
        {
            return new Quaternion(-quaternion.z, quaternion.x, -quaternion.y, quaternion.w);
        }
    *//*


}*/