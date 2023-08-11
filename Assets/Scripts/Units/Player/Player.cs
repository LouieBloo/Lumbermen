using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public int strength { get; private set; }
    public int agility { get; private set; }
    public int intelligence { get; private set; }

    public enum StatTypes { Strength, Agility, Intelligence}

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

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            GetComponent<HealthHaver>().takeDamage(10);
        }    
    }

    public void levelUp(StatTypes stat, int amount)
    {
        switch(stat)
        {
            case StatTypes.Strength:
                strength += amount;
                break;
            case StatTypes.Agility:
                agility += amount;
                break;
            case StatTypes.Intelligence:
                intelligence += amount;
                break;
        }

        Debug.Log(strength + " " + agility + " " + intelligence);
    }
}
