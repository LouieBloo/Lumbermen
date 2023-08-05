using System.Collections;
using System.Collections.Generic;
using DamageNumbersPro;
using UnityEngine;

public class Backpack : MonoBehaviour
{
    public int maxCapacity = 100;
    private int currentCapacity = 0;

    public DamageNumberGUI warningPrefabText;

    private float backpackFullWarningTimer = 10f;
    private float backpackFullWarningDelay = 10f;

    private List<BackPackItem> items = new List<BackPackItem>();

    public class BackPackItem
    {
        public int capacitySpace;
        public string name;

        public BackPackItem(int capacitySpace, string name)
        {
            this.capacitySpace = capacitySpace;
            this.name = name;
        }
    }

    private void Update()
    {
        backpackFullWarningTimer += Time.deltaTime;
    }

    public bool canAddItem(BackPackItem item)
    {
        return currentCapacity + item.capacitySpace <= maxCapacity;
    }

    public bool addItem(BackPackItem item)
    {
        if(canAddItem(item))
        {
            items.Add(item);
            currentCapacity += item.capacitySpace;
            return true;
        }
        else
        {
            showWarningText();
        }

        return false;
    }

    private void showWarningText()
    {
        if(backpackFullWarningTimer >= backpackFullWarningDelay)
        {
            backpackFullWarningTimer = 0f;

            DamageNumber damageNumber = warningPrefabText.Spawn(Vector3.zero);
            //Set the rect parent and anchored position.
            damageNumber.SetAnchoredPosition(GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<RectTransform>(), new Vector2(0, 0));
        }
    }
}
