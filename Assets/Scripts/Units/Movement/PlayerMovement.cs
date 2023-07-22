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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<Animator>().SetTrigger("Attack");
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
