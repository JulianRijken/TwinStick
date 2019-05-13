using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI ammoInMagText = null; 
    [SerializeField] private TextMeshProUGUI ammoInInventoryText = null;
    [SerializeField] private Slider healthSlider = null;
    [SerializeField] private GameObject gunUIGroup = null;

    public void SetAmmoInMag(int ammo)
    {
        ammoInMagText.text = ammo.ToString();
    }

    public void SetAmmoInInventory(int ammo)
    {
        ammoInInventoryText.text = ammo.ToString();
    }

    public void SetHealth(float health,float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    public void SetGunUIActive(bool active)
    {
        gunUIGroup.SetActive(active);
    }
}
