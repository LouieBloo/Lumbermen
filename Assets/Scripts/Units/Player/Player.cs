using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }
    public Unit unit;

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

        unit = GetComponent<Unit>();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GetComponent<HealthHaver>().takeDamage(10);
        }    
    }

}
