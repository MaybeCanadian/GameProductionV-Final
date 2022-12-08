using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory
{
    //--------------------------------------
    //Singleton stuff
    private static Factory instance;
    private List<List<GameObject>> prefabLists; 
    private Factory()
    {
        Initialize();
    }
    public static Factory GetInstance()
    {
        if (instance == null)
        {
            instance = new Factory();
        }

        return Factory.instance;
    }
    //--------------------------------------
    //Spawn Parents
    public Transform spawnParent;
    //--------------------------------------
    //Set Up Functions
    private void Initialize()
    {
        spawnParent = GameObject.FindGameObjectWithTag("Parent").transform;
        SetUpPrefabLists();
        SetUpEnemyPrefabs();
        SetUpProjectilePrfabs();
    }
    private void SetUpPrefabLists()
    {
        prefabLists = new List<List<GameObject>>();
        prefabLists.Add(new List<GameObject>()); //enemies
        prefabLists.Add(new List<GameObject>()); //projectiles
    }
    private void SetUpEnemyPrefabs()
    {
        prefabLists[(int)PrefabType.ENEMY].Add(Resources.Load<GameObject>("Prefabs/Enemies/Goblin Prefab"));
        prefabLists[(int)PrefabType.ENEMY].Add(Resources.Load<GameObject>("Prefabs/Enemies/Skeleton Prefab"));
    }
    private void SetUpProjectilePrfabs()
    {
        prefabLists[(int)PrefabType.PROJECTILE].Add(Resources.Load<GameObject>("Prefabs/Projectiles/Bow_Arrow_Prefab"));
        prefabLists[(int)PrefabType.PROJECTILE].Add(Resources.Load<GameObject>("Prefabs/Projectiles/Ballista_Arrow_Prefab"));
    }
    //--------------------------------------
    //Creation Functions
    public GameObject CreateObject(EnemyTypes enemy)
    {
        GameObject tempObject = GameObject.Instantiate(prefabLists[(int)PrefabType.ENEMY][(int)enemy]);
        if(spawnParent != null)
        {
            tempObject.transform.SetParent(spawnParent);
        }
        tempObject.SetActive(false);
        return tempObject;
    }
    public GameObject CreateObject(ProjectileTypes projectile)
    {
        GameObject tempObject = GameObject.Instantiate(prefabLists[(int)PrefabType.PROJECTILE][(int)projectile]);
        if (spawnParent != null)
        {
            tempObject.transform.SetParent(spawnParent);
        }
        tempObject.SetActive(false);
        return tempObject;
    }
    //--------------------------------------
}

[System.Serializable]
public enum PrefabType
{
    ENEMY,
    PROJECTILE,
}

public enum EnemyTypes
{
    GOBLIN,
    SKELETON
}

public enum ProjectileTypes
{
    ARROW,
    BALLISTA
}