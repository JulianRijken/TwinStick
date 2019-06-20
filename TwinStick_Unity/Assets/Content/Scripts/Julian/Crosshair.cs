using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private Image ammoCircle;
    [SerializeField] private float hightOffset;
    [SerializeField] private float sidewaysOffset;

    private Transform player;
    private Camera uiCamera;

    private void Awake()
    {
        GameManager.instance.notificationCenter.OnGunMagAmmoUpdated += HandleAmmoInMag;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        uiCamera = Camera.main;
        Cursor.visible = false;
    }

    void LateUpdate()
    {
        if (player != null && uiCamera != null && GameManager.instance.menuState == GameMenuState.clear)
        {
            Vector3 dir = transform.position - player.position;
            Quaternion torot = Quaternion.LookRotation(dir, Vector3.up);
            torot.eulerAngles = new Vector3(90, torot.eulerAngles.y, 0);

            transform.rotation = torot;

            transform.position = GetMousePos(uiCamera, new Vector3(0,hightOffset,0) + transform.right * sidewaysOffset);
        }
    }

    /// <summary>
    /// Handles the ammo in mag
    /// </summary>
    private void HandleAmmoInMag(int newAmmoInMag, int maxAmmo)
    {
        if (maxAmmo > 0)
        {
            ammoCircle.fillAmount = newAmmoInMag / (float)maxAmmo;
        }
        else
        {
            ammoCircle.fillAmount = 1;
        }
    }

    /// <summary>
    /// Returns The Mouse Pos
    /// </summary>
    private Vector3 GetMousePos(Camera cam,Vector3 offset)
    {
        if (cam != null)
        {
            Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
            Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

            float rayLength;
            groundPlane.Raycast(cameraRay, out rayLength);

            return cameraRay.GetPoint(rayLength) + offset;
        }

        return Vector3.zero;
    }


    private void OnDestroy()
    {
        GameManager.instance.notificationCenter.OnGunMagAmmoUpdated -= HandleAmmoInMag;
    }
}
