using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Unit;

public class Player : MonoBehaviour, IDier
{
    public static Player Instance { get; private set; }
    public Unit unit;

    Dictionary<StatTypes, PlayerStats> stats = new Dictionary<StatTypes, PlayerStats>();

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

        stats[StatTypes.Gold] = new PlayerStats(StatTypes.Gold,0);
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            //stats[StatTypes.Gold].modifyAmount(10);
            unit.wow();
        }
    }

    public float subscribeToStat(StatTypes type, Action<float> callback)
    {
        return stats[type].subscribe(callback);
    }

    public float modifyStat(StatTypes type, float amount)
    {
        stats[type].modifyAmount(amount);
        return stats[type].amount;
    }

    public void die(GameObject source)
    {
        gameObject.SetActive(false);
    }
}
