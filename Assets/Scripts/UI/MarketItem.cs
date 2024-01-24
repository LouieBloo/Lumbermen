using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Unit;

[Serializable]
public class MarketItem
{
    [SerializeField]
    public string name;
    [SerializeField]
    public int cost;
    [SerializeField]
    public int costIncreaseEachPurchase;

    [SerializeField]
    public StatTypes statToModifty;
    [SerializeField]
    public float amount;
    

    public void buy(Player player)
    {
        if(player.checkStat(StatTypes.Gold) < cost) { return; }

        player.modifyStat(StatTypes.Gold, -cost);

        player.unit.modifyStat(statToModifty, amount);

        cost += costIncreaseEachPurchase;
    }
}
