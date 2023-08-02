using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour, IDier
{
    public GameObject experiencePrefab;
    // Start is called before the first frame update
    void Start()
    {
        //register this tree on the grid
        UnitGrid.Instance.fillCell(transform.position, this.gameObject, UnitGrid.UnitTypes.Tree, true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void die()
    {
        UnitGrid.Instance.fillCell(transform.position, null, UnitGrid.UnitTypes.Empty, false);
        Instantiate(experiencePrefab, transform.position, Quaternion.identity);
    }
}
