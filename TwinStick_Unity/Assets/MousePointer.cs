using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour
{

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    void LateUpdate()
    {
        transform.position = GetMousePos(cam);
    }

    /// <summary>
    /// Returns The Mouse Pos
    /// </summary>
    private Vector3 GetMousePos(Camera cam)
    {
        Ray cameraRay = cam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        float rayLength;
        groundPlane.Raycast(cameraRay, out rayLength);

        return cameraRay.GetPoint(rayLength);
    }
}
