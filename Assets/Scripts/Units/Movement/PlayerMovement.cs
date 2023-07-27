using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // You can adjust this speed in the Unity editor.

    public Transform bodyDirectionFlipper;
    public Animator animator;

    private Rigidbody2D rb;


    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component to apply forces for movement
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("Attack");
        }*/
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

        // If there's input from the player (the player is moving), rotate the player to face the direction of movement
        if (movement.sqrMagnitude > 0)
        {
            // Calculate the angle of the movement direction in degrees
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;

            // Rotate the player to face the direction of movement
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            animator.SetBool("IsWalking", true);

            if (moveX < 0)
            {
                if(bodyDirectionFlipper.localScale.x > 0)
                {
                    bodyDirectionFlipper.localScale = new Vector3(-bodyDirectionFlipper.localScale.x, bodyDirectionFlipper.localScale.y, bodyDirectionFlipper.localScale.z);
                }

                animator.SetBool("FacingEast", false);
            }
            else if (moveX > 0) // Flip it back if moving right
            {
                if (bodyDirectionFlipper.localScale.x < 0)
                {
                    bodyDirectionFlipper.localScale = new Vector3(-bodyDirectionFlipper.localScale.x, bodyDirectionFlipper.localScale.y, bodyDirectionFlipper.localScale.z);
                }

                animator.SetBool("FacingEast", true);
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
