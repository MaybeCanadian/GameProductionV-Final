using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Serialization;
using UnityEditor.iOS;
using UnityEngine;

public class EnemySpawnerScript : MonoBehaviour
{
    [Tooltip("any wave after the last one will just repeart the last one")]
    public List<Wave> waves;
    public int waveNumber = 0;

    public bool spawnBool = true;

    public List<GameObject> enemyPrefabs;
    public List<SpawnLocations> spawnAreaTransforms;
    public Transform enemyParent;

    private void Start()
    {
        waveNumber = 0;
        InitPrefabs();

        StartCoroutine(SpawnLoop());
    }

    private void InitPrefabs()
    {
        enemyPrefabs = new List<GameObject>();

        enemyPrefabs.Add(Resources.Load<GameObject>("Prefabs/Enemies/Goblin Prefab"));
        enemyPrefabs.Add(Resources.Load<GameObject>("Prefabs/Enemies/Skeleton Prefab"));
    }
    private IEnumerator SpawnLoop()
    {
        while (spawnBool)
        {
            float timer = 0;

            
            Wave NextWave = waves[waveNumber];

            while (timer < NextWave.timeToNextWave)
            {
                timer += Time.deltaTime;
                yield return null;  
            }

            SpawnEnemyWave(NextWave);
            //waveNumber++;
            waveNumber = Mathf.Max(waveNumber, waves.Count - 1);
        }

        yield return null;
    }

    private void SpawnEnemyWave(Wave wave)
    {
        Debug.Log("wave " + waveNumber);
    }
}

[System.Serializable]
public class Wave
{
    public List<SpawnAreas> spawningAreas;
    public List<EnemyCluster> enemies;

    public float timeToNextWave;
    
}

[System.Serializable]
public struct EnemyCluster
{
    public EnemyTypes enemyType;
    public int spawnAmount;
}

[System.Serializable]
public enum EnemyTypes
{
    GOBLIN,
    SKELTON
}

[System.Serializable]
public enum SpawnAreas
{
    DESERT,
    GARDEN,
    CASTLE,
    GOBLIN_CAMP,
    BONES,
    EMPTY
}
