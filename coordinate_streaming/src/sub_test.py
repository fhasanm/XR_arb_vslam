import numpy as np
from sklearn.preprocessing import normalize
import math

import rospy
import numpy as np
import pandas as pd
from scipy.spatial.transform import Rotation as Rot
from geometry_msgs.msg import Polygon, Point32
from std_msgs.msg import Int32

pub_data = rospy.Publisher('unity_data', Polygon, queue_size=1)

def angle_between(vector_1, vector_2):
    unit_vector_1 = vector_1 / np.linalg.norm(vector_1)
    unit_vector_2 = vector_2 / np.linalg.norm(vector_2)
    dot_product = np.dot(unit_vector_1, unit_vector_2)
    angle = np.arccos(dot_product)
    return angle * 180 / math.pi


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

def marker_data(marker_id):
    if marker_id == 300:
        filepath = "/home/cviss_lab/catkin_ws/src/coordinate_streaming/src/300_marker.csv"
        df= pd.read_csv(filepath)
        a = df.iloc[0:4, 1:4].to_numpy(copy=True)
        return a
    elif marker_id == 301:
        filepath = "/home/cviss_lab/catkin_ws/src/coordinate_streaming/src/301_marker.csv"
        df= pd.read_csv(filepath)
        a = df.iloc[0:4, 1:4].to_numpy(copy=True)
        return a

def dataframe():
    filepath = "/home/cviss_lab/catkin_ws/src/coordinate_streaming/src/data.csv"
    df= pd.read_csv(filepath)
    a = df.iloc[0:4, 1:4].to_numpy(copy=True)
    return a


def callback(marker):

    vertices = marker_data(marker)
    data = dataframe()
    transformed_points = transformation(data, vertices)
    print(transformed_points)
    point_array = Polygon()
    point = Point32()
    for i, vectors in enumerate(transformed_points):
        point.x = vectors[0]
        point.y = vectors[1]
        point.z = vectors[2]
        point_array.points.append(point)

    pub_data.publish(point_array)

    
if __name__ == "__main__":
    print(marker_data(300))