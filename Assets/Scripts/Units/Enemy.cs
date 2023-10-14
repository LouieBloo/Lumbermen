using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDier
{
    private Unit unit;

    public Transform directionFlipper;
    public AllUnitPrefabs.UnitName unitName;

    public EnemyMovement movementScript;

    void Start()
    {
        GameObject spawnedObject = GameObject.Instantiate(AllUnitPrefabs.Instance.getUnit(unitName),directionFlipper);

        SpawnedEnemy newEnemy = spawnedObject.GetComponent<SpawnedEnemy>();
        unit = newEnemy.unit;
        movementScript.setup(newEnemy.unit, newEnemy.animator);
        /*movementScript.animator = newEnemy.animator;
        movementScript.unit = newEnemy.unit;*/

        newEnemy.healthHaver.dier = this;

        //spawn the weapon
        //GameObject spawnedWeapon = GameObject.Instantiate(AllUnitPrefabs.Instance.getWeapon(unit.weaponToSpawn), unit.weaponLocation.transform);

        GameObject spawnedWeapon = Instantiate(AllUnitPrefabs.Instance.getWeapon(unit.equipmentHolder.startingWeapon));
        // Set the weapon's parent while preserving its world position, rotation, and scale
        spawnedWeapon.transform.SetParent(unit.weaponLocation.transform,true);
        spawnedWeapon.transform.localPosition = Vector3.zero;
        unit.equipmentHolder.addItem(spawnedWeapon.GetComponent<Item>());


        spawnedWeapon.GetComponent<Weapon>().setup(newEnemy.unit, newEnemy.animationHelper);

        GetComponent<SpriteRenderer>().enabled = false;
    }

    void Update()
    {
        
    }

    public void die(GameObject source)
    {
        Destroy(this.gameObject);
    }
}
