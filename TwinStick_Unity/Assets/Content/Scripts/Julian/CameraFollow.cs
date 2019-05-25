using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float distance = 10;
    [SerializeField] private float offsetDistance = 0.5f;
    [SerializeField] private Vector3 maxOffset = new Vector3();
    [SerializeField] private Transform target = null;

    private Camera cam = null;

    private void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (GameManager.instance.GetMenuState() == GameMenuState.clear)
        {
            if (target != null)
            {
                Vector3 _mouseOffset = GetMousePos(cam) - target.position;
                _mouseOffset *= offsetDistance;

                _mouseOffset = new Vector3(Mathf.Clamp(_mouseOffset.x, -maxOffset.x, maxOffset.x), 0, Mathf.Clamp(_mouseOffset.z, -maxOffset.z, maxOffset.z));

                Vector3 toPos = target.position + new Vector3(0, distance, -distance) + _mouseOffset;
                transform.position = toPos;
            }
        }
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
