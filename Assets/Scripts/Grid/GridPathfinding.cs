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

    public static GridPathfinding Instance { get; private set; }

    private System.Random rand = new System.Random();

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        grid = new float[width,height];
        //grid[40, 37] = -1;
    }

    public (int, int) FindNearestNonZeroCell( Vector2 position)
    {
        Vector2 gridPos = worldPointToGridPoint(position);
        int maxDistance = Mathf.Max(grid.GetLength(0), grid.GetLength(1));
        
        for (int currentDistance = 1; currentDistance <= maxDistance; currentDistance++)
        {
            List<(int, int)> validCells = new List<(int, int)>();
            for (int x = 0; x < grid.GetLength(1); x++)
            {
                for (int y = 0; y < grid.GetLength(0); y++)
                {
                    float distance = Mathf.Sqrt(Mathf.Pow(x - gridPos.x, 2) + Mathf.Pow(y - gridPos.y, 2));

                    if (distance <= currentDistance && grid[y,x] != -1)
                    {
                        validCells.Add((x, y));
                    }
                }
            }

            if (validCells.Count > 0)
            {
                // Choose a random cell from the list and return it
                (int,int) chosenCell = validCells[rand.Next(validCells.Count)];
                chosenCell.Item1 -= xOffset;
                chosenCell.Item2 -= yOffset;
                return chosenCell;
            }
        }

        // If there are no non-zero cells, return an invalid index
        return (-99999, -99999);
    }

    public bool isCellOpen(Vector2 position)
    {
        Vector2 gridPoint = worldPointToGridPoint(position);
        if (grid[(int)gridPoint.y,(int)gridPoint.x] == 0)
        {
            return true;
        }

        return false;
    }

    public bool fillCell(Vector2 position)
    {
        if (isCellOpen(position))
        {
            Vector2 gridPoint = worldPointToGridPoint(position);
            grid[(int)gridPoint.y, (int)gridPoint.x] = -1;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void emptyCell(Vector2 position)
    {
        Vector2 gridPoint = worldPointToGridPoint(position);
        grid[(int)gridPoint.y, (int)gridPoint.x] = 0;
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
