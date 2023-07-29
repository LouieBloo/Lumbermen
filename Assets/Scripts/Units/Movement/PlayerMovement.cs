using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // You can adjust this speed in the Unity editor.

    public Transform bodyDirectionFlipper;
    public Animator animator;

    private Rigidbody2D rb;

    public enum Direction { North, South, East, West, NotMoving }
    public Direction currentDirection = Direction.NotMoving;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component to apply forces for movement
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            GridPathfinding.Instance.fillCell(worldPosition);
        }
    }

    // FixedUpdate is called once per frame, but at a fixed interval - better for physics calculations
    void FixedUpdate()
    {
        // Horizontal movement (A/D keys)
        float moveX = Input.GetAxis("Horizontal");

        // Vertical movement (W/S keys)
        float moveY = Input.GetAxis("Vertical");

        // Create a movement vector
        Vector2 movement = new Vector2(moveX, moveY);

        // Apply the velocity to the Rigidbody2D
        rb.velocity = movement * speed;

        // Update the direction based on movement
        if (moveY > 0)
        {
            currentDirection = Direction.North;
        }
        else if (moveY < 0)
        {
            currentDirection = Direction.South;
        }
        else if (moveX > 0)
        {
            currentDirection = Direction.East;
        }
        else if (moveX < 0)
        {
            currentDirection = Direction.West;
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

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
    }
}
