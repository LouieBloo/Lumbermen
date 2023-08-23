using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TrainCar : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthHaver healthHaver = collision.gameObject.GetComponent<HealthHaver>();
        if (healthHaver != null)
        {
            healthHaver.takeDamage(99999, null);
        }
    }
}
