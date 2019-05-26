using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
public class Options : MonoBehaviour
{
    [SerializeField] private TMP_Dropdown resolutionDropdown = null;

    void Start()
    {
        List<string> _resolutionOptions = new List<string>();

        for (int i = 0; i < Screen.resolutions.Length; i++)
        {
            _resolutionOptions.Add(Screen.resolutions[i].width + "x" + Screen.resolutions[i].height);
        }

        resolutionDropdown.AddOptions(_resolutionOptions);
        resolutionDropdown.value = Screen.resolutions.Length;


        

    }

    public void OnChangeResolution(int _index)
    {
        Screen.SetResolution(Screen.resolutions[_index].width, Screen.resolutions[_index].height,Screen.fullScreen);
    }

    public void OnChangeFullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void OnChangeSoftPartcles(bool _soft)
    {
        QualitySettings.softParticles = _soft;
    }

    public void OnChangeVSync(bool _sync)
    {
        if (_sync)
            QualitySettings.vSyncCount = 1;
        else
            QualitySettings.vSyncCount = 0;
    }

    public void OnChangeAntiAliasing(int _index)
    {
        QualitySettings.antiAliasing = (int)Mathf.Pow(2, _index);
    }

    public void OnChangeShadowQuality(int _index)
    {
        switch(_index)
        {
            case 0:
                QualitySettings.shadowResolution = ShadowResolution.VeryHigh;
                QualitySettings.shadowDistance = 200;
                QualitySettings.shadowCascades = 4;
                QualitySettings.shadows = ShadowQuality.All;
                break;
            case 1:
                QualitySettings.shadowResolution = ShadowResolution.High;
                QualitySettings.shadowDistance = 100;
                QualitySettings.shadowCascades = 2;
                QualitySettings.shadows = ShadowQuality.All;
                break;
            case 2:
                QualitySettings.shadowResolution = ShadowResolution.Medium;
                QualitySettings.shadowDistance = 50;
                QualitySettings.shadowCascades = 2;
                QualitySettings.shadows = ShadowQuality.HardOnly;
                break;
            case 3:
                QualitySettings.shadowResolution = ShadowResolution.Low;
                QualitySettings.shadowDistance = 0;
                QualitySettings.shadowCascades = 0;
                QualitySettings.shadows = ShadowQuality.Disable;
                break;



        }
    }


    public void OnChangeTextureQuality(int index)
    {
        QualitySettings.masterTextureLimit = index;
    }

    public void OnChangeAnisotropicFiltering(bool filter)
    {
        if (filter)
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.ForceEnable;
        else
            QualitySettings.anisotropicFiltering = AnisotropicFiltering.Disable;

    }
}
