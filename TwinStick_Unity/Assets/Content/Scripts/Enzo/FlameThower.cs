using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameThower : MonoBehaviour
{
    [SerializeField] private ParticleSystem FlameThrower;

    // Start is called before the first frame update
    private void Start()
    {
        FlameThrower.Stop();
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            FlameThrower.Play();
        }
        if (Input.GetMouseButtonUp(0))
        {
            FlameThrower.Stop();
        }
    }
}