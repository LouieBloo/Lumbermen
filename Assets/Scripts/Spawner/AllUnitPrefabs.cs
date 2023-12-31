using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUnitPrefabs : MonoBehaviour
{

    public static AllUnitPrefabs Instance { get; private set; }

    [System.Serializable] 
    public class PrefabHolder
    {
        public GameObject prefab;
    }

    [System.Serializable] 
    public class UnitHolder : PrefabHolder
    {
        public UnitName name;
    }

    [System.Serializable] 
    public class WeaponHolder : PrefabHolder
    {
        public WeaponName name;
    }

    [System.Serializable] 
    public class ItemHolder : PrefabHolder
    {
        public ItemName name;
    }

    [System.Serializable] 
    public class ProjectileHolder : PrefabHolder
    {
        public ProjectileName name;
    }

    public UnitHolder[] unitPrefabs;
    public WeaponHolder[] weaponPrefabs;
    public ItemHolder[] itemPrefabs;
    public ProjectileHolder[] projectilePrefabs;

    private Dictionary<UnitName, GameObject> allUnits = new Dictionary<UnitName, GameObject>();
    private Dictionary<WeaponName, GameObject> allWeapons = new Dictionary<WeaponName, GameObject>();
    private Dictionary<ItemName, GameObject> allItems = new Dictionary<ItemName, GameObject>();
    private Dictionary<ProjectileName, GameObject> allProjectiles = new Dictionary<ProjectileName, GameObject>();

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
        BearSlash,
        RavenSlash,
        BasicAxe,
        BasicThrowingAxe
    }

    public enum ProjectileName
    {
        BasicAxe,
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

        foreach (ProjectileHolder holder in projectilePrefabs)
        {
            allProjectiles[holder.name] = holder.prefab;
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

    public GameObject getProjectile(ProjectileName name)
    {
        return allProjectiles[name];
    }

}
