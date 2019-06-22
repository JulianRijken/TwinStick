using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NAME THE SAME AS ENUM", menuName = "Inventory/New Item", order = 1)]
public class Item_SO : ScriptableObject
{
    [Header("Standard")]
    public Sprite icon; 

    [Header("Usage")]
    public bool canUse;
    public float useTime;
}
