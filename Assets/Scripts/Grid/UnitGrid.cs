using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class UnitGrid : MonoBehaviour
{
    public int width;
    public int height;
    public int xOffset;
    public int yOffset;
    public static UnitGrid Instance { get; private set; }
    public Tilemap pathfindingTilemap;

    private System.Random rand = new System.Random();

    public enum UnitTypes
    {
        Empty, Tree, Sprout, Stump, Blocker
    }

    public class GridCell
    {
        public UnitTypes unitType;
        public GameObject gameObject;
    }

    private GridCell[,] grid;

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

        grid = new GridCell[width, height];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                grid[x, y] = new GridCell();
            }
        }
    }

    private void Start()
    {
        //scan world for defaults
        GameObject[] trees = GameObject.FindGameObjectsWithTag("Tree");

        foreach (GameObject tree in trees)
        {
            fillCell(tree.transform.position, tree, UnitTypes.Tree);
        }

        BoundsInt bounds = pathfindingTilemap.cellBounds;
        TileBase[] allTiles = pathfindingTilemap.GetTilesBlock(bounds);


        for (int n = pathfindingTilemap.cellBounds.xMin; n < pathfindingTilemap.cellBounds.xMax; n++)
        {
            for (int p = pathfindingTilemap.cellBounds.yMin; p < pathfindingTilemap.cellBounds.yMax; p++)
            {
                Vector3Int localPlace = (new Vector3Int(n, p, (int)pathfindingTilemap.transform.position.y));
                Vector3 place = pathfindingTilemap.CellToWorld(localPlace);
                if (pathfindingTilemap.HasTile(localPlace))
                {
                    //Tile at "place"
                    fillCell(new Vector3(place.x + 0.5f, place.y + 0.5f), null, UnitTypes.Blocker);
                }
                else
                {
                    //No tile at "place"
                }
            }
        }

        /*for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                TileBase tile = allTiles[x + y * bounds.size.x];
                if (tile != null)
                {
                    Debug.Log("x:" + x + " y:" + y + " tile:" + tile.name);
                    Debug.Log(pathfindingTilemap.GetCellCenterWorld(new Vector3Int(x, y)));
                    //Debug.Log(pathfindingTilemap.CellToWorld(new Vector3Int(x, y)));
                    fillCell(pathfindingTilemap.GetCellCenterWorld(new Vector3Int(x, y)), null, UnitTypes.Blocker);
                }
                
            }
        }*/
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 gridPoint = worldPointToGridPoint(worldPosition);
            Debug.Log(grid[(int)gridPoint.x, (int)gridPoint.y].unitType);
        }
    }

    public bool isCellEmpty(Vector2 position)
    {
        Vector2 gridPoint = worldPointToGridPoint(position);
        return grid[(int)gridPoint.x, (int)gridPoint.y].unitType == UnitTypes.Empty;
    }

    public void fillCell(Vector2 position, GameObject gameObject, UnitTypes unitType)
    {
        Vector2 gridPoint = worldPointToGridPoint(position);

        /*if(deleteCell && grid[(int)gridPoint.x, (int)gridPoint.y].gameObject != null)
        {
            IDier dier = grid[(int)gridPoint.x, (int)gridPoint.y].gameObject.GetComponent<IDier>();
            if(dier != null)
            {
                dier.die();
            }
            else
            {
                Destroy(grid[(int)gridPoint.x, (int)gridPoint.y].gameObject);
            }
        }*/

        grid[(int)gridPoint.x, (int)gridPoint.y].unitType = unitType;
        grid[(int)gridPoint.x, (int)gridPoint.y].gameObject = gameObject;

        if(unitType == UnitTypes.Empty)
        {
            GridPathfinding.Instance.emptyCell(position);
        }
        else
        {
            GridPathfinding.Instance.fillCell(position);
        }
    }

    public Vector2 worldPointToGridPoint(Vector2 worldPoint)
    {
        return new Vector2(Mathf.RoundToInt(worldPoint.x) + xOffset, Mathf.RoundToInt(worldPoint.y) + yOffset);
    }

    public bool cellOkToFillWithSprout(GridCell cell)
    {
        return cell.unitType == UnitTypes.Empty || cell.unitType == UnitTypes.Stump;
    }

    public (int, int) findNearestCellForSprout(Vector2 position)
    {
        Vector2 gridPos = worldPointToGridPoint(position);
        int maxDistance = Mathf.Max(grid.GetLength(0), grid.GetLength(1));

        for (int currentDistance = 1; currentDistance <= maxDistance; currentDistance++)
        {
            List<(int, int)> validCells = new List<(int, int)>();
            for (int x = 0; x < grid.GetLength(0); x++)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    float distance = Mathf.Sqrt(Mathf.Pow(x - gridPos.x, 2) + Mathf.Pow(y - gridPos.y, 2));

                    if (distance <= currentDistance && cellOkToFillWithSprout(grid[x, y]))
                    {
                        validCells.Add((x, y));
                    }
                }
            }

            if (validCells.Count > 0)
            {
                // Choose a random cell from the list and return it
                (int, int) chosenCell = validCells[rand.Next(validCells.Count)];
                chosenCell.Item1 -= xOffset;
                chosenCell.Item2 -= yOffset;
                return chosenCell;
            }
        }

        // If there are no non-zero cells, return an invalid index
        return (-99999, -99999);
    }

}
