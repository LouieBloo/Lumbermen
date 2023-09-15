using UnityEngine;

public class AxeMoving : MonoBehaviour
{
    public Transform player;
    public float rotationSpeed = 5f;
    public float radius = 2f;
    public SpriteRenderer spriteRendererToTurnOff;

    public bool turnOffWhenNotMoving = false;
    private bool isSpriteTurnedOff = false;

    private Vector3 previousPlayerPosition;
    private float angle = 0f;

    private void Start()
    {
        if (player != null)
        {
            previousPlayerPosition = player.position;
        }
        else
        {
            Debug.LogError("Player Transform is not set on AxeController.");
        }
    }

    protected virtual float getXDirection()
    {
        return Input.GetAxis("Horizontal");
    }

    protected virtual float getYDirection()
    {
        return Input.GetAxis("Vertical");
    }

    private void Update()
    {
        // Get the horizontal and vertical input axes
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        if (turnOffWhenNotMoving)
        {
            if (horizontal == 0f && vertical == 0f)
            {
                if (!isSpriteTurnedOff)
                {
                    spriteRendererToTurnOff.enabled = false;
                    isSpriteTurnedOff = true;
                }
            }
            else if (isSpriteTurnedOff)
            {
                spriteRendererToTurnOff.enabled = true;
                isSpriteTurnedOff = false;
            }
        }

        Vector3 directionOfMovement = new Vector3(horizontal, vertical, 0);
        if (directionOfMovement.magnitude > 0.01f) // threshold to prevent unnecessary updates
        {
            // Make the axe point towards the direction of movement
            transform.up = directionOfMovement.normalized;

            // Calculate the angle based on the direction of movement
            float angle = Mathf.Atan2(directionOfMovement.y, directionOfMovement.x);

            // Calculate the position of the axe on the circle based on the angle
            Vector3 offset = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            transform.position = player.position + offset;
        }
    }
}
