using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    public GameObject Player;

    private void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);
    }
}