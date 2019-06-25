using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private string parameterName = "progress";
    [SerializeField] private Animator doorAnimator = null;
    [SerializeField] private float animationSpeed = 1;

    [Header("Item Requirement")]
    [SerializeField] private Animator lockAnimatior = null;
    [SerializeField] private ItemID requiredItem = ItemID.Ammo;
    [SerializeField] private int requiredItemCount = 1;
    [SerializeField] private bool requireItem = false;

    [Header("Effects")]
    [SerializeField] private Light[] lamp = new Light[0];
    [SerializeField] private Color enabledColor = Color.green;
    [SerializeField] private Color disabledColor = Color.red;

    private bool colliding;
    private float progress;

    private void Update()
    {
        if (colliding && GotItem())      
            progress += Time.deltaTime * animationSpeed;       
        else
            progress -= Time.deltaTime * animationSpeed;
        

        progress = Mathf.Clamp(progress, 0f, 1f);
        doorAnimator.SetFloat(parameterName, progress);

        if (lamp.Length > 0)
        {
            if (GotItem())
            {
                for (int i = 0; i < lamp.Length; i++)
                    lamp[i].color = enabledColor;
            }
            else
            {
                for (int i = 0; i < lamp.Length; i++)
                    lamp[i].color = disabledColor;
            }
        }


        lockAnimatior.SetBool("gotItem", GotItem());

    }


    /// <summary>
    /// Checks If you got the item
    /// </summary>
    private bool GotItem()
    {
        return requireItem ? GameManager.instance.inventory.CheckItemSlot(requiredItem, requiredItemCount) : true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            colliding = true;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.GetComponent<Player>() != null)
        {
            colliding = false;
        }

    }

}
