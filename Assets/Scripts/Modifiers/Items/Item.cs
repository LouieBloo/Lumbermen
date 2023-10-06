using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, IModifier
{
    [SerializeField]
    private Modification[] modifications;

    public EquipmentHolder.SlotType slotType;

    // Start is called before the first frame update
    void Start()
    {
        
    }


    public Modification[] getModifications()
    {
        return modifications;
    }
}
