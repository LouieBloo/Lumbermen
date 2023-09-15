using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TrainCar : MonoBehaviour
{
    public Train train;

    void Start()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*HealthHaver healthHaver = collision.gameObject.GetComponent<HealthHaver>();
        if (healthHaver != null && train.isMoving)
        {
            healthHaver.takeDamage(99999, null);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HealthHaver healthHaver = collision.gameObject.GetComponent<HealthHaver>();
        if (healthHaver != null && train.isMoving)
        {
            healthHaver.takeDamage(99999, null);
        }
    }
}
