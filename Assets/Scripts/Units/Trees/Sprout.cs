using System.Collections;
using System.Collections.Generic;
using MidniteOilSoftware;
using UnityEngine;

public class Sprout : MonoBehaviour, IDespawnedPoolObject, IRetrievedPoolObject, IDier
{
    public float timeBetweenSprites = 5f;
    private float totalRespawnTimer = 0f;
    public GameObject treeToSpawn;
    public GameObject[] sprites;
    private int currentSpriteIndex = 0;

    bool alive = true;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!alive) { return; }

        totalRespawnTimer += Time.deltaTime;

        if(totalRespawnTimer >= timeBetweenSprites)
        {
            changeSprite();
            totalRespawnTimer = 0f;
        }
    }

    void changeSprite()
    {
        currentSpriteIndex += 1;
        if(currentSpriteIndex >= sprites.Length)
        {
            //Instantiate(treeToSpawn,transform.position,Quaternion.identity);
            die();
            //Destroy(this.gameObject);
        }
        else
        {
            sprites[currentSpriteIndex-1].gameObject.SetActive(false);
            sprites[currentSpriteIndex].gameObject.SetActive(true);
        }
    }

    public void ReturnedToPool()
    {
        //throw new System.NotImplementedException();
        alive = false;
    }

    public void RetrievedFromPool(GameObject prefab)
    {
        //throw new System.NotImplementedException();
        alive = true;

        totalRespawnTimer = 0;
        //sprites[currentSpriteIndex].gameObject.SetActive(false);
        currentSpriteIndex = 0;
        sprites[currentSpriteIndex].gameObject.SetActive(true);
    }

    public void die()
    {
        GameObject tree = ObjectPoolManager.SpawnGameObject(treeToSpawn, transform.position, Quaternion.identity);
        tree.GetComponent<Tree>().setup();
        ObjectPoolManager.DespawnGameObject(this.gameObject);
    }
}
