using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{

    public GameObject spawnerPrefab;

    private GameObject Spawned;

    public Transform spawnLocation;


    private void Start()
    {
        Spawned = null;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if(!Spawned)
            {
                Spawned = Instantiate(spawnerPrefab, spawnLocation.position, spawnerPrefab.transform.rotation);
            }
        }
    }
}
