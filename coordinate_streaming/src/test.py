import numpy as np
from sklearn.preprocessing import normalize
import math

def angle_between(vector_1, vector_2):
    unit_vector_1 = vector_1 / np.linalg.norm(vector_1)
    unit_vector_2 = vector_2 / np.linalg.norm(vector_2)
    dot_product = np.dot(unit_vector_1, unit_vector_2)
    angle = np.arccos(dot_product)
    return angle * 180 / math.pi


a = np.array([3.268002272,-0.142024219,-0.320139855])
b = np.array([3.135512829,-0.10053283,-0.384090245])
c = np.array([3.163381815,0.055291858,-0.373023659])
d = np.array([3.300274372,0.017291896,-0.314827025])
#e = np.array([3.224602,-0.036679,-0.341440])

vertices = np.array([[3.268002272,-0.142024219,-0.320139855], 
    [3.135512829,-0.10053283,-0.384090245], 
    [3.163381815,0.055291858,-0.373023659],
    [3.300274372,0.017291896,-0.314827025]])
e = np.average(vertices, axis=0)
# test
# a = np.array([0.03,0.04,0.02])
# b = np.array([0.03,-1.02,0.03])
# d = np.array([-1.04,0.04,0.03])
# e = np.array([-0.52,-0.54,-0.02, -1])

# Assuming the marker coordinate system is coherent with ros

# pose_xa = a - d
# pose_x = pose_xa.reshape(-1,1)
# pose_ya = a - b
# pose_za = np.cross(pose_xa, pose_ya)

# pose_z = pose_za.reshape(-1,1)

# pose_ya= np.cross(pose_za, pose_xa)
# pose_y = pose_ya.reshape(-1,1)


# Assuming the marker coordinate system is coherent with unity

pose_xa = b - a
pose_x = pose_xa.reshape(-1,1)
pose_za = a - d
pose_ya = np.cross(pose_xa, pose_za)

pose_y = pose_ya.reshape(-1,1)

pose_za= np.cross(pose_ya, pose_xa)
pose_z = pose_za.reshape(-1,1)

#check if all 90
print(angle_between(pose_x.reshape(-1), pose_y.reshape(-1)))
print(angle_between(pose_x.reshape(-1), pose_z.reshape(-1)))
print(angle_between(pose_z.reshape(-1), pose_y.reshape(-1)))
    

R = np.concatenate((pose_x, pose_y, pose_z), axis=1)
print(R)

R = normalize(R, axis=0, norm='l2')
print(R.T)

# print(R.T)


# #R = np.linalg.inv(R)
# # R = np.linalg.inv(R)
# # print(R)






#P_ros = np.array([-1.02,-1.02, 0.03, 1]).reshape(-1,1)
P_ros = np.array([3.181484,0.177250,-0.372977]).reshape(-1,1)
# # transform = np.eye(4)
# # for i, element in enumerate(transform):
# #     if i<3:
# #         element[-1] = e[i]
# #     for elmenet 


P_marker = R.T @ P_ros
e = R.T @ e.reshape(-1,1)
P_marker = P_marker - e.reshape(-1,1)

# T = np.concatenate((R.T,np.array([[0, 0, 0]])), axis = 0)
# T = np.concatenate((T,-e.reshape(-1,1)), axis=1)
# print(T)
# P_marker = T@P_ros



print(P_marker)


# A function that takes in a numpy point array i.e. four points
# stacked horizontally or basically a dataframe style array of points
# It also takes in data, which is another array of

a = np.array([ 3.608229, -0.065698,  0.802331])
b = np.array([3.65608049, 0.07569339, 0.81711981])
c = np.array([3.78736061, 0.01902592, 0.86244451])
d = np.array([ 3.73950912, -0.12236547,  0.8476557 ])
