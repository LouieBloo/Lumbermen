using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridOccupier : MonoBehaviour
{
    private void OnDestroy()
    {
        GridPathfinding.Instance.emptyCell(transform.position);
    }
}
