using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicUnitMovement : MonoBehaviour
{
    public Transform bodyDirectionFlipper;
    public Animator animator;

    private Rigidbody2D rb;
    public Unit unit;
    protected Transform player;
    
    public enum MovementType { Ground, Flying}
    public bool debugging = false;

    public Joystick joystick;

    public enum Direction { North, South, East, West, NotMoving }
    public Direction currentDirection = Direction.NotMoving;

    // Start is called before the first frame update
    public virtual void Start()
    {
        // Get the Rigidbody2D component to apply forces for movement
        if(rb == null) { rb = GetComponent<Rigidbody2D>();}
        if(unit == null) { unit = GetComponent<Unit>(); }
        
        player = Player.Instance.transform;
    }

    protected virtual float getXDirection()
    {
        return GameSettings.Instance.getXDirection();
    }

    protected virtual float getYDirection()
    {
        return GameSettings.Instance.getYDirection();
    }

    protected virtual void processNextTarget()
    {

    }

    public virtual void setup(Unit unit, Animator animator)
    {
        this.unit = unit;
        this.animator = animator;
    }

    // FixedUpdate is called once per frame, but at a fixed interval - better for physics calculations
    void FixedUpdate()
    {
        if(unit == null) { return; }
        // Horizontal movement (A/D keys)
        float moveX = getXDirection();

        // Vertical movement (W/S keys)
        float moveY = getYDirection();

        // Create a movement vector
        Vector2 movement = new Vector2(moveX, moveY);


        // Apply the velocity to the Rigidbody2D
        rb.velocity = movement * unit.movementSpeed;

        // Update the direction based on movement
        if (moveX > 0)
        {
            currentDirection = Direction.East;
        }
        else if (moveX < 0)
        {
            currentDirection = Direction.West;
        }
        else if (moveY > 0)
        {
            currentDirection = Direction.North;
        }
        else if (moveY < 0)
        {
            currentDirection = Direction.South;
        }
        else
        {
            //currentDirection = Direction.NotMoving;
        }

        // If there's input from the player (the player is moving), rotate the player to face the direction of movement
        if (movement.sqrMagnitude > 0)
        {
            // Calculate the angle of the movement direction in degrees
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

            // Rotate the player to face the direction of movement
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            animator.SetBool("IsWalking", true);

            // Handle sprite flipping based on current direction
            if (currentDirection == Direction.East)
            {
                if (bodyDirectionFlipper.localScale.x < 0)
                {
                    bodyDirectionFlipper.localScale = new Vector3(-bodyDirectionFlipper.localScale.x, bodyDirectionFlipper.localScale.y, bodyDirectionFlipper.localScale.z);
                }
            }
            else if (currentDirection == Direction.West)
            {
                if (bodyDirectionFlipper.localScale.x > 0)
                {
                    bodyDirectionFlipper.localScale = new Vector3(-bodyDirectionFlipper.localScale.x, bodyDirectionFlipper.localScale.y, bodyDirectionFlipper.localScale.z);
                }
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }
}
