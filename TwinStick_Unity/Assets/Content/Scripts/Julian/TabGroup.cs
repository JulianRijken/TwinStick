using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabGroup : MonoBehaviour
{
    private Tab[] tabs;

    void Awake()
    {
        tabs = gameObject.GetComponentsInChildren<Tab>();
    }

    /// <summary>
    /// Sets all tabs to normal
    /// </summary>
    public void SetDiffrentTab(Tab _newTab)
    {
        foreach (Tab tab in tabs)
        {
            if (tab != _newTab)
            {
                tab.SetToNormal();
            }
            else
            {
                tab.SetToSelected();
            }
        }
    }
}
