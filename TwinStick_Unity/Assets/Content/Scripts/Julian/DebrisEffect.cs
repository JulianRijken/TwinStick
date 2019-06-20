using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebrisEffect : MonoBehaviour
{
    [SerializeField] private float effectStrength = 10;

    void Start()
    {
        Rigidbody[] childs = GetComponentsInChildren<Rigidbody>();
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].AddForce(Random.Range(-effectStrength, effectStrength), 0, Random.Range(-effectStrength, effectStrength), ForceMode.Impulse);
            childs[i].AddTorque(Random.Range(-effectStrength, effectStrength),0, Random.Range(-effectStrength, effectStrength), ForceMode.Impulse);

            Destroy(childs[i].gameObject, Random.Range(2f, 6f));
        }

    }

}
