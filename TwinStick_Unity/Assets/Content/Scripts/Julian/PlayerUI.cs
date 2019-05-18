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
    [SerializeField] private Slider armorSlider = null;
    [SerializeField] private GameObject gunUIGroup = null;


    /// <summary>
    /// Sets the ammo in the mag
    /// </summary>
    public void SetAmmoInMag(int ammo)
    {
        ammoInMagText.text = ammo.ToString();
    }

    /// <summary>
    /// Sets the ammo in inventoy
    /// </summary>
    public void SetAmmoInInventory(int ammo)
    {
        ammoInInventoryText.text = ammo.ToString();
    }

    /// <summary>
    /// Sets the Health
    /// </summary>
    public void SetHealth(float health,float maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = health;
    }

    /// <summary>
    /// Sets the ArmorHealth
    /// </summary>
    public void SetArmorHealth(float armorHealth, float maxArmorhealth)
    {
        armorSlider.maxValue = maxArmorhealth;
        armorSlider.value = armorHealth;
    }

    /// <summary>
    /// Sets the Gun UI Activity
    /// </summary>
    public void SetGunUIActive(bool active)
    {
        gunUIGroup.SetActive(active);
    }
}
