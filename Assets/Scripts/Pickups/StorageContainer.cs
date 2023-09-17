using System.Collections.Generic;
using System.Linq;

public class StorageContainer 
{
    public int maxCapacity = 100;
    public int currentCapacity = 0;

    private List<StorageItem> items = new List<StorageItem>();

    public class StorageItem
    {
        public int capacitySpace;
        public string name;

        public StorageItem(int capacitySpace, string name)
        {
            this.capacitySpace = capacitySpace;
            this.name = name;
        }
    }

    public bool canAddItem(StorageItem item)
    {
        return currentCapacity + item.capacitySpace <= maxCapacity;
    }

    public bool addItem(StorageItem item)
    {
        if (canAddItem(item))
        {
            items.Add(item);
            currentCapacity += item.capacitySpace;
            return true;
        }
        return false;
    }

    public StorageItem removeItem(StorageItem item)
    {
        items.Remove(item);
        currentCapacity -= item.capacitySpace;
        return item;
    }

    public List<StorageItem> grabItemsByName(string itemName)
    {
        return items.Where(obj => obj.name == itemName).ToList(); ;
    }

}
