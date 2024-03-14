using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentHolder : MonoBehaviour
{
    public AllUnitPrefabs.WeaponName startingWeapon;
    private Dictionary<SlotType, Item> equipment = new Dictionary<SlotType, Item>();

    private List<Powerup> powerups = new List<Powerup>();

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
        Helmet, Feet, Weapon, Powerup, None
    }

    public void addItem(Item item)
    {
        if (item.slotType != SlotType.Powerup && equipment.TryGetValue(item.slotType, out Item targetItem))
        {
            //if we already have a duplicate item equipped, dont allow it 
            if (targetItem.itemName == item.itemName)
            {
                Destroy(item.gameObject);
                return;
            }
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

            removeItem(outgoingItem);
        }

        // Equip the choice item
        if(choiceItem.slotType == SlotType.Powerup)
        {
            powerups.Add(choiceItem.GetComponent<Powerup>());
        }
        else
        {
            equipment[choiceItem.slotType] = choiceItem;
        }
        
        unit.addModifier(choiceItem);
        choiceItem.equipped(unit, unit.GetComponent<CreatureAnimatorHelper>());
    }

    public void removeItem(Item itemToRemove)
    {
        if (itemToRemove.slotType == SlotType.Powerup)
        {
            powerups.Remove(itemToRemove.GetComponent<Powerup>());
        }

        // Unequip the outgoing item if it's not the choice item
        unit.deleteModifier(itemToRemove);
        Destroy(itemToRemove.gameObject);
    }
}
