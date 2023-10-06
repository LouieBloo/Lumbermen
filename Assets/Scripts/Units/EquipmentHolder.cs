using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static Unit;

public class EquipmentHolder : MonoBehaviour
{

    private Dictionary<SlotType, Item> equipment = new Dictionary<SlotType, Item>();

    /*public class EquipmentSlot
    {
        public Item item;
        public SlotType slotType;
    }*/

    public enum SlotType
    {
        Helmet, Feet, None
    }

    public void addItem(Item item)
    {
        if (equipment.TryGetValue(item.slotType, out Item targetItem))
        {
            //bring up ui so player can choose which to keep
        }
        else
        {
            equipment[item.slotType] = item;
            GetComponent<Unit>().addModifier(item);
        }
    }
    

}
