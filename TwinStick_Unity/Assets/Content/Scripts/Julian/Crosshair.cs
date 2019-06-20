using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{

    private Transform player;
    private Camera uiCamera;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        uiCamera = Camera.main;
        Cursor.visible = true;
    }

    void LateUpdate()
    {
        if (player != null && uiCamera != null && GameManager.instance.menuState == GameMenuState.clear)
        {
            Vector3 dir = transform.position - player.position;
            Quaternion torot = Quaternion.LookRotation(dir, Vector3.up);
            torot.eulerAngles = new Vector3(90, torot.eulerAngles.y, 0);

            transform.rotation = torot;

            transform.position = GetMousePos(uiCamera, new Vector3(0, 1.5f, 0));
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
}
