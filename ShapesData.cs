namespace Tetris;
public static class ShapesData
{

    public static readonly List<List<int[,]>> shapes = new List<List<int[,]>>
    {
    
        // Square block (only one rotation)
        new List<int[,]>
        {
            new int[,]
            {
                {0, 0}, {0, 1}, {0, 2}, {0, 3}, {1, 0}, {1, 1}, {1, 2}, {1, 3}
            }
        },

        // Long block (two rotations)
        new List<int[,]>
        {
            new int[,]
            {
                {1, 0}, {1, 1}, {1, 2}, {1, 3}, {1, -1}, {1, -2}, {1, 4}, {1, 5} // Horizontal rotation
            },
            new int[,]
            {
                {0, 0}, {1, 0}, {2, 0}, {3, 0}, {0, 1}, {1, 1}, {2, 1}, {3, 1} // Vertical rotation
            }
        },

        // L block right (four rotations)
        new List<int[,]>
        {
            new int[,]
            {
                {0, -2}, {0, -1}, {0, 0}, {0, 1}, {0, 2}, {0, 3}, {1, 2}, {1, 3} // Rotation 180 degrees
            },
            new int[,]
            {
                {1, -2}, {1, -1}, {1, 0}, {1, 1}, {0, 0}, {0, 1}, {-1, 0}, {-1, 1} // Rotation 90 degrees
            },
            new int[,]
            {
                {1, -2}, {1, -1}, {1, 0}, {1, 1}, {1, 2}, {1, 3}, {0, -2}, {0, -1} // Rotation 0 degrees
            },
            new int[,]
            {
                {-1, 0}, {-1, 1}, {1, 0}, {1, 1}, {0, 0}, {0, 1}, {-1, 2}, {-1, 3} // Rotation 270 degrees
            }
        },

        // L block left (four rotations)
        new List<int[,]>
        {
            new int[,]
            {
                {0, -2}, {0, -1}, {0, 0}, {0, 1}, {0, 2}, {0, 3}, {1, -2}, {1, -1} // Rotation 0 degrees
            },
            new int[,]
            {
                {-1, 0}, {-1, 1}, {1, 0}, {1, 1}, {0, 0}, {0, 1}, {-1, -1}, {-1, -2} // Rotation 270 degrees
            },
            new int[,]
            {
                {1, -2}, {1, -1}, {1, 0}, {1, 1}, {1, 2}, {1, 3}, {0, 2}, {0, 3} // Rotation 90 degrees
            },
            new int[,]
            {
                {-1, 0}, {-1, 1}, {1, 0}, {1, 1}, {0, 0}, {0, 1}, {1, 2}, {1, 3} // Rotation 180 degrees
            }
        },

        // Z block left (two rotations)
        new List<int[,]>
        {
            new int[,]
            {
                {1, -2}, {1, -1}, {0, 0}, {0, 1}, {1, 0}, {1, 1}, {0, 2}, {0, 3}
            },
            new int[,]
            {
                {0, 0}, {0, 1}, {1, 0}, {1, 1}, {0, -1}, {0, -2}, {-1, -1}, {-1, -2}
            }
        },

        // T block (four rotations)
        new List<int[,]>
        {
            new int[,]
            {
                {0, -2}, {0, -1}, {0, 0}, {0, 1}, {0, 2}, {0, 3}, {1, 0}, {1, 1}
            },
            new int[,]
            {
                {-1, 0}, {-1, 1}, {0, 0}, {0, 1}, {1, 0}, {1, 1}, {0, -1}, {0, -2}
            },
            new int[,]
            {
                {1,-2}, {1,-1}, {1,0}, {1,1}, {1, 2}, {1, 3}, {0, 0}, {0, 1}
            },
            new int[,]
            {
                {-1, 0}, {-1, 1}, {0, 0}, {0, 1}, {1, 0}, {1, 1}, {0, 2}, {0, 3}
            }
        },

        // Z block right (two rotations)
        new List<int[,]>
        {
            new int[,]
            {
                {0, -2}, {0, -1}, {0, 0}, {0, 1}, {1, 0}, {1, 1}, {1, 2}, {1, 3}
            },
            new int[,]
            {
                {0, 0}, {0, 1}, {1, 0}, {1, 1}, {0, 2}, {0, 3}, {-1 , 2}, {-1, 3}
            }
        }
    };
}
