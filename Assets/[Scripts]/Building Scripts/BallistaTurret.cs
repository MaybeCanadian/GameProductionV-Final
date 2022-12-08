using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallistaTurret : BuildingScript
{
    public Transform target;
    public GameObject upperPortion;

    private bool OnCoolDown = true;

    public float FireRate = 2.0f;

    [Min(0.0f)]
    public float FireSpeed = 10.0f;

    public float rotationSpeed = 1.0f;

    public float spawnOffset;
    public float DetectionRadius = 1.0f;

    public LayerMask TargetingLayermask;

    public bool Activated = false;

    public ProjectileTypes arrowType;

    public override void Activate()
    {
        Activated = true;
        Invoke("ResetCoolDown", 1.0f / FireSpeed);
    }

    private new void Start()
    {
        base.Start();
        OnCoolDown = true;
        Activated = false;
    }

    private void Update()
    {
        if (Activated)
        {
            if (target)
            {
                if ((transform.position - target.position).magnitude > DetectionRadius)
                {
                    target = null;
                }
                else
                {
                    Vector3 forward = target.position - upperPortion.transform.position;
                    forward = new Vector3(forward.x, 0, forward.z);
                    Quaternion TargetRotation = Quaternion.LookRotation(forward, transform.up);
                    //upperPortion.transform.LookAt(target);
                    upperPortion.transform.rotation = Quaternion.Lerp(upperPortion.transform.rotation, TargetRotation, rotationSpeed);

                    if (!OnCoolDown)
                    {
                        FireProjectile();
                        OnCoolDown = true;
                        Invoke("ResetCoolDown", FireRate);
                    }
                }
            }
            else
            {
                FindTarget();
            }
        }
    }

    private void FindTarget()
    {
        Collider[] colliders =  Physics.OverlapSphere(transform.position, DetectionRadius, TargetingLayermask);

        float closest = DetectionRadius;

        foreach(Collider hit in colliders)
        {
            if((transform.position - hit.transform.position).magnitude < closest)
            {
                closest = (transform.position - hit.transform.position).magnitude;
                target = hit.transform;
            }
        }
    }

    private void ResetCoolDown()
    {
        Debug.Log("test");
        OnCoolDown = false;
    }


    private void FireProjectile() 
    {
        Vector3 vectorSpawnOffset = upperPortion.transform.forward * spawnOffset;
        Vector3 SpawnPosition = upperPortion.transform.position;
        SpawnPosition += vectorSpawnOffset;
        GameObject arrow = ObjectPoolScript.instance.GetProjectile(arrowType);
        arrow.transform.position = SpawnPosition;
        arrow.transform.rotation = upperPortion.transform.rotation;

        BallistaArrow arrowScript = arrow.GetComponent<BallistaArrow>();

        if(arrowScript)
        {
            arrowScript.Initialize(10.0f, new Vector2(10.0f, 20.0f), new Vector2(0.0f, 0.0f), FireSpeed);
        }
        else
        {
            Debug.LogError("issue with arrows");
        }
    }
}
