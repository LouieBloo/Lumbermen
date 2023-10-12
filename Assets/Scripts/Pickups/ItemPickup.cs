using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Pickup
{
    public override void pickedUp(GameObject pickerUpper)
    {
        pickerUpper.GetComponent<Unit>().equipmentHolder.addItem(GetComponent<Item>());
    }
}

