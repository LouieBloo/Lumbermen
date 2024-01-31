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
    public string description;
    [SerializeField]
    public int cost;
    [SerializeField]
    public int level = 0;
    [SerializeField]
    public int costIncreaseEachPurchase;

    [SerializeField]
    public AllUnitPrefabs.UnitName unitToModify;

    [SerializeField]
    public ItemUIArray[] statsToModify;

    [Serializable]
    public class ItemUIArray
    {
        [SerializeField]
        public StatTypes statToModifty;
        [SerializeField]
        public float amount;
    }


    private List<Action<int>> costChangeCallbacks = new List<Action<int>>();
    private List<Action<int>> levelChangeCallbacks = new List<Action<int>>();

    public void buy(Player player)
    {
        if(player.checkStat(StatTypes.Gold) < cost) { return; }

        player.modifyStat(StatTypes.Gold, -cost);

        Unit targetUnit = player.unit;

        switch (unitToModify)
        {
            case AllUnitPrefabs.UnitName.Train:
                targetUnit = Train.Instance.unit;
                break;
            case AllUnitPrefabs.UnitName.TrainDropoff:
                targetUnit = null;
                if (statsToModify[0].statToModifty == StatTypes.TrainDropoffCount)
                {
                    TrainStop.Instance.upgrateDropoffCount();
                }
                
                break;
        }

        if (targetUnit)
        {
            foreach (ItemUIArray itemUI in statsToModify)
            {
                targetUnit.modifyStat(itemUI.statToModifty, itemUI.amount);
            }
        }
        
        cost += costIncreaseEachPurchase;
        level++;

        flushCallbacks();
    }

    public int subscribeToCostChange(Action<int> callback)
    {
        costChangeCallbacks.Add(callback);
        flushCallbacks();
        return cost;
    }

    public int subscribeToLevelChange(Action<int> callback)
    {
        levelChangeCallbacks.Add(callback);
        flushCallbacks();
        return level;
    }

    private void flushCallbacks()
    {
        foreach (var callback in costChangeCallbacks)
        {
            callback(cost);
        }
        foreach (var callback in levelChangeCallbacks)
        {
            callback(level);
        }
    }
}
