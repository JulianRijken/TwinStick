using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class InventoryUISlot : MonoBehaviour, IPointerClickHandler
{
    private Image iconImage = null;
    private TextMeshProUGUI countText = null;

    void Awake()
    {
        iconImage = GetComponentInChildren<Image>();
        countText = GetComponentInChildren<TextMeshProUGUI>();
        SetIcon(GameManager.instance.inventory.GetItem(ItemID.keyCardA).icon);

        gameObject.SetActive(false);
    }

    public void SetIcon(Sprite _sprite)
    {
        iconImage.sprite = _sprite;
    }

    public void SetCount(int _count)
    {
        countText.text = _count.ToString();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            // Show options to use and maybe throw away
            Debug.Log("Pressed0");
        }
            
    }

}
