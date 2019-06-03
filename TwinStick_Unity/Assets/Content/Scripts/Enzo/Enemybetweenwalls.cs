using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemybetweenwalls : MonoBehaviour
{
    public Transform Startpoint;
    public Transform Endpoint;

    public float speed = 1.0f;

    private float StartTime;

    private float JourneyLength;

    // Start is called before the first frame update
    private void Start()
    {
        StartTime = Time.time;

        JourneyLength = Vector3.Distance(Startpoint.position, Endpoint.position);
    }

    // Update is called once per frame
    private void Update()
    {
        float distCoverd = (Time.time - StartTime) * speed;

        float fracJourney = distCoverd / JourneyLength;

        transform.position = Vector3.Lerp(Startpoint.position, Endpoint.position, fracJourney);
    }
}