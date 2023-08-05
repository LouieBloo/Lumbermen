using System.Collections;
using System.Collections.Generic;
using MidniteOilSoftware;
using UnityEngine;

public class Tree : MonoBehaviour, IDier, IDespawnedPoolObject, IRetrievedPoolObject
{
    public GameObject experiencePrefab;
    public GameObject logPrefab;

    void Start()
    {
        //register this tree on the grid
        //UnitGrid.Instance.fillCell(transform.position, this.gameObject, UnitGrid.UnitTypes.Tree, true);
        //RetrievedFromPool(null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setup()
    {
        UnitGrid.Instance.fillCell(transform.position, this.gameObject, UnitGrid.UnitTypes.Tree);
    }

    public void die()
    {
        UnitGrid.Instance.fillCell(transform.position, null, UnitGrid.UnitTypes.Empty);
        //Instantiate(experiencePrefab, transform.position, Quaternion.identity);
        Instantiate(logPrefab, transform.position, Quaternion.identity);
        ObjectPoolManager.DespawnGameObject(this.gameObject);
    }

    public void ReturnedToPool()
    {

    }

    public void RetrievedFromPool(GameObject prefab)
    {
    }
}
