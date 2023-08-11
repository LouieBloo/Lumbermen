using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static StorageContainer;

public class LogDropoff : MonoBehaviour
{
    public int maxStorageCapacity = 100;
    public GameObject[] logSprites;
    public TextMeshProUGUI storageText;

    // Start is called before the first frame update
    private StorageContainer container = new StorageContainer();

    void Start()
    {
        container.maxCapacity = maxStorageCapacity;
        updateText();
    }

    public bool addItem(StorageItem item)
    {
        if (container.addItem(item))
        {
            updateText();
            return true;
        }
        
        return false;
    }

    public List<StorageItem> grabItemsByName(string itemName)
    {
        return container.grabItemsByName(itemName);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            Backpack targetBackpack = collision.GetComponent<Backpack>();
            if(targetBackpack != null)
            {
                List<StorageItem> incomingLogs = targetBackpack.grabItemsByName("Log");
                if(incomingLogs != null && incomingLogs.Count > 0)
                {
                    foreach (StorageItem item in incomingLogs)
                    {
                        if(addItem(item))
                        {
                            //must remove it from the backpack
                            targetBackpack.removeItem(item);
                        }
                        else
                        {
                            //we full
                        }
                    }
                }
            }
         }
    }

    void updateText()
    {
        storageText.text = container.currentCapacity + "/" + maxStorageCapacity;

        float percentage = ((float)container.currentCapacity / (float)maxStorageCapacity) * 100f;
        if (percentage > 0 && percentage <= 20)
        {
            logSprites[0].SetActive(true);
            logSprites[1].SetActive(false);
            logSprites[2].SetActive(false);
            logSprites[3].SetActive(false);
            logSprites[4].SetActive(false);
        }
        else if (percentage > 20 && percentage <= 40)
        {
            logSprites[0].SetActive(true);
            logSprites[1].SetActive(true);
            logSprites[2].SetActive(false);
            logSprites[3].SetActive(false);
            logSprites[4].SetActive(false);
        }
        else if (percentage > 40 && percentage <= 60)
        {
            logSprites[0].SetActive(true);
            logSprites[1].SetActive(true);
            logSprites[2].SetActive(true);
            logSprites[3].SetActive(false);
            logSprites[4].SetActive(false);
        }
        else if (percentage > 60 && percentage <= 80)
        {
            logSprites[0].SetActive(true);
            logSprites[1].SetActive(true);
            logSprites[2].SetActive(true);
            logSprites[3].SetActive(true);
            logSprites[4].SetActive(false);
        }
        else if (percentage > 60)
        {
            logSprites[0].SetActive(true);
            logSprites[1].SetActive(true);
            logSprites[2].SetActive(true);
            logSprites[3].SetActive(true);
            logSprites[4].SetActive(true);
        }
        else
        {
            foreach(GameObject g in logSprites)
            {
                g.SetActive(false);
            }
        }
    }

}
