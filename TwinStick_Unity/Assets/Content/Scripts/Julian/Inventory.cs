using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The name of the item
/// </summary>
public enum ItemID
{
    Ammo,
    HealthPack,
    Keycard
}

/// <summary>
/// Lets you edit the Inventory
/// </summary>
public class InventoryController
{

    private List<ItemSlot> inventoryItems = new List<ItemSlot>();


    /// <summary>
    /// Adds the item to the inventory
    /// </summary>
    public void AddItem(ItemID item, int count = 1)
    {
        count = Mathf.Clamp(count,1, int.MaxValue);

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].itemID == item)
            {
                int lastCount = inventoryItems[i].count;           
                inventoryItems.Add(new ItemSlot(item, lastCount + count));
                return;
            }
        }

        inventoryItems.Add(new ItemSlot(item, count));

    }

    /// <summary>
    /// Returns true if the item exits
    /// </summary>
    public bool CheckItemSlot(ItemID item,int neededCount = 1)
    {
        neededCount = Mathf.Clamp(neededCount, 1, int.MaxValue);

        for (int i = 0; i < inventoryItems.Count; i++)
        {
            if (inventoryItems[i].itemID == item)
                if (inventoryItems[i].count >= neededCount)
                    return true;
        }

        return false;
    }


    /// <summary>
    /// Returns The Item when given a Id 
    /// </summary>
    public Item_SO GetItem(ItemID item)
    {
        return (Item_SO)Resources.Load("Items/" + item.ToString());
    }

}


/// <summary>
/// holds a type of item and a count of them
/// </summary>
public struct ItemSlot
{
    public ItemSlot(ItemID _itemID,int _count)
    {
        itemID = _itemID;
        count = _count;
    }

    public ItemID itemID;
    public int count;
}
