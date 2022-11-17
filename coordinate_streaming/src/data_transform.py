#!/usr/bin/env python3

import numpy as np
from sklearn.preprocessing import normalize
import math

import rospy
import numpy as np
import pandas as pd
from scipy.spatial.transform import Rotation as Rot
from geometry_msgs.msg import Polygon, Point32
from std_msgs.msg import Int32MultiArray, Int32

pub_points = rospy.Publisher('unity_points', Polygon, queue_size=10)
pub_int = rospy.Publisher('unity_int', Int32MultiArray, queue_size=10)
pub_count = rospy.Publisher('unity_count', Int32, queue_size=10)

def angle_between(vector_1, vector_2):
    unit_vector_1 = vector_1 / np.linalg.norm(vector_1)
    unit_vector_2 = vector_2 / np.linalg.norm(vector_2)
    dot_product = np.dot(unit_vector_1, unit_vector_2)
    angle = np.arccos(dot_product)
    return angle * 180 / math.pi

def planar_grid_on_3D(vertices, separation_len):

    data = np.empty((0,3), float)
    no_line_seperations = int(np.linalg.norm(vertices[2]-vertices[3])/separation_len)
    line = np.linspace(vertices[3], vertices[2], no_line_seperations)
    offset = separation_len*(1/np.linalg.norm(vertices[0]-vertices[3])*(vertices[0]-vertices[3]))
    data = np.append(data, line, axis=0)

    for n in range(int(np.linalg.norm(vertices[0]-vertices[3])/separation_len)):
        end = offset + data[-1]
        start = offset + data[-(no_line_seperations)]
        new_line = np.linspace(start, end, no_line_seperations)
        data = np.append(data, new_line, axis=0)

    return data

def transformation(points, marker_vertices):

    #points n x 3 np array
    #marker_vertices 4 x 3 np array
    centroid = np.average(marker_vertices, axis=0)
    pose_xa = marker_vertices[1] - marker_vertices[0]
    pose_x = pose_xa.reshape(-1,1)
    pose_za = marker_vertices[0] - marker_vertices[3]
    pose_ya = np.cross(pose_xa, pose_za)
    pose_y = pose_ya.reshape(-1,1)
    pose_za= np.cross(pose_ya, pose_xa)
    pose_z = pose_za.reshape(-1,1)
    #check if all 90
    # print(angle_between(pose_x.reshape(-1), pose_y.reshape(-1)))
    # print(angle_between(pose_x.reshape(-1), pose_z.reshape(-1)))
    # print(angle_between(pose_z.reshape(-1), pose_y.reshape(-1)))

    R = np.concatenate((pose_x, pose_y, pose_z), axis=1)
    R = normalize(R, axis=0, norm='l2')
    P_ros = points.T

    #transform by rotation first and then translation
    P_marker = R.T @ P_ros
    centroid = R.T @ centroid.reshape(-1,1)
    P_marker = P_marker - centroid.reshape(-1,1)

    # RETURN n x 3 so
    return P_marker.T
    
# I want my algorithm to take in any known marker points in the future and datapoints as well
# it will localize the data so that it can be visualuzed on Varjo 


def marker_data(marker_id):
    root = "/home/cviss_lab/catkin_ws/src/coordinate_streaming/src/"
    if marker_id == 302:
        filepath = root + "302_marker.csv"
        df= pd.read_csv(filepath)
        a = df.iloc[0:4, 1:4].to_numpy(copy=True)
        return a
    elif marker_id == 301:
        filepath = root + "301_marker.csv"
        df= pd.read_csv(filepath)
        a = df.iloc[0:4, 1:4].to_numpy(copy=True)
        return a
    elif marker_id == 304:
        filepath = root + "304_marker.csv"
        df= pd.read_csv(filepath)
        a = df.iloc[0:4, 1:4].to_numpy(copy=True)
        return a
    elif marker_id == 305:
        filepath = root + "305_marker.csv"
        df= pd.read_csv(filepath)
        a = df.iloc[0:4, 1:4].to_numpy(copy=True)
        return a
    elif marker_id == 306:
        filepath = root + "306_marker.csv"
        df= pd.read_csv(filepath)
        a = df.iloc[0:4, 1:4].to_numpy(copy=True)
        return a
    

# #Visualize four points in sdic
def dataframe():
    filepath = "/home/cviss_lab/catkin_ws/src/coordinate_streaming/src/data.csv"
    df= pd.read_csv(filepath)
    points = df.iloc[0:4, 1:4].to_numpy(copy=True)
    int = df.iloc[0:4, 4].to_numpy(copy=True)
    return points, int


# # Visualize grid in sdic
# def dataframe():
#     vertices1 = np.array([[3.868067264557,0.525567650795,0.797985255718],
#         [3.682871580124,0.008146121167,0.729879736900],
#         [3.882496595383,-0.018041867763,0.218425542116],
#         [4.104630470276,0.509674787521,0.291629314423]])
#     vertices2 = np.array([[3.569483041763,-0.396687656641,0.729679524899],
#         [3.388985633850,-0.921267330647,0.661168694496],
#         [3.633138895035,-0.950550854206,0.160199016333],
#         [3.800356388092,-0.423501789570,0.225526183844]])
#     grid1 = planar_grid_on_3D(vertices1,0.05)
#     grid2 = planar_grid_on_3D(vertices2,0.05)randomInts[x for x in random]

#Visualize Grid in Structures Lab
# def dataframe():

#     vertices1 = np.array([[21.225635528564,1.465494155884,-3.737819194794],
#         [21.434080123901,-0.906795442104,-3.794130325317],
#         [2.843964338303,-0.992843151093,-1.554784893990],
#         [2.853264808655,0.835537195206,-1.530918717384]])
#     vertices2 = np.array([[2.153766870499,-4.706916809082,-1.595539927483],
#         [2.203196287155,-6.810250282288,-1.645994067192],
#         [21.812911987305,-5.735001087189,-3.979870319366],
#         [21.621103286743,-3.695419549942,-3.926954507828]])
#     vertices3 = np.array([[9.239838600159,-2.138735294342,-2.343685150146],
#         [8.522959709167,-4.500967025757,-2.333038806915],
#         [6.042370319366,-4.383880615234,-2.056563138962],
#         [7.410479545593,-2.145559787750,-2.142096757889]])
    
#     grid_res = 0.2
#     grid1 = planar_grid_on_3D(vertices1, grid_res)
#     grid2 = planar_grid_on_3D(vertices2, grid_res)
#     grid3 = planar_grid_on_3D(vertices3, grid_res)
#     np.random.seed(5)
#     gridint1 = np.random.randint(1,4,len(grid1))
#     gridint2 = np.random.randint(8,10,len(grid2))
#     gridint3 = np.random.randint(5,7,len(grid3))
#     data = np.concatenate((grid1, grid2, grid3), axis = 0)
#     ints = np.concatenate((gridint1, gridint2, gridint3), axis = 0).reshape(len(data))
#     return data, ints

#Change this grid and change filepath



def callback(marker):

    vertices = marker_data(marker.data)
    #vertices = marker_data(marker)
    #print(vertices)
    point_data, int_data = dataframe()
    # print(dataframe)
    transformed_points = transformation(points = point_data, marker_vertices=vertices)
    
    # normalize the int here

    # print(transformed_points)
    point_array = Polygon()
    
    for vectors in transformed_points:
        point = Point32()
        point.x = vectors[0]
        point.y = vectors[1]
        point.z = vectors[2]
        point_array.points.append(point)

    count_msg = Int32()
    count_msg.data = len(transformed_points)

    int_array = Int32MultiArray()
    for i in int_data:
        
        int_array.data.append(int(i))

    
    pub_points.publish(point_array)
    pub_count.publish(count_msg)
    pub_int.publish(int_array)

    


if __name__ == "__main__":
    rospy.init_node('data_transform', anonymous=True)
    hz1 = rospy.Rate(10)

    rospy.Subscriber('marker_id', Int32, callback)

    # while not rospy.is_shutdown():
    #     callback(301)
    #     hz1.sleep()

    rospy.spin()

