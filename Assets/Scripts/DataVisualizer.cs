using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataVisualizer : MonoBehaviour
{
    public GameObject dataObject;
    public GameObject layeredDateObject;
    public Material[] intensityColor;
    private float scaleMin = 0.5f;
    private float scaleInterval = 0.25f;
    public int numData;
    //private List<Vector3> dataPosList = new List<Vector3>();
    //private List<int> intensityList = new List<int>();
    public Vector3[] dataPosArray;
    public int[] intensityArray;

    public bool isSpawned = false;

    Vector3[] datapoints;
    bool data_received;
    GameObject[] data_visual;
    public GameObject visual;
    GameObject marker_frame;
    bool flag;

    bool executed_once;
    bool execute_flag2;
    int dataCount;

    public GameObject[] layeredDataObjects;
    public GameObject[] dataObjects;

    // Start is called before the first frame update
    void Start()
    {
        scaleMin = 0.01f;
        scaleInterval = 0.01f;
        //dataPosList.Add(new Vector3(0, 0, 0));
        //intensityList.Add(1);
        //VisualizeDataSz(dataPosList, intensityList);








        marker_frame = GameObject.Find("marker_frame");



    }

    void Update()
    {
        data_received = FindObjectOfType<point_sub>().data_sent;
        if (data_received)
        {
            dataCount = FindObjectOfType<point_sub>().data_count;
            if (!executed_once)
            {
                dataPosArray = new Vector3[dataCount];
                intensityArray = new int[dataCount];
                layeredDataObjects = new GameObject[dataCount];
                dataObjects = new GameObject[dataCount];
                executed_once = true;
            }


            for (int i = 0; i < dataCount; i++)
            {
                dataPosArray[i] = FindObjectOfType<point_sub>().data[i];
                intensityArray[i] = FindObjectOfType<point_sub>().intensity[i];

            }

            if (!flag)
            {
                //VisualizeDataSz(dataPosArray, intensityArray, dataCount);
                VisualizeDataSzCl(dataPosArray, intensityArray, dataCount);
                flag = true;
            }

            for (int i = 0; i < dataCount; i++)
            {
                //data_visual[i].transform.localPosition = datapoints[i];
                //data_visual[i].transform.localRotation = Quaternion.identity;
                //data_visual[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

                //UpdateDataSz(i);
                UpdateDataSzCl(i);
            }

        }


    }


    Vector3[] GenerateDataPosArray()
    {
        dataPosArray = new Vector3[4];
        for (int i = 0; i < dataPosArray.Length; i++)
        {
            dataPosArray[i] = RandomPos();
        }
        return dataPosArray;

    }

    int[] GenerateIntensityArray()
    {
        intensityArray = new int[4];
        for (int i = 0; i < intensityArray.Length; i++)
        {
            intensityArray[i] = RandomIntensity();
        }
        return intensityArray;
    }
    Vector3 RandomPos()
    {
        float xzRange = 20;
        float yRange = 1.5f;
        float xPos = Random.Range(-xzRange, xzRange);
        float yPos = Random.Range(0, yRange);
        float zPos = Random.Range(-xzRange, xzRange);

        return new Vector3(xPos, yPos, zPos);
    }

    int RandomIntensity()
    {
        int intensity = Random.Range(0, 10) + 1;
        return intensity;
    }

    // Button Functions
    /*public void SizeButton()
    {
        if (isSpawned)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("All Data Objects");
            foreach (GameObject gameObject in taggedObjects)
            {
                Destroy(gameObject);
            }
        }
        VisualizeDataSz(dataPosArray, intensityArray);
        isSpawned = true;
    }*/
    //public void SizeColorButton()
    //{
    //    if (isSpawned)
    //    {
    //        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("All Data Objects");
    //        foreach (GameObject gameObject in taggedObjects)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //    VisualizeDataSzCl(dataPosArray, intensityArray);
    //    isSpawned = true;
    //}

    // Visulize Data by Size only
    // Intensity level is assumed to be 1-10
    // 'intensity' as 1: 'scaleMin'
    // 'scale' increments by 0.25f as 'itensity' increases 1
    // More options: let the user choose a color!
    void VisualizeDataSz(Vector3[] dataPosArray, int[] intensityArray, int numData)
    {
        for (int i = 0; i < numData; i++)
        {
            float scale = scaleMin + (intensityArray[i] - 1) * scaleInterval;
            layeredDateObject.transform.localScale = new Vector3(scale, scale, scale);
            layeredDataObjects[i] = Instantiate(layeredDateObject, transform.parent, false) as GameObject;
            /*layeredDataObjects[i].transform.localPosition = dataPosArray[i];*/
        }
    }
    void UpdateDataSz(int index)
    {
        float scale = scaleMin + (intensityArray[index] - 1) * scaleInterval;
        layeredDataObjects[index].transform.localScale = new Vector3(scale, scale, scale);
        layeredDataObjects[index].transform.localPosition = dataPosArray[index];
    }


    // Visulize Data by Size and Color
    // Opaque Color because hard to tell the color differences if transparent
    public void VisualizeDataSzCl(Vector3[] dataPosArray, int[] intensityArray, int numData)
    {
        for (int i = 0; i < numData; i++)
        {
            //float scale = scaleMin + (intensityArray[i] - 1) * scaleInterval;
            float scale = 0.05f;
            dataObject.transform.localScale = new Vector3(scale, scale, scale);
            dataObject.GetComponent<MeshRenderer>().material = intensityColor[intensityArray[i] - 1];
            dataObjects[i] = Instantiate(dataObject, transform.parent, false) as GameObject;
            //dataObject.transform.localPosition = dataPosArray[i];
        }
    }

    void UpdateDataSzCl(int index)
    {
        float scale = scaleMin + (intensityArray[index] - 1) * scaleInterval;
        dataObjects[index].GetComponent<MeshRenderer>().material = intensityColor[intensityArray[index] - 1];
        //dataObjects[index].transform.localScale = new Vector3(scale, scale, scale);
        dataObjects[index].transform.localPosition = dataPosArray[index];
        dataObjects[index].transform.rotation = Quaternion.identity;
    }

}

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataVisualizer : MonoBehaviour
{
    public GameObject dataObject;
    public GameObject layeredDateObject;
    public Material[] intensityColor;
    private float scaleMin = 0.5f;
    private float scaleInterval = 0.25f;
    public int numData;
    //private List<Vector3> dataPosList = new List<Vector3>();
    //private List<int> intensityList = new List<int>();
    public Vector3[] dataPosArray;
    public int[] intensityArray;

    public bool isSpawned = false;

    Vector3[] datapoints;
    bool data_received;
    GameObject[] data_visual;
    public GameObject visual;
    GameObject marker_frame;
    bool flag;

    bool executed_once;
    bool execute_flag2;
    int dataCount;
    
    public GameObject[] layeredDataObjects;

    // Start is called before the first frame update
    void Start()
    {
        scaleMin = 0.01f;
        scaleInterval = 0.01f;
        //dataPosList.Add(new Vector3(0, 0, 0));
        //intensityList.Add(1);
        //VisualizeDataSz(dataPosList, intensityList);





        

        datapoints = new Vector3[4];

        marker_frame = GameObject.Find("marker_frame");



    }

    void Update()
    {
        data_received = FindObjectOfType<point_sub>().data_sent;
        if (data_received)
        {
            dataCount = FindObjectOfType<point_sub>().data_count;
            if (!executed_once)
            {
                dataPosArray = new Vector3[dataCount];
                intensityArray = new int[dataCount];
                layeredDataObjects = new GameObject[dataCount];
                executed_once = true;
            }


            for (int i = 0; i < dataCount; i++)
            {
                dataPosArray[i] = FindObjectOfType<point_sub>().data[i];
                intensityArray[i] = FindObjectOfType<point_sub>().intensity[i];

            }

            if (!flag)
            {
                VisualizeDataSz(dataPosArray, intensityArray, dataCount);
                flag = true;
            }

            for (int i = 0; i < 4; i++)
            {
                //data_visual[i].transform.localPosition = datapoints[i];
                //data_visual[i].transform.localRotation = Quaternion.identity;
                //data_visual[i].transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                UpdateDataSz(i);
            }

        }


    }


    Vector3[] GenerateDataPosArray()
    {
        dataPosArray = new Vector3[4];
        for (int i = 0; i < dataPosArray.Length; i++)
        {
            dataPosArray[i] = RandomPos();
        }
        return dataPosArray;

    }

    int[] GenerateIntensityArray()
    {
        intensityArray = new int[4];
        for (int i = 0; i < intensityArray.Length; i++)
        {
            intensityArray[i] = RandomIntensity();
        }
        return intensityArray;
    }
    Vector3 RandomPos()
    {
        float xzRange = 20;
        float yRange = 1.5f;
        float xPos = Random.Range(-xzRange, xzRange);
        float yPos = Random.Range(0, yRange);
        float zPos = Random.Range(-xzRange, xzRange);

        return new Vector3(xPos, yPos, zPos);
    }

    int RandomIntensity()
    {
        int intensity = Random.Range(0, 10) + 1;
        return intensity;
    }

    // Button Functions
    *//*public void SizeButton()
    {
        if (isSpawned)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("All Data Objects");
            foreach (GameObject gameObject in taggedObjects)
            {
                Destroy(gameObject);
            }
        }
        VisualizeDataSz(dataPosArray, intensityArray);
        isSpawned = true;
    }*//*
    public void SizeColorButton()
    {
        if (isSpawned)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag("All Data Objects");
            foreach (GameObject gameObject in taggedObjects)
            {
                Destroy(gameObject);
            }
        }
        VisualizeDataSzCl(dataPosArray, intensityArray);
        isSpawned = true;
    }

    // Visulize Data by Size only
    // Intensity level is assumed to be 1-10
    // 'intensity' as 1: 'scaleMin'
    // 'scale' increments by 0.25f as 'itensity' increases 1
    // More options: let the user choose a color!
    void VisualizeDataSz(Vector3[] dataPosArray, int[] intensityArray, int numData)
    {
        
        

        for (int i = 0; i < numData; i++)
        {
            float scale = scaleMin + (intensityArray[i] - 1) * scaleInterval;
            layeredDateObject.transform.localScale = new Vector3(scale, scale, scale);
            layeredDataObjects[i] = Instantiate(layeredDateObject, transform.parent, false) as GameObject;
            *//*layeredDataObjects[i].transform.localPosition = dataPosArray[i];*//*
        }

    }
    void UpdateDataSz(int index)
    {
        float scale = scaleMin + (intensityArray[index] - 1) * scaleInterval;
        layeredDataObjects[index].transform.localScale = new Vector3(scale, scale, scale);
        layeredDataObjects[index].transform.localPosition = dataPosArray[index];
    }



    // Visulize Data by Size and Color
    // Opaque Color because hard to tell the color differences if transparent
    public void VisualizeDataSzCl(Vector3[] dataPosArray, int[] intensityArray)
    {
        numData = dataPosArray.Length;

        for (int i = 0; i < numData; i++)
        {
            float scale = scaleMin + (intensityArray[i] - 1) * scaleInterval;
            dataObject.transform.localScale = new Vector3(scale, scale, scale);
            dataObject.GetComponent<MeshRenderer>().material = intensityColor[intensityArray[i] - 1];
            Instantiate(dataObject, transform.parent, false);
            dataObject.transform.localPosition = dataPosArray[i];
        }
    }

}*/