using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TreeSpawner : MonoBehaviour
{
    public float spawnSpeed = 1f;
    public float totalSpawnTime = 30f;
    private float spawnTimer = 0;
    private float totalSpawnTimer = 0;

    public string treeToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        totalSpawnTimer += Time.deltaTime;

        if(spawnTimer >= spawnSpeed)
        {
            spawnTree();
        }
        if(totalSpawnTimer>= totalSpawnTime)
        {
            Destroy(this);
        }
    }

    void spawnTree()
    {
        (int,int) position = UnitGrid.Instance.findNearestCellForSprout(new Vector2(0,0));
        if(position.Item1 == -99999 && position.Item2 == -99999)
        {
            Debug.Log("Tree spawn couldnt find open pos!");
        }
        else
        {
            //Debug.Log(position);
            GameObject newSprout = Instantiate(AllUnitPrefabs.Instance.sprout, new Vector2(position.Item1, position.Item2), Quaternion.identity);
            UnitGrid.Instance.fillCell(new Vector2(position.Item1,position.Item2), newSprout,UnitGrid.UnitTypes.Sprout,true);
        }

        spawnTimer= 0;
    }
}
