using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    //private float timeleft = 30.0f;
    private float currCountdownValue;

    //public Text countwdown;

    public IEnumerator StartCountdown(float countdownValue = 170)
    {
        currCountdownValue = countdownValue;
        while (currCountdownValue > 0)
        {
            //Debug.Log("Countdown: " + currCountdownValue);
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
        //timeleft -= Time.deltaTime;
        //if (timeleft < 0)
        //{
        //    SceneManager.LoadScene(0);
        //}
        if (currCountdownValue < 1)
        {
            //SceneManager.LoadScene(0);
            Debug.Log("Gameover");
        }
        //countwdown.text = "time =" + currCountdownValue.ToString();
    }
}