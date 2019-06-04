using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MiniEnemys : MonoBehaviour
{
    public GameObject Player;

    // Update is called once per frame
    private void Update()
    {
        GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);

        //Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }

    //protected override void ()
}