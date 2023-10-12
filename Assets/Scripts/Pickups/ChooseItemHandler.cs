using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChooseItemHandler : MonoBehaviour
{
    public GameObject ui;

    public TMP_Text outgoingItemNameText;
    public TMP_Text outgoingItemDescription;
    public RawImage outgoingItemImage;

    public TMP_Text incomingItemNameText;
    public TMP_Text incomingItemDescription;
    public RawImage incomingItemImage;

    public static ChooseItemHandler Instance { get; private set; }

    private Action<Item, Item, Item> itemChoosenCallback;

    private Item outgoingItem;
    private Item incomingItem;

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

    public void chooseItem(Action<Item, Item, Item> callback, Item outgoingItem, Item incomingItem)
    {
        Time.timeScale = 0f;

        this.outgoingItem = outgoingItem;
        this.incomingItem = incomingItem;
        itemChoosenCallback = callback;
        
        ui.SetActive(true);

        outgoingItemImage.texture = outgoingItem.getSprite().texture;
        outgoingItemNameText.text = outgoingItem.getName();
        outgoingItemDescription.text = outgoingItem.getDescription();

        incomingItemImage.texture = incomingItem.getSprite().texture;
        incomingItemNameText.text = incomingItem.getName();
        incomingItemDescription.text = incomingItem.getDescription();
    }

    public void close(Item chosenItem)
    {
        Time.timeScale = 1.0f;
        ui.SetActive(false);
        itemChoosenCallback(outgoingItem, incomingItem, chosenItem);
    }

    public void outgoingChoosen()
    {
        close(outgoingItem);
    }

    public void incomingChoosen()
    {
        close(incomingItem);
    }
}
