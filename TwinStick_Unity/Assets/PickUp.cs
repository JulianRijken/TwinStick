using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

}
