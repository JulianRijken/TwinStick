using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelectScreen : MonoBehaviour
{
    // zet alle level buttons in een array ofzo en zet ze disabled en enabled voor de levels
    [SerializeField] private Tab[] levelTabs = null;


    private void OnEnable()
    {
        for (int i = 0; i < levelTabs.Length; i++)
        {
            levelTabs[i].SetInteractable(GameManager.instance.unlockedLevels[i]);
        }
    }

}
