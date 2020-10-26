using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawn : MonoBehaviour
{
    public float spawnDelay = 3f;

    private float spawnTimer = 3f;

    public GameObject smallenemy;
    [SerializeField] public Transform spawnpoint;

    private void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > spawnDelay)
        {
            for (int i = 0; i < 3; i++)
            {
                if (spawnpoint != null)
                {
                    Vector3 _randomPos = spawnpoint.position + Random.insideUnitSphere * 3;
                    _randomPos.y = 0;
                    SpawnEnemy(_randomPos);
                }
            }
            spawnTimer = spawnDelay - spawnTimer;
        }
    }

    private void SpawnEnemy(Vector3 _pos)
    {
        var newsmallenemy = Instantiate(smallenemy, _pos, spawnpoint.rotation);
    }

    
}
