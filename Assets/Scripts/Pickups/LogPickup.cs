using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogPickup : Pickup
{
    public override void pickedUp(GameObject pickerUpper)
    {
        Backpack targetBackpack = pickerUpper.GetComponent<Backpack>();
        if (targetBackpack != null && targetBackpack.addItem(new Backpack.BackPackItem(capacitySpace, "Log")))
        {
            Destroy(this.gameObject);
        }
    }
}
