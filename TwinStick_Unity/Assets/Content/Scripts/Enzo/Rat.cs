using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RatState
{
    spuug = 0,
    staart,
    bijt
}

public class Rat : Damageable
{
    //public GameObject Player;

    //// Start is called before the first frame update
    //private void Start()
    //{
    //    GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);
    //}

    //// Update is called once per frame
    //private void Update()
    //{
    //}
    [SerializeField]
    private Animator animRat;

    [SerializeField] private RatState Ratstate;

    private Player player;

    [SerializeField] private float staartRange;
    [SerializeField] private float bijtRange;

    private void Start()
    {
        animRat = GetComponent<Animator>();
        player = FindObjectOfType<Player>();

        Ratstate = RatState.spuug;

        UpdateAnimatorValues();
    }

    public void Update()
    {
        if (player != null)
        {
            float dist = Vector3.Distance(player.gameObject.transform.position, transform.position);

            if (dist <= staartRange && dist > bijtRange)
            {
                //print("Hee, je komt de dicht bij....ratel...ratel!");
                Ratstate = RatState.staart;
            }
            else if (dist <= bijtRange)
            {
                //print("ik ben heel boos en ga nu aanvallen!");
                Ratstate = RatState.bijt;
            }
            else if (dist > staartRange)
            {
                //print("ik ben chill!");
                Ratstate = RatState.spuug;
                //GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);
            }
        }

        UpdateAnimatorValues();
    }

    public void UpdateAnimatorValues()
    {
        Debug.LogFormat("Change: {0}", (int)Ratstate);
        animRat.SetInteger("State", (int)Ratstate);
    }
}