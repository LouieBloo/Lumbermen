using System.Collections;
using System.Collections.Generic;
using DamageNumbersPro;
using UnityEngine;
using static StorageContainer;
using static UnityEditor.Progress;

public class Backpack : MonoBehaviour
{
    public int maxCapacity = 100;

    public DamageNumberGUI warningPrefabText;

    private float backpackFullWarningTimer = 10f;
    private float backpackFullWarningDelay = 10f;

    private StorageContainer container = new StorageContainer();

    private void Start()
    {
        container.maxCapacity = maxCapacity;
    }

    private void Update()
    {
        backpackFullWarningTimer += Time.deltaTime;
    }

    public bool addItem(StorageItem item)
    {
        if (container.addItem(item))
        {
            return true;
        }
        else
        {
            showWarningText();
        }

        return false;
    }

    public List<StorageItem> grabItemsByName(string itemName)
    {
        return container.grabItemsByName(itemName);
    }

    public void removeItem(StorageItem item)
    {
        container.removeItem(item);
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
