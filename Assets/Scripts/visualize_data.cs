using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class visualize_data : MonoBehaviour
{
    Vector3[] datapoints;
    bool data_received;
    GameObject[] data_visual;
    public GameObject visual;
    GameObject marker_frame;
    bool flag;

    
    // Start is called before the first frame update
    void Start()
    {
        datapoints = new Vector3[4];
        data_visual = new GameObject[4];
        marker_frame = GameObject.Find("marker_frame");
        
    }

    // Update is called once per frame
    void Update()
    {
        data_received = FindObjectOfType<point_sub>().data_sent;
        if (data_received)
        {
            for (int i = 0; i < 4; i++)
            {
                datapoints[i] = FindObjectOfType<point_sub>().data[i];
                Debug.Log(datapoints[i]);
            }
            
           if(!flag)
            {
                for (int i = 0; i < 4; i++)
                {

                    data_visual[i] = Instantiate(visual, marker_frame.transform, false) as GameObject;
                  

                }
                flag = true;
            }

            for (int i = 0; i < 4; i++)
            {
                data_visual[i].transform.localPosition = datapoints[i];
               
                data_visual[i].transform.localRotation = Quaternion.identity;
                data_visual[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                
            }

        }
        
        
    }
}
