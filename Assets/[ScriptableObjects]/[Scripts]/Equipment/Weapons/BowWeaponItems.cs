using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Bow", menuName = "Equipment/Weapons/Bows")]
public class BowWeaponItems : WeaponItems
{
    [Tooltip("How long the fired arow lasts after shot")]
    public float DecayTime = 10.0f;
    [Tooltip("How fast thne arrow fires")]
    public float Speed = 30.0f;
    [Tooltip("How many times the arrow can pierce")]
    public int Pierces = 0;
    [Tooltip("The arrow this weapon shoots")]
    public GameObject arrorPrefab;

    public override void attack(Transform currentPosition)
    {
        Vector3 vectorSpawnOffset = currentPosition.forward * 0.5f;
        Vector3 SpawnPosition = currentPosition.position;
        SpawnPosition += vectorSpawnOffset;
        SpawnPosition = new Vector3(SpawnPosition.x, SpawnPosition.y + 1.0f, SpawnPosition.z);

        Quaternion spawnRotation = new Quaternion(currentPosition.transform.rotation.x + Mathf.Deg2Rad * 5.0f, 
            currentPosition.transform.rotation.y, currentPosition.transform.rotation.z, 
            currentPosition.transform.rotation.w);

        GameObject projParent =  GameObject.FindGameObjectWithTag("Parent");
        GameObject arrow = Instantiate(arrorPrefab, SpawnPosition, spawnRotation, projParent.transform);

        ProjectileScript proj = arrow.GetComponent<ProjectileScript>();

        if(proj)
        {
            proj.Initialize(DecayTime, 
                attackDamage * PlayerStatsScript.instance.GetStat(StatTypes.DAMAGE) 
                + new Vector2(PlayerStatsScript.instance.GetStat(StatTypes.RANGEDDAMAGE), 
                PlayerStatsScript.instance.GetStat(StatTypes.RANGEDDAMAGE))
                , Speed * PlayerStatsScript.instance.GetStat(StatTypes.RANGEDPROJSPEED));
        }


    }

}
