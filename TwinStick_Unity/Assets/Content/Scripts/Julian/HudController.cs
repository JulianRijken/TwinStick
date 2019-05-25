using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// This class controlles all the Heads up display in the game
/// </summary>
public class HudController : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI ammoInMagText = null; 
    [SerializeField] private TextMeshProUGUI ammoInInventoryText = null;
    [SerializeField] private Slider healthSlider = null;
    [SerializeField] private Slider armorSlider = null;
    [SerializeField] private GameObject gunUIGroup = null;
    //[SerializeField] private GameObject gameCursor = null;

    //private Camera mainCamera;


    private NotificationCenter notificationCenter;

    private void Awake()
    {
        notificationCenter = GameManager.instance.notificationCenter;

        notificationCenter.OnArmorHealthChange += HandleArmorHealth;
        notificationCenter.OnGunMagAmmoUpdated += HandleAmmoInMag;
        notificationCenter.OnPlayerHealthChange += HandlePlayerHealth;
        notificationCenter.OnGunInventoyAmmoUpdated += HandleAmmoInInventoy;
    }

    //private void Start()
    //{
    //    mainCamera = Camera.main;
    //}

    //private void LateUpdate()
    //{
    //    gameCursor.transform.position = GetMousePos(mainCamera);
    //}


    /// <summary>
    /// Handles the ammo in mag
    /// </summary>
    private void HandleAmmoInMag(int newAmmoInMag)
    {
        ammoInMagText.text = newAmmoInMag.ToString();
    }

    /// <summary>
    /// Handles the ammo in inventoy
    /// </summary>
    private void HandleAmmoInInventoy(ItemSlot itemSlot)
    {
        ammoInInventoryText.text = (itemSlot != null ? itemSlot.count : 0).ToString();
    }

    /// <summary>
    /// Handles Player Health
    /// </summary>
    private void HandlePlayerHealth(float newHealth, float newMaxHealth)
    {
        healthSlider.maxValue = newMaxHealth;
        healthSlider.value = newHealth;
    }

    /// <summary>
    /// Handles Armor Health
    /// </summary>
    private void HandleArmorHealth(float newArmorHealth, float newMaxArmorHealth)
    {
        armorSlider.maxValue = newMaxArmorHealth;
        armorSlider.value = newArmorHealth;
    }


    /// <summary>
    /// Returns The Mouse Pos
    /// </summary>
    //private Vector3 GetMousePos(Camera cam)
    //{
    //    if (cam != null)
    //    {
    //        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
    //        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

    //        float rayLength;
    //        groundPlane.Raycast(cameraRay, out rayLength);

    //        Vector3 hitPoint = cameraRay.GetPoint(rayLength);
    //        hitPoint += mouseOffset;

    //        hitPoint = cam.WorldToScreenPoint(hitPoint);
    //        return hitPoint;
    //    }

    //    return Vector3.zero;
    //}



    private void OnDestroy()
    {
        notificationCenter.OnGunMagAmmoUpdated -= HandleAmmoInMag;
        notificationCenter.OnPlayerHealthChange -= HandlePlayerHealth;
        notificationCenter.OnGunInventoyAmmoUpdated -= HandleAmmoInInventoy;
        notificationCenter.OnArmorHealthChange -= HandleArmorHealth;
    }
}
