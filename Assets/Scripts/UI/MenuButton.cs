using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI goldText;

    private MarketItem marketItem;
    private Action<MarketItem> callback;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void setup(MarketItem item, Action<MarketItem> callback)
    {
        this.callback = callback;
        this.marketItem = item;

        titleText.text = this.marketItem.name;
        descriptionText.text = this.marketItem.description;

        this.marketItem.subscribeToCostChange(itemCostChanged);
        this.marketItem.subscribeToLevelChange(itemLevelChanged);
    }

    void itemCostChanged(int newCost)
    {
        goldText.text = "$" + newCost;
    }

    void itemLevelChanged(int newLevel)
    {
        levelText.text = "Level: " + newLevel;
    }

    public void clicked()
    {
        callback(marketItem);
    }
}
