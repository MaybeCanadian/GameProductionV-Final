using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawnerScript : MonoBehaviour
{
    public List<GameObject> enemyPrefabs;

    public float spawnDelay = 1.0f;

    public Transform enemyParent;

    public Transform spawnPosition;

    private void Start()
    {
        StartCoroutine("SpawningLoop");
    }

    private IEnumerator SpawningLoop()
    {
        while(true)
        {
            SpawnEnemy(enemyPrefabs[Random.Range(0, enemyPrefabs.Count - 1)]);

            yield return new WaitForSeconds(spawnDelay);
        }

        yield break;
    }

    private void SpawnEnemy(GameObject prefab)
    {
        Instantiate(prefab, spawnPosition, enemyParent);
    }
}
