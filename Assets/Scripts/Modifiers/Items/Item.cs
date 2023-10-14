using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IModifier
{
    [SerializeField]
    private bool disableOnEquip = true;

    [SerializeField]
    private Modification[] modifications;

    [SerializeField]
    private string name;
    public EquipmentHolder.SlotType slotType;

    public Modification[] getModifications()
    {
        return modifications;
    }

    public Sprite getSprite()
    {
        return GetComponent<SpriteRenderer>().sprite;
    }

    public string getName()
    {
        return name;
    }

    public string getDescription()
    {
        string description = "";
        foreach(Modification mod in modifications)
        {
            description += mod.statType + " by " + mod.amount + "\n";
        }

        return description;
    }

    public void equipped()
    {
        if (disableOnEquip)
        {
            gameObject.SetActive(false);
        }
    }
}
