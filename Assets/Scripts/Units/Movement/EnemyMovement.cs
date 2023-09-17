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
    }

    public override void setup(Unit unit, Animator animator)
    {
        base.setup(unit, animator);

        if(unit.movementType == MovementType.Ground)
        {
            StartCoroutine(MoveTowardsTarget());
        }
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
        if (unit.movementType == MovementType.Flying) {
            return (player.position - transform.position).normalized.x;
        }
        if (currentPath == null) { return 0; }
        Vector2 direction = (currentPathPosition - transform.position).normalized;
        return direction.x;
    }

    protected override float getYDirection()
    {
        if (unit.movementType == MovementType.Flying)
        {
            return (player.position - transform.position).normalized.y;
        }
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
            //if we are already considered at the target we should just go to the next point on path. That way we dont get stuck in a loop
            if (currentPathIndex == 0 && currentPath.Length > 1 && GridPathfinding.Instance.worldPointToGridPoint(transform.position) == GridPathfinding.Instance.worldPointToGridPoint(currentPathPosition))
            {
                processNextTarget();
            }
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
        float findPathDelay = 0.5f;
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
    }
}
