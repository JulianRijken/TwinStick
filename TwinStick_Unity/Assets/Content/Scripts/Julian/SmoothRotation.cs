using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothRotation : MonoBehaviour
{
    [SerializeField] private Transform toTransform;
    [SerializeField] private float speed;

    void Update()
    {
        if (toTransform != null)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, toTransform.rotation, Time.deltaTime * speed);

            transform.position = toTransform.position;
        }
    }
}
