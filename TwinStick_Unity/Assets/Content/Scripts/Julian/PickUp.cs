﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;


public class PickUp : MonoBehaviour
{

    [SerializeField] private ItemID item = ItemID.keyCardA; 
    [SerializeField] private int count = 30;
    [SerializeField] private float pickUpDelay = 0.3f;
    [SerializeField] private float hightOffset = 1;
    [SerializeField] private int givePerPickUp = 5;
    [SerializeField] private Slider slider = null;

    private bool pickUpAllowed = true;
    private bool colliding = false;
    private Camera mainCamera;
    private Canvas mainCanvas;

    private void Awake()
    {
        mainCamera = Camera.main;
        mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas").GetComponent<Canvas>();

        if (slider != null)
            slider = Instantiate(slider, mainCanvas.transform.position, mainCanvas.transform.rotation, mainCanvas.transform);
    }

    private void Start()
    {
        colliding = false;

        if (slider != null)
        {
            if (count == 1)
                slider.gameObject.SetActive(false);

            slider.maxValue = count;
            slider.minValue = 0;
            slider.value = slider.maxValue;
        }
    }

    private void Update()
    {
        if (colliding)
            if (pickUpAllowed)
                if (Input.GetButton("Use"))
                {
                    // Adds and removes the items
                    if (count >= givePerPickUp)
                    {
                        GameManager.instance.inventory.AddItem(item, givePerPickUp);
                        count -= givePerPickUp;
                    }
                    else if (count > 0)
                    {
                        GameManager.instance.inventory.AddItem(item, count);
                        count = 0;
                    }

                    // Checks if done and destroy
                    if (count <= 0)
                    {
                        Destroy(gameObject);
                    }

                    // Update Slider
                    if (slider != null)
                        slider.value = count;

                    // Create a delay
                    StartCoroutine(AllowDelayCoroutine());
                }
    }

    private void LateUpdate()
    {
        if (slider != null)
            slider.transform.position = mainCamera.WorldToScreenPoint(new Vector3(transform.position.x,transform.position.y,transform.position.z + hightOffset));
    }


    // Resets the delay
    IEnumerator AllowDelayCoroutine()
    {
        pickUpAllowed = false;
        yield return new WaitForSeconds(pickUpDelay);
        pickUpAllowed = true;
    }

    // Checks Collition
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Player>() != null)
            colliding = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Player>() != null)
            colliding = false;
    }


    [ExecuteInEditMode]
    void OnValidate()
    {
        gameObject.name = item.ToString() + "_PickUp";
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position + Vector3.up, "PickUp.psd");
    }

    private void OnDestroy()
    {
        if(slider != null)
            Destroy(slider.gameObject);
    }

}
