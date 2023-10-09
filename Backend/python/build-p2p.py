#argv[1] = assetpath
#argv[2] = x_range min
#argv[3] = x_range max
#argv[4] = y_range min
#argv[5] = y_range max
#argv[6] = xystep
#argv[7] = rho
#argv[8] = lambda
#argv[9] = axon threshold
#argv[10] = number axons
#argv[11] = number axon segments
#argv[12] = use left eye?


# How to call from the command line: $ python build-p2p.py
import pulse2percept as p2p
import sys
import numpy as np

def main(argv):
    assetpath = argv[1]   
    
    # Define the width and height of the little screen (in degrees of visual
    # angle). This is the window into the world for which we will simulate
    # the prosthetic vision. (0,0) is the center of vision, anchored to a
    # specific eye:
    x_range = (float(argv[2]), float(argv[3]))
    y_range = (float(argv[4]), float(argv[5]))
    #x_range = (-20, 20)
    #y_range = (-20, 20)

    # - `xystep` is the degree/pixel step size of the grid. For example,
    #   xystep=1 with x_range=(-20, 20) creates a grid [-20, -19, ..., 19, 20].
    # - `xystep` is tied to the screen resolution, and must be the same for x
    #   and y. For example, on a 1024x768 screen spanning a 60deg field of view
    #   (FOV) horizontally, one xystep=60/1024 deg/px.
    xystep = float(argv[6])

    # - 'rho' and 'lambda' are used for simulating percepts when the axon is stimulated
    rho = float(argv[7])
    axlambda = float(argv[8])
    
    min_ax_sensitivity = float(argv[9])
    num_axon = int(argv[10])
    num_axon_seg = int(argv[11])
    useLeftEye = bool(argv[12])
            
    # - Axon map is different for left eye ('LE') vs. right eye ('RE')
    eye = 'RE'
    if useLeftEye:
        eye = 'LE'

    print("Path: "+str(assetpath))
    print("X Range: " + str(x_range[0])+","+str(x_range[1]))
    print("Y Range: " + str(y_range[0])+","+str(y_range[1]))
    print("XYstep: " + str(xystep))
    print("Calcuating axon map, please wait...")


    # Build the axon map model:
    model = p2p.models.AxonMapModel(xrange=x_range, yrange=y_range, xystep=xystep, eye=eye, axlambda = axlambda, rho = rho, min_ax_sensitivity = min_ax_sensitivity, n_axons = num_axon, n_ax_segments = num_axon_seg, ignore_pickle = True)
    model.build()

    # - For each pixel in the grid created above (check out `model.grid.x` and
    #   `model.grid.y`), the model stores information about the axon belonging
    #   to the neuron whose cell body sits at that pixel location.
    # - The axon is chunked into segments, each of which has its own exact
    #   (x,y) location whose resolution is unrelated to the grid created above.
    # - `model.axon_contrib` is a 2D float32 NumPy array that stores all the
    #   axon segments. Each row describes an axon segment: its x location, y
    #   location, and its contribution to the brightness at the corresponding
    #   pixel location (depends on ``axlambda``). With S segments, it's
    #   therefore a Sx3 array.
    # - Because not every axon has the same number of segments, all S segments
    #   are stacked on top of each other. For example, the axon belonging to
    #   the i-th pixel has segments
    #   `model.axon_contrib[model.axon_idx_start[i]:model.axon_idx_end[i]]`
    sizeOfAxonContrib = np.array([model.axon_contrib.shape])
    with open(assetpath+'axon_contrib_length.dat', 'wb') as f:
        sizeOfAxonContrib.tofile(f)
        
    with open(assetpath+'axon_contrib.dat', 'wb') as f:
        model.axon_contrib.tofile(f)

    # - `model.axon_idx_start` is a Nx1 int32 array.
    # - The i-th element gives the start index to `model.axon_contrib` for all
    #   axon segments belonging to the neuron encoding the i-th pixel location:
    with open(assetpath+'axon_idx_start.dat', 'wb') as f:
        model.axon_idx_start.tofile(f)

    # - Analogously, `model.axon_idx_end` gives the end index:
    with open(assetpath+'axon_idx_end.dat', 'wb') as f:
        model.axon_idx_end.tofile(f)


if __name__ == "__main__":
    main(sys.argv)