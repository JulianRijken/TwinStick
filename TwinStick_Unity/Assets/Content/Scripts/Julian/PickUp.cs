using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteInEditMode]
public class PickUp : MonoBehaviour
{
    [SerializeField] private ItemID item = ItemID.Keycard; 
    [SerializeField] private int count = 1;

    private void OnTriggerEnter(Collider collder)
    {
        Player player = collder.GetComponent<Player>();

        if(player != null)
        {
            GameManager.instance.inventory.AddItem(item, count);
            Destroy(gameObject);
        }
    }



    void OnValidate()
    {
        gameObject.name = item.ToString() + "_PickUp";
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + Vector3.up, "PickUp.psd");
    }

}
