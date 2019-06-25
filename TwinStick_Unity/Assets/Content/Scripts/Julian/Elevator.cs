using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{

    [Header("Animation")]
    [SerializeField] private Animator animator = null;


    [Header("Item Requirement")]
    [SerializeField] private ItemID requiredItem = ItemID.Ammo;
    [SerializeField] private int requiredItemCount = 1;
    [SerializeField] private bool requireItem = false;


    /// <summary>
    /// Checks If you got the item
    /// </summary>
    private bool GotItem()
    {
        return requireItem ? GameManager.instance.inventory.CheckItemSlot(requiredItem, requiredItemCount) : true;
    }

    private void OnTriggerEnter(Collider other)
    {
        Player _player = other.GetComponent<Player>();

        if (_player != null && GotItem())
        {
            _player.SetPlayerMovementState(PlayerMovementState.locked);
            animator.SetTrigger("Close");
            GameManager.instance.notificationCenter.FireBlur(true);
        }
    }

    public void EscapePlayer()
    {
        GameManager.instance.notificationCenter.FirePlayerEscaped();
    }
}
