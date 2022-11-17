#! /usr/bin/env python3
from distutils.log import debug

import numpy as np
from sklearn.preprocessing import normalize
import click
import rospy
import numpy as np
import pandas as pd
from scipy.spatial.transform import Rotation as Rot
from geometry_msgs.msg import Pose, Transform


# Input: expects 3xN matrix of points
# Returns R,t*---**********************************
# R = 3x3 rotation matrix
# t = 3x1 column vector




# def rigid_transform_3D(A, B):
    
#     assert A.shape == B.shape

#     num_rows, num_cols = A.shape
#     if num_rows != 3:
#         raise Exception(f"matrix A is not 3xN, it is {num_rows}x{num_cols}")

#     num_rows, num_cols = B.shape
#     if num_rows != 3:
#         raise Exception(f"matrix B is not 3xN, it is {num_rows}x{num_cols}")

#     # find mean column wise
#     centroid_A = np.mean(A, axis=1)
#     centroid_B = np.mean(B, axis=1)

#     # ensure centroids are 3x1
#     centroid_A = centroid_A.reshape(-1, 1)
#     centroid_B = centroid_B.reshape(-1, 1)

#     # subtract mean
#     Am = A - centroid_A
#     Bm = B - centroid_B

#     H = Am @ np.transpose(Bm)

#     # sanity check
#     #if linalg.matrix_rank(H) < 3:
#     #    raise ValueError("rank of H = {}, expecting 3".format(linalg.matrix_rank(H)))

#     # find rotation
#     U, S, Vt = np.linalg.svd(H)
#     R = Vt.T @ U.T

#     # special reflection case
#     if np.linalg.det(R) < 0:
#         print("det(R) < R, reflection detected!, correcting for it ...")
#         Vt[2,:] *= -1
#         R = Vt.T @ U.T
#     t = -R @ centroid_A + centroid_B
#     r = Rot.from_matrix(R)
#     Q = r.as_quat()
#     return Q, t



def affine_transform(points):
    pose_xa = points[1] - points[0]
    pose_x = pose_xa.reshape(-1,1)
    pose_za = points[0] - points[3]
    pose_z = pose_za.reshape(-1,1)
    pose_y = np.cross(pose_xa, pose_za).reshape(-1, 1)
    R = np.concatenate((pose_x, pose_y, pose_z), axis=1)
    R = normalize(R)

    P_ros = np.array([3.181484,0.177250,-0.372977])
    P_marker = R.T @ P_ros
    r = Rot.from_matrix(R)
    rotation = r.as_quat()
    
    position = np.average(points, axis=0)
    
    return position, rotation


def normalize(v):
    norm = np.linalg.norm(v)
    if norm == 0: 
       return v
    return v / norm
    
    

# def test(data):
#     p = np.array([[]]
#     p, r = affine_transform(dataread())
#     unityq = [0.1, 0,2, 0.3, 0.4]
  
#     # Quaternion difference = qdiff * unityq = rosq --->  qdiff = rosq * conjugate(unityq) / abs(unityq)
#     conjugate_unityq = np.array([[-unityq[0], -unityq[1], -unityq[2], unityq[3]]]) 
#     abs_unity = np.linalg.norm(np.array([unityq[0], unityq[1], unityq[2], unityq[3]]))
#     inv = conjugate_unityq / abs_unity
#     rot = r @ inv

    

def main():
    p, r = affine_transform(dataread())
    trans = Transform()
    # trans.translation.x = data.position.x - p[0]
    # trans.translation.y = data.position.y - p[1]
    # trans.translation.z = data.position.z - p[2]
    # # Quaternion difference = qdiff * unityq = rosq --->  qdiff = rosq * conjugate(unityq) / abs(unityq)
    # conjugate_unityq = np.array([[-data.orientation.x, -data.orientation.y, -data.orientation.z, data.orientation.w]]) 
    # abs_unity = np.linalg.norm(np.array([data.orientation.x, data.orientation.y, data.orientation.z, data.orientation.w]))
    # inv = conjugate_unityq / abs_unity
    # rot = r.reshape(-1,1) @ inv
    # rot = np.array(rot, dtype=np.float64)

    trans.translation.x = p[0]
    trans.translation.y = p[1]
    trans.translation.z = p[2]
    trans.rotation.x = r[0]
    trans.rotation.y = r[1]
    trans.rotation.z = r[2]
    trans.rotation.w = r[3]
    print("trans prepared")
    pub_rot.publish(trans)



def dataread():
    filepath = "/home/cviss_lab/catkin_ws/src/coordinate_streaming/src/varjo_marker_coordinates.csv"
    df= pd.read_csv(filepath)
    a = df.iloc[0:4, 1:4].to_numpy(copy=True)
    return a
    
if __name__ == "__main__":

    rospy.init_node('hololens_transform', anonymous=True)

    pub_rot = rospy.Publisher('transform',Transform, queue_size=1)
    hz1 = rospy.Rate(1)
    
    while not rospy.is_shutdown():
        main()
        hz1.sleep()
    
    # rospy.Subscriber('pose', Pose, callback)
    
    # rospy.spin()

    
    
    



    
#Should I transpose?
#quaternions are defaulted to [x y z w]

    

# def callback(data):
    
#     click_array = np.empty((4,3))
        
#     i=0
#     for vectors in click_array:            
#         vectors[0] = data.points[i].x
#         vectors[1] = data.points[i].y
#         vectors[2] = data.points[i].z
#         #print(click_array)
#         i+=1normalize(np.array(
#     trans.rotation.x = Q[0]
#     trans.rotation.y = Q[1]
#     trans.rotation.z = Q[2]
#     trans.rotation.w = Q[3]
#     trans.translation.x = t[0]
#     trans.translation.y = t[1]
#     trans.translation.z = t[2]

#     pub_rot.publish(trans)

    

# def dataread():
#     filepath = "/home/cviss_lab/catkin_ws/src/coordinate_streaming/src/varjo_marker_coordinates.csv"
#     df= pd.read_csv(filepath)
#     a = df.iloc[0:4, 1:4].to_numpy(copy=True)
#     return a
    
# if __name__ == "__main__":
#     rospy.init_node('hololens_transform', anonymous=True)
    
    
    
#     rospy.Subscriber('clicks', Polygon, callback)
    
#     rospy.spin()

    
    
    



    
#Should I transpose?
#quaternions are defaulted to [x y z w]

    