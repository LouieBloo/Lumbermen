using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour
{
    public Transform player;
    public float deadZoneSize = 0.3f; // Size of the dead zone as a fraction of the camera's height/width
    private Camera cam;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position = new Vector3(player.position.x , player.position.y, -10);
        Vector2 viewPos = cam.WorldToViewportPoint(player.position);

        Vector3 deltaPosition = Vector3.zero;

        // Check if the player is outside of the dead zone on the X axis
        if (viewPos.x > 1.0f - deadZoneSize)
        {
            deltaPosition.x = player.position.x - cam.ViewportToWorldPoint(new Vector3(1.0f - deadZoneSize, 0)).x;
        }
        else if (viewPos.x < deadZoneSize)
        {
            deltaPosition.x = player.position.x - cam.ViewportToWorldPoint(new Vector3(deadZoneSize, 0)).x;
        }

        // Check if the player is outside of the dead zone on the Y axis
        if (viewPos.y > 1.0f - deadZoneSize)
        {
            deltaPosition.y = player.position.y - cam.ViewportToWorldPoint(new Vector3(0, 1.0f - deadZoneSize)).y;
        }
        else if (viewPos.y < deadZoneSize)
        {
            deltaPosition.y = player.position.y - cam.ViewportToWorldPoint(new Vector3(0, deadZoneSize)).y;
        }

        // Move the camera by the computed deltaPosition
        transform.position += deltaPosition;
    }
}
