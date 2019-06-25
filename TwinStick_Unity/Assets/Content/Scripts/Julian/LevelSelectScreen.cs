using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScreen : MonoBehaviour
{
    // zet alle level buttons in een array ofzo en zet ze disabled en enabled voor de levels
    [SerializeField] private Tab[] levelTabs = null;

    private void Awake()
    {
        GameManager.instance.notificationCenter.OnSetLevelInteractable += SetLevelInteractible;
    }


    public void SetLevelInteractible(int _level, bool _locked)
    {
        levelTabs[_level].SetInteractable(_locked);
    }

    private void OnDestroy()
    {
        GameManager.instance.notificationCenter.OnSetLevelInteractable -= SetLevelInteractible; 
    }
}
