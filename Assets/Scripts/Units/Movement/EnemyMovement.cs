using System.Collections;
using System.Collections.Generic;
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
    public override void Start()
    {
        base.Start();

        StartCoroutine(MoveTowardsTarget());
    }

    protected override float getXDirection()
    {
        if (!target) { return 0; }
        Vector2 direction = (target.position - transform.position).normalized;
        return direction.x;
    }

    protected override float getYDirection()
    {
        if (!target) { return 0; }
        Vector2 direction = (target.position - transform.position).normalized;
        return direction.y;
    }

    IEnumerator MoveTowardsTarget()
    {
        yield return null;
        yield return null;
        yield return null;

        Vector2 me = transform.position;
        Vector2 target = player.position;
        var t = Task.Run(async () => await GridPathfinding.Instance.findPath(me, target));

        yield return new WaitUntil(() => t.IsCompleted);
        (int, int)[] path = t.Result;

        Debug.Log(path.Length);

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
