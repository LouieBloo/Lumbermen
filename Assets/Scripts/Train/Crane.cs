using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crane : MonoBehaviour
{
    public float rotationSpeed = 5f;  // Degrees per second
    public Transform linkedTrain;
    public LogDropoff linkedLogDropoff;

    private StorageContainer hand = new StorageContainer();

    public bool active = false;
    public bool rotating = false;

    private Transform targetObject;

    private void Start()
    {
        hand.maxCapacity = 10;
        moveToTarget(linkedLogDropoff.transform);
        //StartCoroutine(RotateTowardsTarget(targetEulerAngle));
    }

    public void trainReady()
    {
        active = true;

        if (hand.currentCapacity != 0)
        {
            moveToTarget(linkedTrain);
        }
        
    }

    public void trainNotReady()
    {
        active = false;
    }

    void Update()
    {
        if(active) {
            if(rotating)
            {
                RotateTowardsTarget();
            }
            else
            {
                checkIfAnyLogs();
            }
        }
    }

    void checkIfAnyLogs()
    {
        if(!rotating && targetObject == linkedLogDropoff.transform)
        {
            List<StorageContainer.StorageItem> logs = linkedLogDropoff.grabItemsByName("Log");
            if (logs != null && logs.Count > 0)
            {
                if (hand.canAddItem(logs[0]))
                {
                    hand.addItem(linkedLogDropoff.removeItem(logs[0]));
                    //do something about sprites
                    moveToTarget(linkedTrain);
                }
            }
        }
    } 

    void moveToTarget(Transform target)
    {
        targetObject = target;
        rotating = true;
    }

    void doneRotating()
    {
        if(targetObject == linkedTrain)
        {
            Debug.Log("Done rotating to train");
            StorageContainer.StorageItem itemInHand = hand.removeItem(hand.grabItemsByName("Log")[0]);
            //market?
            // do something with sprite
            Player.Instance.modifyStat(Unit.StatTypes.Gold, 10);
            moveToTarget(linkedLogDropoff.transform);
        }
        else if(targetObject == linkedLogDropoff.transform)
        {
            Debug.Log("Done rotating to loggggg");
        }
    }

    void RotateTowardsTarget()
    {
        if (targetObject == null) return; // If no target is set, don't proceed

        // Determine the direction from this object to the target object
        Vector3 directionToTarget = targetObject.position - transform.position;

        // Ignore the Z-axis difference to calculate the rotation angle in the X-Y plane
        directionToTarget.z = 0;

        // Calculate the angle between the current direction (forward) and the target direction
        float angle = Vector3.SignedAngle(transform.up, directionToTarget, Vector3.forward);

        // Calculate the target rotation around the Z axis based on the calculated angle
        Quaternion targetRotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + angle);

        // Smoothly rotate towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        // Check if rotation is almost complete
        if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
        {
            transform.rotation = targetRotation; // Snap to exact target rotation
            rotating = false;  // Stop rotating

            doneRotating();
        }
    }
}
