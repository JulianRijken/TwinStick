using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    private float currCountdownValue;


    public IEnumerator StartCountdown(float countdownValue = 170)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            yield return new WaitForSeconds(1.0f);
            currCountdownValue--;
        }
    }

    private void Start()
    {
        StartCoroutine(StartCountdown());
    }

    private void Update()
    {
      
        if (currCountdownValue < 1)
        {
            Debug.Log("Gameover");
        }
    }
}
