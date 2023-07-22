using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // You can adjust this speed in the Unity editor.

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component to apply forces for movement
        rb = GetComponent<Rigidbody2D>();
    }

    // FixedUpdate is called once per frame, but at a fixed interval - better for physics calculations
    void FixedUpdate()
    {
        // Horizontal movement (A/D keys)
        float moveX = Input.GetAxis("Horizontal");

        // Vertical movement (W/S keys)
        float moveY = Input.GetAxis("Vertical");

        // Apply the force to the Rigidbody2D
        rb.velocity = new Vector2(moveX * speed, moveY * speed);
    }
}
