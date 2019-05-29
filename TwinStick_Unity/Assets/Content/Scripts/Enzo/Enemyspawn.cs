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
    }

    private void SpawnEnemy()
    {
        var newsmallenemy = Instantiate(smallenemy, spawnpoint.position, spawnpoint.rotation);
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