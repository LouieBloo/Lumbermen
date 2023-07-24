using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using AStar;
using UnityEngine;

public class GridPathfinding : MonoBehaviour
{
    public int width;
    public int height;

    public int xOffset;
    public int yOffset;

    public bool drawGrid = true;

    private float[,] grid;

    // Start is called before the first frame update
    void Start()
    {
        grid = new float[width,height];
        //grid[40, 37] = -1;
    }

    public (int,int)[] findPath(Vector2 startingPosition, Vector2 goalPosition)
    {
        (int, int)[] path = AStarPathfinding.GeneratePathSync((int)startingPosition.x, (int)startingPosition.y, (int)goalPosition.x, (int)goalPosition.y, grid);
        if(path != null && path.Length > 0) {
            for (int i = 0; i < path.Length; i++)
            {
                // Add the amount to each item of the tuples
                path[i] = (path[i].Item1 - xOffset, path[i].Item2 - yOffset);
            }
        }

        return path;
    }

    public Vector2 worldPointToGridPoint(Vector2 worldPoint)
    {
        return new Vector2(Mathf.RoundToInt(worldPoint.x) + xOffset, Mathf.RoundToInt(worldPoint.y) + yOffset);
    }

    void OnDrawGizmos()
    {
        if (!drawGrid) { return; }
        // draw a cube for each cell in the grid
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // calculate the position of the cell
                Vector3 position = new Vector3(x * 1, y * 1, 0) + transform.position;

                // draw the cell as a small cube
                Gizmos.DrawWireCube(position, Vector3.one * 1);
            }
        }
    }
}
