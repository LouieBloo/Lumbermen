using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IModifier
{
    [SerializeField]
    public bool pickedUp = false;

    [SerializeField]
    private bool disableOnEquip = true;

    [SerializeField]
    private SpriteRenderer iconSpriteRenderer;

    [SerializeField]
    private Modification[] modifications;

    [SerializeField]
    private string name;
    public EquipmentHolder.SlotType slotType;

    protected Unit unit;

    public Modification[] getModifications()
    {
        return modifications;
    }

    public Sprite getSprite()
    {
        if(iconSpriteRenderer!= null)
            return iconSpriteRenderer.sprite;

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

    public virtual void equipped(Unit unit, CreatureAnimatorHelper animationHelper)
    {
        if (disableOnEquip)
        {
            gameObject.SetActive(false);
        }

        this.unit = unit;
        pickedUp = true;
        if(GetComponent<BoxCollider2D>() != null)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
    }
}
