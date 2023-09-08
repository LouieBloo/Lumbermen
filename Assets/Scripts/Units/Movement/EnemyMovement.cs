using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using demo2;
using UnityEngine;

public class EnemyMovement : BasicUnitMovement
{
    //public async Task findPath()
    //{
    //    (int,int)[] foundPath = await GridPathfinding.Instance.findPath(transform.position, player.position);
    //    Debug.Log(foundPath.Length);
    //}
    public float distanceToConsiderAtPoint = 0.1f;
    private (int, int)[] currentPath;
    private int currentPathIndex = -1;
    private Vector3 currentPathPosition;

    public override void Start()
    {
        base.Start();

        StartCoroutine(MoveTowardsTarget());
    }

    private void Update()
    {
        if(currentPath == null) { return; }

        //check if we are close enough to the current target path point
        if(Vector2.Distance(transform.position, currentPathPosition) <= distanceToConsiderAtPoint){
            processNextTarget();
        }
    }


    protected override float getXDirection()
    {
        if (currentPath == null) { return 0; }
        Vector2 direction = (currentPathPosition - transform.position).normalized;
        return direction.x;
    }

    protected override float getYDirection()
    {
        if (currentPath == null) { return 0; }
        Vector2 direction = (currentPathPosition - transform.position).normalized;
        return direction.y;
    }

    protected override void processNextTarget()
    {
        currentPathIndex++;
        if(currentPath != null && currentPathIndex < currentPath.Length)
        {
            
            currentPathPosition = new Vector3(currentPath[currentPathIndex].Item1, currentPath[currentPathIndex].Item2, 0);
        }
        else
        {
            //we are at the end
            currentPathIndex = 0;
            currentPath = null;
        }
    }

    IEnumerator MoveTowardsTarget()
    {
        yield return null;
        yield return null;
        yield return null;

        float timer = 0;
        float findPathDelay = 1.5f;
        bool waitingForPath = false;
        
        while (true)
        {
            if(!waitingForPath && Time.timeScale > 0)
            {
                timer += Time.deltaTime;
                if(timer >= findPathDelay)
                {
                    timer = 0;
                    Vector2 me = transform.position;
                    Vector2 target = player.position;
                    waitingForPath = true;
                    var t = Task.Run(async () => await GridPathfinding.Instance.findPath(me, target));
                    yield return new WaitUntil(() => t.IsCompleted);
                    waitingForPath = false;
                    currentPath = t.Result;
                    currentPathIndex = -1;//ghetto but works

                    /*foreach(var item in currentPath)
                    {
                        Debug.Log(item.Item1 + " " + item.Item2);
                    }*/

                    processNextTarget();
                }
            }

            yield return null;
        }

        /*if (path != null && path.Length > 0)
        {
            int currentPathIndex = 0;
            while (currentPathIndex < path.Length)
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

        movingRoutine = null;*/
    }

}
