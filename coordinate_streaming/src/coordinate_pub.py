#!/usr/bin/env python3

import rospy
import pandas as pd
import numpy as np
from geometry_msgs.msg import Polygon, Point32



if __name__ == "__main__":

    rospy.init_node('coordinate_pub', anonymous=True)
    pub = rospy.Publisher('marker_coordinate_stream', Polygon, queue_size=1, latch=True)
    markers =Polygon()
    hz1 = rospy.Rate(1)
    filepath = "/home/cviss_lab/catkin_ws/src/coordinate_streaming/src/varjo_marker_coordinates.csv"
    df= pd.read_csv(filepath)
    a = df.iloc[0:4, 1:4].to_numpy(copy=True)

    centroid = np.average(a, axis=0)
    markers =Polygon() 
    
    #print(type(markers.points))
    #print(df_extracted)
    #make a for loop instead
    
    #row_no = 0
    for vectors in a:
        point = Point32()
        point.x = vectors[0]
        point.y = vectors[1]
        point.z = vectors[2]
        markers.points.append(point) 

    point = Point32()
    point.x = centroid[0]
    point.y = centroid[1]
    point.z = centroid[2]
    markers.points.append(point)

    pub.publish(markers)
    #print(markers.points)

    while not rospy.is_shutdown():
        
        hz1.sleep()
    
    #print("main")
    #rospy.spin()
        

        


   




