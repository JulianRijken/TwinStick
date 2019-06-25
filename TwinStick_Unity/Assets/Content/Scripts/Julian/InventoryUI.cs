using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    [SerializeField] private Sprite defaltSprite = null;
    [SerializeField] private InventoryUISlot[] uiSlots = null;

    private void OnEnable()
    {
        SetSlots();
    }

    private void LateUpdate()
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
