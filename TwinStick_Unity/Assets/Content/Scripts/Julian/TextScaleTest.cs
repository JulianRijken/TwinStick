using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextScaleTest : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private AnimationCurve scaleCurve;

    private float scaleTime;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            scaleTime = 0;


        text.rectTransform.localScale = Vector3.one * scaleCurve.Evaluate(scaleTime);
        scaleTime += Time.deltaTime;
    }


}
