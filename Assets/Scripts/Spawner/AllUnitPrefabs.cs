using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllUnitPrefabs : MonoBehaviour
{

    public static AllUnitPrefabs Instance { get; private set; }

    [System.Serializable] //This attribute makes the class show up in the Unity inspector
    public class Trees
    {
        public string name;
        public GameObject prefab;
    }

    public Trees[] allTrees;

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
    }
}
