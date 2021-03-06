﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlink : MonoBehaviour
{
    [SerializeField] private Light lamp;
    [SerializeField] private float lerpSpeed = 5;
    [SerializeField] private Vector2 randomTimeBitween = new Vector2(0f,0.4f);
    [SerializeField] private Vector2 randomIntensityBitween = new Vector2(0f,0.1f);


    float toIntensity;

    IEnumerator Start()
    {
        if(lamp == null)
        {
            lamp = GetComponent<Light>();
        }

        if (lamp == null)
            Destroy(this);


        while (true)
        {
            // random intensity
            toIntensity = Random.Range(randomIntensityBitween.x, randomIntensityBitween.y);

            // random time
            yield return new WaitForSeconds(Random.Range(randomTimeBitween.x, randomTimeBitween.y));
        }
    }

    private void Update()
    {
            lamp.intensity = Mathf.Lerp(lamp.intensity, toIntensity,Time.deltaTime * lerpSpeed);

    }
}
