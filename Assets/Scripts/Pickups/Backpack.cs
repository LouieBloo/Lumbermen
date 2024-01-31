using System.Collections;
using System.Collections.Generic;
using DamageNumbersPro;
using UnityEngine;
using static StorageContainer;

public class Backpack : MonoBehaviour
{
    public DamageNumberGUI warningPrefabText;

    private float backpackFullWarningTimer = 10f;
    private float backpackFullWarningDelay = 10f;

    private StorageContainer container = new StorageContainer();

    Unit unit;

    private void Start()
    {
        unit = GetComponent<Unit>();
        unit.subscribeToStat(Unit.StatTypes.MaxBackpackCapacity, backpackMaxCapcityChanged);
        //unit.subscribeToStat(Unit.StatTypes.BackpackCurrentCapacity, backpackCapcityChanged);
        //container.maxCapacity = maxCapacity;
    }

    private void Update()
    {
        backpackFullWarningTimer += Time.deltaTime;
    }

    void backpackMaxCapcityChanged(float newCapacity)
    {
        container.maxCapacity = (int)newCapacity;
    }

    public bool addItem(StorageItem item)
    {
        if (container.addItem(item))
        {
            unit.modifyStat(Unit.StatTypes.BackpackCurrentCapacity, item.capacitySpace);
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
        unit.modifyStat(Unit.StatTypes.BackpackCurrentCapacity, -item.capacitySpace);
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
