using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemyspawn : MonoBehaviour
{
    //[SerializeField] public GameObject enemyspawn;
    public float spawnTime = 3f;

    public GameObject smallenemy;
    [SerializeField] public Transform spawnpoint;

    // Use this for initialization
    private void Start()
    {
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    // Update is called once per frame
    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            Vector3 _randomPos = spawnpoint.position + Random.insideUnitSphere * 10;
            _randomPos.y = 0;
            SpawnEnemy(_randomPos);
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