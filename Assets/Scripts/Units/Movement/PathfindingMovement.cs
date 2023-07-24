using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathfindingMovement : MonoBehaviour
{
    public GridPathfinding grid;
    public float speed = 0.5f;

    IEnumerator movingRoutine;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            

            if(movingRoutine != null)
            {
                StopCoroutine(movingRoutine);
            }
            movingRoutine = MoveTowardsTarget(worldPosition);
            StartCoroutine(movingRoutine);
        }
    }

    IEnumerator MoveTowardsTarget(Vector2 target)
    {
        Vector2 startingPosition = grid.worldPointToGridPoint(transform.position);
        Vector2 endPosition = grid.worldPointToGridPoint(target);

        Debug.Log("start: " + startingPosition);
        Debug.Log("end: " + endPosition);

        (int, int)[] path = grid.findPath(startingPosition, endPosition);

        Debug.Log(path.Length);

        if(path != null && path.Length > 0) {
            int currentPathIndex = 0;
            while(currentPathIndex < path.Length)
            {
                Vector2 targetPos = new Vector2(path[currentPathIndex].Item1, path[currentPathIndex].Item2);
                // Continue the loop until the object is very close to the target position
                while (Vector2.Distance(transform.position, targetPos) > 0.01f)
                {
                    // Calculate the direction vector to the target position
                    Vector2 direction = targetPos - (Vector2)transform.position;

                    // Move the object towards the target position
                    transform.position = Vector2.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);

                    // Calculate the angle to the target position
                    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

                    // Rotate the object to face the target position
                    transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                    // Wait for the next frame
                    yield return null;
                }

                currentPathIndex++;
            }

            
            
        }

        movingRoutine = null;
    }
}
