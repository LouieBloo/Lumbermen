using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Train : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float accelerationTime = 2f;  // Time it takes to reach max speed
    public float decelerationDistance = 3f;  // Begin decelerating this distance away from the target

    private float currentSpeed = 0f;
    private float accelerationRate;
    private Vector3 targetPoint;
    public bool isMoving = false;
    private float velocityReference;

    private Vector3 trainStopPoint;
    private Vector3 trainEndPoint;

    private bool waitingAtStop = false;
    public float waitAtStopTime = 4f;
    private float waitingAtStopTimer = 0f;

    void Start()
    {
        transform.position = GameObject.FindGameObjectWithTag("TrainStart").transform.position;
        trainStopPoint = GameObject.FindGameObjectWithTag("TrainStop").transform.position;
        trainEndPoint = GameObject.FindGameObjectWithTag("TrainEnd").transform.position;

        SetTarget(trainStopPoint);
    }

    public void SetTarget(Vector3 newTarget)
    {
        targetPoint = newTarget;
        isMoving = true;
        accelerationRate = maxSpeed / accelerationTime;
    }

    private void Update()
    {
        /*if (Input.GetMouseButtonDown(0))
        {
            SetTarget(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }*/


        if (isMoving)
        {
            float distanceToTarget = targetPoint.x - transform.position.x;

            // Check if we should be accelerating or decelerating
            if (Mathf.Abs(distanceToTarget) > decelerationDistance || currentSpeed == 0)
            {
                // Accelerate
                currentSpeed = Mathf.SmoothDamp(currentSpeed, maxSpeed, ref velocityReference, accelerationTime);
            }
            else
            {
                // Decelerate based on remaining distance
                float requiredDeceleration = (currentSpeed * currentSpeed) / (2 * Mathf.Abs(distanceToTarget));
                currentSpeed -= requiredDeceleration * Time.deltaTime;
            }

            // Ensure we don't overshoot the target
            float potentialMove = currentSpeed * Time.deltaTime;
            if (Mathf.Abs(potentialMove) > Mathf.Abs(distanceToTarget))
            {
                currentSpeed = distanceToTarget / Time.deltaTime;
            }

            // Move the train
            transform.position += Vector3.left * currentSpeed * Time.deltaTime;

            // Check if train has passed or reached the target
            if (Mathf.Abs(distanceToTarget) <= 0.01f)
            {
                transform.position = new Vector3(targetPoint.x, transform.position.y, transform.position.z);
                currentSpeed = 0;
                isMoving = false;

                if(targetPoint == trainEndPoint) { Destroy(this.gameObject); }

                if(targetPoint == trainStopPoint) {
                    waitingAtStop = true;
                }
            }
        }else if(waitingAtStop)
        {
            waitingAtStopTimer += Time.deltaTime;
            if(waitingAtStopTimer >= waitAtStopTime)
            {
                waitingAtStop = false;
                SetTarget(trainEndPoint);
            }
        }
    }
}
