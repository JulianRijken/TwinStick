using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private float distance = 10;
    [SerializeField] private float offsetDistance = 0.5f;
    [SerializeField] private Vector3 maxOffset = new Vector3();
    [SerializeField] private Transform target;


    void FixedUpdate()
    {
        Vector3 mouseOffset = GetMousePos(Camera.main) - target.position;
        mouseOffset *= offsetDistance;

        mouseOffset = new Vector3(Mathf.Clamp(mouseOffset.x, -maxOffset.x, maxOffset.x), 0, Mathf.Clamp(mouseOffset.z, -maxOffset.z, maxOffset.z));

        Debug.Log(mouseOffset);

        Vector3 toPos = target.position + new Vector3(0, distance, -distance) + mouseOffset;
        transform.position = toPos;
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
