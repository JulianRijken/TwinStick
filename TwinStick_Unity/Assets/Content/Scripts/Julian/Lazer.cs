using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lazer : MonoBehaviour
{
    private LineRenderer lineRender;
    private bool on;

    private Vector2 textureOffset;
    [SerializeField] private float speed = 0.1f;

    private void Start()
    {
        lineRender = GetComponent<LineRenderer>();
        on = true;
        lineRender.enabled = true;
        textureOffset.y = lineRender.material.mainTextureOffset.y;
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

        textureOffset.x -= Time.deltaTime  * speed;
        //textureOffset.y += Time.deltaTime  * speed;

        lineRender.material.mainTextureOffset = textureOffset;
    }

    void UpdateLazer()
    {
        if (on)
        {
            if (lineRender != null)
            {
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.forward, out hit, 100f))
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
