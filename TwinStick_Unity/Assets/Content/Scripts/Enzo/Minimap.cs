﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public Transform Player;

    private void LateUpdate()
    {
        Vector3 newposition = Player.position;
        newposition.y = transform.position.y;
        transform.position = newposition;
    }
}