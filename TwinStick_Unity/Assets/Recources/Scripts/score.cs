using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class score : MonoBehaviour
{
    public static int Score;

    private Text text;

    public void Awake()
    {
        text = GetComponent<Text>();

        Score = 0;
    }

    private void Update()
    {
        text.text = "score " + Score;
    }
}