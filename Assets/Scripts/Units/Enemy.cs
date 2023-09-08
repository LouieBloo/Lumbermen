using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDier
{
    private Unit unit;

    public Transform directionFlipper;
    public GameObject unitPrefab;

    public EnemyMovement movementScript;
    public HealthHaver healthHaver;

    void Start()
    {
        GameObject spawnedObject = GameObject.Instantiate(unitPrefab,directionFlipper);

        SpawnedEnemy newEnemy = spawnedObject.GetComponent<SpawnedEnemy>();
        movementScript.animator = newEnemy.animator;
        movementScript.unit = newEnemy.unit;

        healthHaver.setup(newEnemy.unit);
    }

    void Update()
    {
        
    }

    public void die(GameObject source)
    {
        Destroy(this.gameObject);
    }
}
