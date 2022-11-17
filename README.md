# XR_arb_vslam
 
# Visual Localization of XR device on arbitrary map 
By [Fuad Hasan](https://www.linkedin.com/in/fuadhasanm/)

## Problem 
Assuming we have collected some arbitrary data on an arbitrary frame, how do we localize that data on an augmented scene frame and visualize that data on the same scene such as that of Varjo XR3? 


## Methodology

### Setup your Linux ROS TCP connection
Check out and clone the following repositiory in your ROS packages src directory:
(https://github.com/Unity-Technologies/ROS-TCP-Endpoint)

In the launch/endpoint.launch file, change the value of "default=0.0.0.0" in Line 2 to the value of the IP address of your ROS system.

Transfer the coordinate_streaming package from this repo to your ROS system packages src directory. 

Run the launch file from the coordinate_streaming package. It will set up the endpoint connection and the ROS nodes required for the localization to run.

### Adding your arbitrary data to the script for visualization

Firstly, the localization works via marker-tracking. Therefore, while collecting data, it is assumed that the four vertex coordinates of the marker was also recorded as a csv file in the same format and filename as src/301_marker.csv where x, y, z are the coordinates of the vertices in the same arbitrary frame of the data.
Then, edit the data_transform.py file. Just add an elif block with the same format and change the marker_id and filepath values. 

Second, by default, the data that is visualized is a grid of coordinates and integer values separately. To add your own arbitrary data, follow the format of the data.csv file, uncomment the dataframe() function and change the filepath to the path of your csv file. 

### Setup Unity TCP connection

In order for Unity to connect to the ROS TCP endpoint, you need to add the ROS-TCP-Connector to Unity:
(https://github.com/Unity-Technologies/ROS-TCP-Connector)
Follow the steps to set up the TCP connection. Afterwards, add an Empty GameObject and add the ROS Connection script to that GameObject. From the inspector of that GameObject, set the values of the IP and port number to the device where ROS-TCP-Endpoint is running.
In the XRRig>ROSConnection GameObject, set the values of the IP and port number to the device where ROS-TCP-Endpoint is running. 

### Setup your Varjo XR3 device

Follow the following page to setup Varjo Base and set it up for Unity. Put [Varjo's Reference Marker](https://developer.varjo.com/docs/get-started/varjo-markers) at a fixed point in the room before the callibration, and then Callibrate Varjo via its Inside-Out Tracking in Vrjo Base > System. Once Callibration is done, run the Unity script. 
