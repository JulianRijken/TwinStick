using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Door : MonoBehaviour
{

    [SerializeField] private Vector3 collisionBox = Vector3.zero;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private string boolName = "IsOpen";

    [Header("Gizoms")]
    [SerializeField] private Color edgeColor = Color.green;
    [SerializeField] private Color insideColor = Color.red;


    void FixedUpdate()
    {
        Debug.Log(GetCollision());
    }

    private bool GetCollision()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position + offset, collisionBox / 2, Quaternion.identity);
        if(hitColliders.Length >= 1)
        {
            return true;
        }


        return false;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = edgeColor;
        Gizmos.DrawWireCube(transform.position + offset, collisionBox);

        Gizmos.color = insideColor;
        Gizmos.DrawCube(transform.position + offset, collisionBox);
    }

}
