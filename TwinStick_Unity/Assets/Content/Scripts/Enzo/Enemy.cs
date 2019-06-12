using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Damageable
{
    [SerializeField] private FieldOfView targetFinder = null;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private EnemyStatus status = EnemyStatus.Patrol;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPoint = null;
    [SerializeField] private float resetShotTimer;

    private int nextpoint = 0;
    private NavMeshAgent agent;
    private Transform target;

    private float timer;
    [SerializeField] private float shotTimer;

    //public AudioSource Music;
    //public AudioClip Musicclip;

    private enum EnemyStatus { Patrol, Attack }

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        agent.autoBraking = false;

        GotoNextPoint();
        //Music.clip = Musicclip;

        shotTimer = resetShotTimer;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (status.Equals(EnemyStatus.Patrol))
        {
            if (!agent.pathPending && agent.remainingDistance < 0.5f)
                GotoNextPoint();

            List<Transform> targets = targetFinder.GetTargets();
            if (targets != null && targets.Count > 0)
            {
                target = targets[0];
                status = EnemyStatus.Attack;
                //Music.Play();
            }
        }
        else if (status == EnemyStatus.Attack)
        {
            if (target != null)
                agent.destination = target.position;

            if (shotTimer > 0)
                shotTimer -= Time.deltaTime;
            else if (shotTimer < 0)
            {
                Shoot();
                shotTimer = resetShotTimer;
            }
        }
    }

    private void GotoNextPoint()
    {
        // Returns if no points have been set up
        if (patrolPoints.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = patrolPoints[nextpoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        nextpoint = (nextpoint + 1) % patrolPoints.Length;
    }

    private void Shoot()
    {
        if (bullet != null)
            Instantiate(bullet, shootPoint.position, shootPoint.rotation);
    }

    protected override void OnDeath()
    {
        base.OnDeath();
        Destroy(gameObject);
        //Music.Stop();
    }
}