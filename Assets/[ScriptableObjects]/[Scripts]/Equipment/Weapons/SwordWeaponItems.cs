using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Sword", menuName = "Equipment/Weapons/Swords")]
public class SwordWeaponItems : WeaponItems
{
    [Tooltip("How far from the player the attack hits, attacks hit in a cone")]
    public float attackRange = 2.0f;
    [Tooltip("At what angle from in front of the player does that attack hit.")]
    public float attackArc = 90.0f;

    public override void attack(Transform currentPosition)
    {
        Collider[] collisions = Physics.OverlapSphere(currentPosition.position, attackRange 
            * PlayerStatsScript.instance.GetStat(StatTypes.MELEERANGE), enemyMask);

        //Debug.Log(collisions.Length);
        foreach (Collider collision in collisions) //https://answers.unity.com/questions/498657/detect-colliders-in-an-arc.html
        {
            Vector3 vectorToCollider = collision.transform.position - currentPosition.position;
            float VectorDot = Vector3.Dot(vectorToCollider.normalized, currentPosition.forward);
            //in referance to the dot product, 0 is 180 arc, 1 is 0 arc, -1 is 360 arc
            //0.5 is 90arc
            //-(x/180 - 1)
            float targetDot = -1.0f * ((attackArc * PlayerStatsScript.instance.GetStat(StatTypes.MELEEARC) / 180) - 1);
            //Debug.Log(targetDot);
            if (VectorDot > targetDot)
            {
                //Debug.Log("We have hit " + collision.gameObject.name);
                HitSomething(collision);
            }
        }
    }

    public void HitSomething(Collider other)
    {
        float Damage = Random.Range(attackDamage.x, attackDamage.y);
        Damage *= PlayerStatsScript.instance.GetStat(StatTypes.DAMAGE);
        Damage += PlayerStatsScript.instance.GetStat(StatTypes.MELEEDAMAGE);

        EnemyScript tempEnemy = other.GetComponent<EnemyScript>();
        if (tempEnemy)
        {
            tempEnemy.TakeDamage(Damage);
            return;
        }

        ResourceScript tempResource = other.GetComponent<ResourceScript>();
        if (tempResource)
        {
            tempResource.Hit(Damage);
            return;
        }
    }

}
