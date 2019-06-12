using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TongueState
{
    passive = 0,
    aggressive,
    attacking
}

public class Tongue : MonoBehaviour
{
    private Animator anim;

    [SerializeField] private TongueState state;

    private Player player;

    [SerializeField] private float aggressiveRange;
    [SerializeField] private float attackRange;

    private void Start()
    {
        anim = GetComponent<Animator>();
        player = FindObjectOfType<Player>();

        state = TongueState.passive;

        UpdateAnimatorValues();
    }

    public void Update()
    {
        if (player != null)
        {
            float dist = Vector3.Distance(player.gameObject.transform.position, transform.position);

            if (dist <= aggressiveRange && dist > attackRange)
            {
                //print("Hee, je komt de dicht bij....ratel...ratel!");
                state = TongueState.aggressive;
            }
            else if (dist <= attackRange)
            {
                //print("ik ben heel boos en ga nu aanvallen!");
                state = TongueState.attacking;
            }
            else if (dist > aggressiveRange)
            {
                //print("ik ben chill!");
                state = TongueState.passive;
                //GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);
            }
        }

        UpdateAnimatorValues();
    }

    //public void OnTriggerEnter(Collider other)
    //{
    //    player = other.GetComponent<Player>();

    //    if (player != null)
    //    {
    //        state = TongueState.aggressive;
    //    }
    //}

    //public void OnTriggerExit(Collider other)
    //{
    //    state = TongueState.passive;
    //}

    public void UpdateAnimatorValues()
    {
        anim.SetInteger("State", (int)state);
    }
}