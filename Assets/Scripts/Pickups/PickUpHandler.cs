using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpHandler : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision != null)
        {
            Pickup pickup= collision.GetComponent<Pickup>();
            if(pickup!= null )
            {
                pickup.pickedUp(this.gameObject);
            }
        }
    }
}
