using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static Unit;
using static UnityEditor.Progress;

public class EquipmentHolder : MonoBehaviour
{
    public AllUnitPrefabs.WeaponName startingWeapon;
    private Dictionary<SlotType, Item> equipment = new Dictionary<SlotType, Item>();

    private Unit unit;

    /*public class EquipmentSlot
    {
        public Item item;
        public SlotType slotType;
    }*/
    private void Awake()
    {
        unit = GetComponent<Unit>();
    }

    public enum SlotType
    {
        Helmet, Feet, Weapon, None
    }

    public void addItem(Item item)
    {
        if (equipment.TryGetValue(item.slotType, out Item targetItem))
        {
            //bring up ui so player can choose which to keep
            ChooseItemHandler.Instance.chooseItem(equipItem, targetItem, item);
        }
        else
        {
            equipItem(null, item, item);
        }
    }

    private void equipItem(Item outgoingItem, Item incomingItem, Item choiceItem)
    {
        if (outgoingItem != null)
        {
            if (outgoingItem == choiceItem)
            {
                Destroy(incomingItem.gameObject);
                return;  // If outgoing item is the choice item, we're done
            }

            // Unequip the outgoing item if it's not the choice item
            unit.deleteModifier(outgoingItem);
            Destroy(outgoingItem.gameObject);
        }

        // Equip the choice item
        equipment[choiceItem.slotType] = choiceItem;
        unit.addModifier(choiceItem);
        choiceItem.equipped(unit, unit.GetComponent<CreatureAnimatorHelper>());
    }


}
