using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDier
{
    private Unit unit;

    public Transform directionFlipper;
    public GameObject unitPrefab;

    public EnemyMovement movementScript;
    public MeleeAttacker attacker;

    void Start()
    {
        GameObject spawnedObject = GameObject.Instantiate(unitPrefab,directionFlipper);

        SpawnedEnemy newEnemy = spawnedObject.GetComponent<SpawnedEnemy>();
        unit = newEnemy.unit;
        movementScript.setup(newEnemy.unit, newEnemy.animator);
        /*movementScript.animator = newEnemy.animator;
        movementScript.unit = newEnemy.unit;*/

        newEnemy.healthHaver.dier = this;

        attacker.setup(newEnemy);

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
