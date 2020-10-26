using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum RatState
{
    spuug = 0,
    staart,
    bijt
}

public class Rat : Damageable
{

    //// Start is called before the first frame update

    [SerializeField]
    private Animator animRat;

    [SerializeField] private RatState Ratstate;

    private Player player;
    public GameObject Player;

    [SerializeField] private float staartRange;
    [SerializeField] private float bijtRange;
    [SerializeField] private LayerMask hitLayer;

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
                Ratstate = RatState.staart;
            }
            else if (dist <= bijtRange)
            {
                Ratstate = RatState.bijt;
            }
            else if (dist > staartRange)
            {
                Ratstate = RatState.spuug;
            }
        }

        GetComponent<NavMeshAgent>().SetDestination(Player.transform.position);

        UpdateAnimatorValues();
    }

    public void UpdateAnimatorValues()
    {
        Debug.LogFormat("Change: {0}", (int)Ratstate);
        animRat.SetInteger("State", (int)Ratstate);
    }

    private void OnCollisionEnter(Collision collider_enemy)
    {
        Damageable _damageble = collider_enemy.gameObject.GetComponent<Damageable>();
        Debug.Log((1 << collider_enemy.gameObject.layer) + " " + hitLayer.value);
        if (_damageble != null && 1 << collider_enemy.gameObject.layer == hitLayer.value)
        {
            Debug.Log("DAMAGE");
            _damageble.DoDamage(20, "Rat");
        }
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        Destroy(gameObject);
    }
}
