using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawn : MonoBehaviour
{
    //[SerializeField] public GameObject enemyspawn;
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
                Vector3 _randomPos = spawnpoint.position + Random.insideUnitSphere * 3;
                _randomPos.y = 0;
                SpawnEnemy(_randomPos);
            }
            spawnTimer = spawnDelay - spawnTimer;
        }
    }

    private void SpawnEnemy(Vector3 _pos)
    {
        var newsmallenemy = Instantiate(smallenemy, _pos, spawnpoint.rotation);
    }

    //private void SpawnCube()
    //{
    //    Vector3 position = new Vector3(Random.Range(-10.0F, 10.0F), 1, Random.Range(-10.0F, 10.0F));
    //    Instantiate(enemyspawn, position, Quaternion.identity);
    //}

    //private void OnTriggerExit(Collider collider)
    //{
    //    if (collider.gameObject.tag == "player")
    //    {
    //        //SpawnCube();
    //        Destroy(gameObject);
    //    }
    //}

    //private void OnTriggerEnter(Collider collider)
    //{
    //    if (collider.gameObject.tag == "player")
    //    {
    //        //Destroy(gameObject);
    //        SpawnCube();
    //    }
    //}
}