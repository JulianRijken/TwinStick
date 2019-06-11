using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenToggle : MonoBehaviour
{
    [SerializeField] private ToggleScreen[] screens = null;

    /// <summary>
    /// Turns all screens in active and the given nu to active
    /// </summary>
    public void SetScreenActive(int _screenNum)
    {
        foreach(ToggleScreen screen in screens )
        {
            if(screen.screenNumber == _screenNum)         
                screen.screenObj.SetActive(true);
            else
                screen.screenObj.SetActive(false);

        }
    }
}

[System.Serializable]
public struct ToggleScreen
{
    public int screenNumber;
    public GameObject screenObj;
}
