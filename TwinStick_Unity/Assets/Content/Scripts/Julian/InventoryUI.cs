using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] private Sprite defaltSprite = null;
    [SerializeField] private InventoryUISlot[] uiSlots = null;


    // JA IK KAN BETER ON ENABLE GEBRUIKEN MAAR DAT GEEFT EEN RAREN BUG WAAR IK NU OM 0:15 GEEN ZIN IN HEB DUS HIER HEB JE UPDATE
    private void Update()
    {
        SetSlots();
    }

    private void SetSlots()
    {
        List<ItemSlot> _itemSlots = GameManager.instance.inventory.GetItemSlots();

        for (int i = 0; i < _itemSlots.Count; i++)
        {
            if (_itemSlots[i].count == 0)
            {
                GameManager.instance.inventory.RemoveItemSlot(_itemSlots[i]);
                i--;
            }
            else
            {
                uiSlots[i].gameObject.SetActive(true);
                uiSlots[i].SetSlot(_itemSlots[i]);
            }
        }


        for (int i = _itemSlots.Count; i < uiSlots.Length; i++)
        {
            uiSlots[i].gameObject.SetActive(false);
        }
    }
}
