using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lazer : MonoBehaviour
{
    private LineRenderer lineRender;
    private bool on;

    private void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        on = true;
        lineRender.enabled = true;
    }

    public void SwitchPower()
    {
        on = !on;
        lineRender.enabled = !lineRender.enabled;
        UpdateLazer();
    }

    private void Update()
    {
        UpdateLazer();
    }

    void UpdateLazer()
    {
        if (on)
        {
            if (lineRender != null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 10))
                {
                    lineRender.SetPosition(0, transform.position);
                    lineRender.SetPosition(1, hit.point);
                }
                else
                {
                    lineRender.SetPosition(0, transform.position);
                    lineRender.SetPosition(1, transform.position + transform.forward * 20);
                }
            }
        }
    }

}
