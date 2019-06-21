using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] private Sprite defaltSprite = null;
    [SerializeField] private InventoryUISlot[] slots = null;


    // JA IK KAN BETER ON ENABLE GEBRUIKEN MAAR DAT GEEFT EEN RAREN BUG WAAR IK NU OM 0:15 GEEN ZIN IN HEB DUS HIER HEB JE UPDATE
    private void Update()
    {
        SetSlots();
    }

    private void SetSlots()
    {
        List<ItemSlot> _itemSlots = GameManager.instance.inventory.GetItemSlots();

        foreach(InventoryUISlot slot in slots)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = 0; i < _itemSlots.Count; i++)
        {
            if (i < slots.Length)
            {
                if (_itemSlots[i].count > 0)
                {
                    slots[i].gameObject.SetActive(true);
                    slots[i].SetCount(_itemSlots[i].count);

                    Sprite _icon = GameManager.instance.inventory.GetItem(_itemSlots[i].itemID).icon;

                    if (_icon != null)
                    {
                        slots[i].SetIcon(_icon);
                    }
                    else if(defaltSprite != null)
                    {
                        slots[i].SetIcon(defaltSprite);
                    }
                        
                }
                else
                {
                    GameManager.instance.inventory.GetItemSlots().Remove(_itemSlots[i]);
                    slots[i].gameObject.SetActive(false);
                    i--;
                }

            }
        }
    }


}
