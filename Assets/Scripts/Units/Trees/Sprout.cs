using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprout : MonoBehaviour
{
    public float timeBetweenSprites = 5f;
    private float totalRespawnTimer = 0f;
    public GameObject treeToSpawn;
    public GameObject[] sprites;
    private int currentSpriteIndex = 0;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
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
            Instantiate(treeToSpawn,transform.position,Quaternion.identity);
            //Destroy(this.gameObject);
        }
        else
        {
            sprites[currentSpriteIndex-1].gameObject.SetActive(false);
            sprites[currentSpriteIndex].gameObject.SetActive(true);
        }
    }

}
