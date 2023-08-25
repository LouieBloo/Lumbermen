using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unit;

public class PlayerStats
{
    public float amount { get; private set; }
    public StatTypes type { get; private set; }

    private List<Action<float>> callbacks = new List<Action<float>>();

    public PlayerStats(StatTypes type, float amount)
    {
        this.type = type;
        this.amount = amount;
    }

    public float subscribe(Action<float> callback)
    {
        callbacks.Add(callback);
        return amount;
    }

    public void modifyAmount(float amountToModify)
    {
        amount += amountToModify;
        foreach (var callback in callbacks)
        {
            callback(amount);
        }
    }

}