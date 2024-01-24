using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : MonoBehaviour
{
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
    }

    public void clicked()
    {
        callback(marketItem);
    }
}
