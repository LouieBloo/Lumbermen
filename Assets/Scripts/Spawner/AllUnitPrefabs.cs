using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUnitPrefabs : MonoBehaviour
{

    public static AllUnitPrefabs Instance { get; private set; }

    [System.Serializable] //This attribute makes the class show up in the Unity inspector
    public class PrefabHolder
    {
        public GameObject prefab;
    }

    [System.Serializable] //This attribute makes the class show up in the Unity inspector
    public class UnitHolder : PrefabHolder
    {
        public UnitName name;
    }

    [System.Serializable] //This attribute makes the class show up in the Unity inspector
    public class WeaponHolder : PrefabHolder
    {
        public WeaponName name;
    }

    [System.Serializable] //This attribute makes the class show up in the Unity inspector
    public class ItemHolder : PrefabHolder
    {
        public ItemName name;
    }

    public UnitHolder[] unitPrefabs;
    public WeaponHolder[] weaponPrefabs;
    public ItemHolder[] itemPrefabs;

    private Dictionary<UnitName, GameObject> allUnits = new Dictionary<UnitName, GameObject>();
    private Dictionary<WeaponName, GameObject> allWeapons = new Dictionary<WeaponName, GameObject>();
    private Dictionary<ItemName, GameObject> allItems = new Dictionary<ItemName, GameObject>();

    public enum UnitName {
        BrownBear,
        Raven,
    }

    public enum ItemName
    {
        BrownBoots,
        RedBoots,
    }

    public enum WeaponName
    {
        BearSlash
    }

    public GameObject sprout;

    private void Awake()
    {
        // Singleton setup
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }


        foreach(UnitHolder holder in unitPrefabs) {
            allUnits[holder.name] = holder.prefab;
        }

        foreach (WeaponHolder holder in weaponPrefabs)
        {
            allWeapons[holder.name] = holder.prefab;
        }

        foreach (ItemHolder holder in itemPrefabs)
        {
            allItems[holder.name] = holder.prefab;
        }

    }

    public GameObject getUnit(UnitName name)
    {
        return allUnits[name];
    }

    public GameObject getWeapon(WeaponName name)
    {
        return allWeapons[name];
    }

    public GameObject getItem(ItemName name)
    {
        return allItems[name];
    }


}
