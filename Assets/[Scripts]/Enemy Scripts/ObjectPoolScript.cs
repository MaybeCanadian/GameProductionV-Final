using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolScript : MonoBehaviour
{
    public static ObjectPoolScript instance;

    public List<ObjPool> enemyPools;
    public List<ObjPool> projPools;
    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        Initalize();
    }
    private void Initalize()
    {
        SetUpEnemyPools();
        SetUpProjectilePools();

        int it = 0;
        foreach (ObjPool pool in enemyPools)
        {
            BuildEnemyPools((EnemyTypes)it, pool);
            it++;
        }

        it = 0;
        foreach (ObjPool pool in projPools)
        {
            BuildProjectilePools((ProjectileTypes)it, pool);
            it++;
        }
    }

    //---------------------------------------------------
    //Enemies
    private void SetUpEnemyPools()
    {
        enemyPools = new List<ObjPool>();
        projPools = new List<ObjPool> ();

        for(int i = 0; i < Enum.GetNames(typeof(EnemyTypes)).Length; i++)
        {
            enemyPools.Add(new ObjPool());
        }

        for(int i = 0; i < Enum.GetNames(typeof(ProjectileTypes)).Length; i++)
        {
            projPools.Add(new ObjPool());
        }
    }
    private void BuildEnemyPools(EnemyTypes type, ObjPool pool)
    {
        for(int i = 0; i < pool.startingAmount; i++)
        {
            enemyPools[(int)type].pool.Enqueue(CreateEnemy(type));
        }

        pool.remainingAmount = pool.pool.Count;
    }
    private GameObject CreateEnemy(EnemyTypes type)
    {
        GameObject tempObject = Factory.GetInstance().CreateObject(type);
        return tempObject;
    }
    public GameObject GetEnemy(EnemyTypes type)
    {
        ObjPool pool = enemyPools[(int)type];

        if(pool.pool.Count <= 0)
        {
            pool.pool.Enqueue(CreateEnemy(type));
            pool.remainingAmount = pool.pool.Count;
        }

        GameObject tempObject = pool.pool.Dequeue();
        EnemyScript enemy = tempObject.GetComponent<EnemyScript>();
        enemy.Activate();

        tempObject.SetActive(true);
        pool.activeAmount++;
        pool.remainingAmount = pool.pool.Count;

        return tempObject;
    }
    public void ReturnEnemy(GameObject enemy, EnemyTypes type)
    {
        ObjPool pool = enemyPools[(int)type];

        EnemyScript enemScript = enemy.GetComponent<EnemyScript>();

        enemy.SetActive(false);
        pool.pool.Enqueue(enemy);
        pool.activeAmount--;
        pool.remainingAmount = pool.pool.Count;
    }
    //---------------------------------------------------
    //Projectile Functions
    private void SetUpProjectilePools()
    {
        projPools = new List<ObjPool>();

        for (int i = 0; i < Enum.GetNames(typeof(ProjectileTypes)).Length; i++)
        {
            projPools.Add(new ObjPool());
        }
    }
    private void BuildProjectilePools(ProjectileTypes type, ObjPool pool)
    {
        for (int i = 0; i < pool.startingAmount; i++)
        {
            projPools[(int)type].pool.Enqueue(CreateProjectile(type));
        }

        pool.remainingAmount = pool.pool.Count;
    }
    private GameObject CreateProjectile(ProjectileTypes type)
    {
        GameObject tempObject = Factory.GetInstance().CreateObject(type);
        return tempObject;
    }
    public GameObject GetProjectile(ProjectileTypes type)
    {
        ObjPool pool = projPools[(int)type];

        if (pool.pool.Count <= 0)
        {
            pool.pool.Enqueue(CreateProjectile(type));
            pool.remainingAmount = pool.pool.Count;
        }

        GameObject tempObject = pool.pool.Dequeue();
        tempObject.SetActive(true);
        pool.activeAmount++;
        pool.remainingAmount = pool.pool.Count;

        return tempObject;
    }
    public void ReturnProjectile(GameObject proj, ProjectileTypes type)
    {
        ObjPool pool = projPools[(int)type];

        ProjectileScript projScript = proj.GetComponent<ProjectileScript>();

        if(projScript)
        {
            projScript.rb.velocity = new Vector3(0.0f, 0.0f, 0.0f);
        }

        proj.SetActive(false);
        pool.pool.Enqueue(proj);
        pool.activeAmount--;
        pool.remainingAmount = pool.pool.Count;
    }
    //---------------------------------------------------
}

[System.Serializable]
public class ObjPool
{
    public ObjPool()
    {
        pool = new Queue<GameObject>();
    }

    public Queue<GameObject> pool;
    public int startingAmount = 50;
    public int activeAmount = 0;
    public int remainingAmount = 0;
}