using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// The name of the item
/// </summary>
public enum ItemID
{
    testItemOne,
    testItemTwoo
}

/// <summary>
/// Lets you edit the Inventory
/// </summary>
public class InventoryController : MonoBehaviour
{

    private List<ItemSlot> inventoryItems = new List<ItemSlot>();

    /// <summary>
    /// Adds the item to the inventory
    /// </summary>
    public void AddItem(ItemID item, int count = 1)
    {
        if (count > 0)
            inventoryItems.Add(new ItemSlot(item, count));
        else
            Debug.LogError("Added a item without a count");
    }

    // ^ Zorg dat als er inventory ruimte is dat the bij de slot word toegevoegd ^


    /// <summary>
    /// Returns true if the item exits
    /// </summary>
    public bool CheckItem(ItemID item,int count = 1)
    {
        if (count > 0)
            for (int i = 0; i < inventoryItems.Count; i++)
            {
                if (inventoryItems[i].itemID == item)
                    if (inventoryItems[i].count >= count)
                        return true;
            }
       
        return false;
    }

    /// <summary>
    /// Returns The Item when given a Id 
    /// </summary>
    public Item GetItem(ItemID item)
    {
        return (Item)Resources.Load("Items/" + item.ToString());
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
