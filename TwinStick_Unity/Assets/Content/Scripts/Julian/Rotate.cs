using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
   [SerializeField] private Vector3 speed;


    void Update()
    {
        transform.Rotate(speed * Time.unscaledDeltaTime);
    }
}
