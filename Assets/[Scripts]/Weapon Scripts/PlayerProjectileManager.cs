using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectileManager : MonoBehaviour
{
    public GameObject ArrowPrefab;
    public void MakeArrowFromPlayer(Vector3 pos, Quaternion rot, float speed, float DestroyTime, Vector2 AttackDamage, Vector2 HarvestDamage)
    {
        GameObject arrow = Instantiate(ArrowPrefab, pos, rot);

        BallistaArrow arrowScript = arrow.GetComponent<BallistaArrow>();

        if (arrowScript)
        {
            arrowScript.Initialize(DestroyTime, AttackDamage, HarvestDamage, speed);
        }
        else
        {
            Debug.LogError("issue with arrows");
        }
    }
}
