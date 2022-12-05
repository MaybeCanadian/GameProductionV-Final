using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallistaArrow : MonoBehaviour
{
    public Rigidbody rb;
    private BoxCollider boxCollider;
    public float LifeTime = 10.0f;
    public Vector2 AttackDamage = new Vector2(10.0f, 20.0f);
    public Vector2 HarvestDamage = new Vector2(0.0f, 0.0f);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        boxCollider = GetComponent<BoxCollider>();
    }

    public void Initialize(float DestroyDelay, Vector2 damage, Vector2 harvest, float LaunchSpeed)
    {
        rb.velocity = transform.forward * LaunchSpeed;
        Invoke("DestroyAfterTime", LifeTime);
    }

    public void Initialize()
    {
        Invoke("DestroyAfterTime", LifeTime);
    }

    private void DestroyAfterTime()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Terrain")
        {
            EnemyScript tempEnemy = collision.gameObject.GetComponent<EnemyScript>();

            if(tempEnemy)
            {
                tempEnemy.TakeDamage(UnityEngine.Random.Range(AttackDamage.x, AttackDamage.y));
                Destroy(rb);
                Destroy(boxCollider);
                transform.SetParent(collision.gameObject.transform);
                transform.localPosition = new Vector3(0, transform.position.y, 0);
            }
        }
    }
}
