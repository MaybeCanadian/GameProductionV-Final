using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Staff", menuName = "Equipment/Weapons/Staffs")]
public class StaffWeaponItems : WeaponItems
{
    [Tooltip("How fast the projectile moves")]
    public float projectileSpeed;
    [Header("Explosion Radius")]
    public float explosionRadius;
    public override void attack(Transform currentPosition)
    {


        //Vector3 vectorSpawnOffset = currentPosition.forward * 0.5f;
        //Vector3 SpawnPosition = currentPosition.position;
        //SpawnPosition += vectorSpawnOffset;
        //SpawnPosition = new Vector3(SpawnPosition.x, SpawnPosition.y + 2.0f, SpawnPosition.z);

        //Quaternion spawnRotation = new Quaternion(currentPosition.transform.rotation.x + Mathf.Deg2Rad * 5.0f, 
        //    currentPosition.transform.rotation.y, currentPosition.transform.rotation.z, 
        //    currentPosition.transform.rotation.w);
    }
}
