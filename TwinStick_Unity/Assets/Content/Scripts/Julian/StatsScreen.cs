using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class StatsScreen : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timesPlayedText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI healthLostText;
    [SerializeField] private TextMeshProUGUI shotsFiredText;
    [SerializeField] private TextMeshProUGUI diedBy;

    private void Awake()
    {
        GameManager.instance.notificationCenter.OnStatsChanged += UpdateUI;

        UpdateUI();
    }

    void UpdateUI()
    {
 
        StatsData data = GameManager.instance.statsController.GetData();

        timesPlayedText.text = data.timesPlayed.ToString();
        scoreText.text = data.score.ToString();
        healthLostText.text = data.healthLost.ToString();
        shotsFiredText.text = data.shotsFired.ToString();

        if (data.diedBy.Count > 0)
            diedBy.text = data.diedBy[data.diedBy.Count - 1].ToString();
        else
            diedBy.text = "No One";
    }

    private void OnDestroy()
    {
        GameManager.instance.notificationCenter.OnStatsChanged -= UpdateUI;
    }

}
