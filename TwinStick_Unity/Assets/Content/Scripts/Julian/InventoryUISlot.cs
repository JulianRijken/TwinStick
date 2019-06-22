using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class InventoryUISlot : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage = null;
    [SerializeField] private TextMeshProUGUI countText = null;
    [SerializeField] private Animator animator = null;

    private ItemSlot slot;
    private bool inUse;
    private bool canUse;

    void Awake()
    {
        gameObject.SetActive(false);
        inUse = false;
    }

    void OnEnable()
    {
        inUse = false;
    }

    public void SetSlot(ItemSlot _slot)
    {
        slot = _slot;

        countText.text = slot.count.ToString();
        iconImage.sprite = GameManager.instance.inventory.GetItem(slot.itemID).icon;
        canUse = GameManager.instance.inventory.GetItem(slot.itemID).canUse;
    }


    IEnumerator UseItem()
    {
        inUse = true;
        float timer = 0;

        while(timer < 1f)
        {
            animator.SetFloat("time", timer);

            timer += Time.unscaledDeltaTime / GameManager.instance.inventory.GetItem(slot.itemID).useTime;
            yield return new WaitForSecondsRealtime(Time.unscaledDeltaTime);
        }

        GameManager.instance.notificationCenter.FireItemUsed(slot.itemID);

        if (slot.count > 1)
        {
            slot.count--;
            inUse = false;
            animator.SetFloat("time", 0);
        }
        else
        {
            GameManager.instance.inventory.RemoveItemSlot(slot);
            gameObject.SetActive(false);
        }

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (inUse == false && canUse)
            {
                StartCoroutine(UseItem());
            }
        }
            
    }

}
