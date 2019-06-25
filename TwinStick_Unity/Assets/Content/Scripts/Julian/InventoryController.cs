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
    ArmorPack,
    keyCardA,
    keyCardB,
    keyCardC,
    keyCardD,
    keyCardE,
    keyCardF,
    keyCardG
}

/// <summary>
/// Lets you edit the Inventory
/// </summary>
public class InventoryController
{
    private List<ItemSlot> itemSlots = new List<ItemSlot>();

    /// <summary>
    /// Adds the item to the inventory
    /// </summary>
    public void AddItem(ItemID item, int count = 1)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].itemID == item)
            {
                itemSlots[i].count += count;

                GameManager.instance.notificationCenter.FireInventoyItemChange();
                return;
            }
        }

        itemSlots.Add(new ItemSlot(item, count));
        GameManager.instance.notificationCenter.FireInventoyItemChange();
    }

    /// <summary>
    /// Adds the item to the inventory
    /// </summary>
    public void RemoveItemSlot(ItemSlot _slot)
    {
        itemSlots.Remove(_slot);
        GameManager.instance.notificationCenter.FireInventoyItemChange();
    }

    /// <summary>
    /// Remove Items
    /// </summary>
    public void ClearInventoy()
    {
        itemSlots.Clear();
        GameManager.instance.notificationCenter.FireInventoyItemChange();
    }

    /// <summary>
    /// Returns true if the item exits
    /// </summary>
    public ItemSlot GetItemSlot(ItemID item)
    {
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].itemID == item)
                return itemSlots[i];
        }

        return null;
    }

    /// <summary>
    /// Returns The Item when given a Id
    /// </summary>
    public Item_SO GetItem(ItemID item)
    {
        var _recource = Resources.Load("Items/" + item.ToString());
        if (_recource != null)
            return (Item_SO)_recource;
        else
            return null;
    }

    /// <summary>
    /// Returns true if the item exits
    /// </summary>
    public bool CheckItemSlot(ItemID item, int neededCount = 1)
    {
        neededCount = Mathf.Clamp(neededCount, 1, int.MaxValue);

        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (itemSlots[i].itemID == item)
                if (itemSlots[i].count >= neededCount)
                    return true;
        }

        return false;
    }

    /// <summary>
    /// Returns the item slots
    /// </summary>
    public List<ItemSlot> GetItemSlots()
    {
        return itemSlots;
    }
}

/// <summary>
/// holds a type of item and a count of them
/// </summary>
public class ItemSlot
{
    public ItemSlot(ItemID _itemID, int _count)
    {
        itemID = _itemID;
        count = _count;
    }

    public ItemID itemID;
    public int count;
}