using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEditor.iOS;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    public List<BoxCollider> spawnAreas;
    public LayerMask spawnOverlapCheckLayers;
    public bool spawnEnabled = true;

    public int waveNumber = 0;

    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        float timer = 0.0f;
        float spawnTimer = 0.0f;

        while(spawnEnabled)
        {
            timer = 0.0f;
            spawnTimer = UnityEngine.Random.Range(5.0f, 15.0f);

            while (timer <= spawnTimer)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            for(int i = 0; i < waveNumber + 1; i++)
            {
                SpawnEnemies();
            }

            waveNumber++;
            yield return null;
        }

        yield return null;
    }
    private void SpawnEnemies()
    {
        BoxCollider spawnArea = spawnAreas[UnityEngine.Random.Range(0, spawnAreas.Count)];


        Debug.Log(spawnArea.name);
        EnemyTypes enemyType = (EnemyTypes)UnityEngine.Random.Range(0, Enum.GetNames(typeof(EnemyTypes)).Length);

        GameObject enemy = ObjectPoolScript.instance.GetEnemy(enemyType);

        Vector3 spawnPosition = GetRandomSpawnPosition(spawnArea);

        CapsuleCollider enemyCol = enemy.GetComponent<CapsuleCollider>();

        while(Physics.OverlapSphere(spawnPosition, 1.0f, spawnOverlapCheckLayers).Length > 0)
        {
            Debug.Log("rechecking");
            spawnPosition = GetRandomSpawnPosition(spawnArea);
        }

        Debug.Log("found spawn position at " + spawnPosition);
        enemy.SetActive(true);
        enemy.transform.position = spawnPosition;
        EnemyScript enemScript = enemy.GetComponent<EnemyScript>();
        enemScript.Activate();
    }

    private Vector3 GetRandomSpawnPosition(BoxCollider spawnArea)
    {
        float spawnX = UnityEngine.Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x);
        float spawnZ = UnityEngine.Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z);

        Vector3 spawnPosition = new Vector3(spawnX, 1.0f, spawnZ);
        //.spawnPosition += new Vector3(spawnArea.transform.position.x, 0.0f, spawnArea.transform.position.z);

        return spawnPosition;
    }
  
}
