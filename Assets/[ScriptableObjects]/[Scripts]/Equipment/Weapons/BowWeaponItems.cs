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
    [Tooltip("Number of arrows to shoot")]
    public int volleyAmount = 1;
    [Tooltip("Angle between arrows")]
    public float volleyAngle = 0.0f;
    [Tooltip("The arrow this weapon shoots")]
    public ProjectileTypes arrowType;

    public override void attack(Transform currentPosition)
    {
        Vector3 vectorSpawnOffset = currentPosition.forward * 0.5f;
        Vector3 SpawnPosition = currentPosition.position;
        SpawnPosition += vectorSpawnOffset;
        SpawnPosition = new Vector3(SpawnPosition.x, SpawnPosition.y + 2.0f, SpawnPosition.z);

        Quaternion spawnRotation = new Quaternion(currentPosition.transform.rotation.x + Mathf.Deg2Rad * 5.0f, 
            currentPosition.transform.rotation.y, currentPosition.transform.rotation.z, 
            currentPosition.transform.rotation.w);

        float totalFanAngle = volleyAngle * volleyAmount - 1;

        for (int i = 0; i < volleyAmount; i++)
        {
            Quaternion tempSpawnRotation = spawnRotation;
            tempSpawnRotation.y += Mathf.Deg2Rad * (-totalFanAngle/2.0f + (i * volleyAngle));
            SpawnArrow(tempSpawnRotation, SpawnPosition);
            Debug.Log(totalFanAngle);
        }
    }

    private void SpawnArrow(Quaternion rotation, Vector3 position)
    {
        GameObject projParent = GameObject.FindGameObjectWithTag("Parent");
        GameObject arrow = ObjectPoolScript.instance.GetProjectile(arrowType);
        arrow.transform.position = position;
        arrow.transform.rotation = rotation;

        ProjectileScript proj = arrow.GetComponent<ProjectileScript>();

        if (proj)
        {
            proj.Initialize(DecayTime,
                attackDamage * PlayerStatsScript.instance.GetStat(StatTypes.DAMAGE)
                + new Vector2(PlayerStatsScript.instance.GetStat(StatTypes.RANGEDDAMAGE),
                PlayerStatsScript.instance.GetStat(StatTypes.RANGEDDAMAGE))
                , Speed * PlayerStatsScript.instance.GetStat(StatTypes.RANGEDPROJSPEED), arrowType);
        }
    }

}
