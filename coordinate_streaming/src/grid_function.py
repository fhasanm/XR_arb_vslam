import matplotlib.pyplot as plt
import numpy as np

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


def dataframe():
    
    fig = plt.figure(figsize=(12, 12))
    ax = fig.add_subplot(projection='3d')
    vertices = np.array([[3.868067264557,0.525567650795,0.797985255718],
        [3.682871580124,0.008146121167,0.729879736900],
        [3.882496595383,-0.018041867763,0.218425542116],
        [4.104630470276,0.509674787521,0.291629314423]])
    
    data = planar_grid_on_3D(vertices,0.05)
    ints = 2*np.ones(len(data))
    
 
    # Creating plot
    ax.scatter(data[:,0], data[:,1], data[:,2],  c = 'b', marker='o')
    
    plt.title("3D scatter plot")
    
   
    ax.set_xlabel('X-axis')
    ax.set_ylabel('Y-axis')
    ax.set_zlabel('Z-axis')
    # show plot
    plt.show()
    

  

if __name__ == "__main__":
    dataframe()

# plt.scatter(mid_x, mid_y, color='r')
# plt.show()

# ax = plt.axes(projection='3d')

# # Data for a three-dimensional line
# zline = np.linspace(0, 15, 1000)
# xline = np.sin(zline)
# yline = np.cos(zline)
# ax.plot3D(xline, yline, zline, 'gray')

# # Data for three-dimensional scattered points
# zdata = 15 * np.random.random(100)
# xdata = np.sin(zdata) + 0.1 * np.random.randn(100)
# ydata = np.cos(zdata) + 0.1 * np.random.randn(100)
# ax.scatter3D(xdata, ydata, zdata, c=zdata, cmap='Greens')