using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.AI;

public class EnemyScript : MonoBehaviour
{
    [Header("Default stats")]
    public EnemyBaseStats stats;
    public Rigidbody rb;

    [Header("Health stats")]
    public float currentHealth;
    public HealthBarScript healthBar;
    public bool IsDead = false;

    [Header("Enemy Animations")]
    public Animator enemyAnimations;
    public Collider capCollider;

    [Header("Enemy AI")]
    public NavMeshAgent navigation;
    public Collider currentTarget;
    public NavMeshObstacle obstacle;

    [Header("Enemy Attacking")]
    public bool IsStunned = false;
    public bool IsAttacking = false;


    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        IsStunned = false;
        currentTarget = null;
        IsAttacking = false;
        IsDead = false;
        if (stats.AITickSpeed <= 0) //stops crashing
        {
            gameObject.SetActive(false);
        }
        currentHealth = stats.MaxHealth;
        capCollider = GetComponent<CapsuleCollider>();
        enemyAnimations = GetComponent<Animator>();
        enemyAnimations.fireEvents = false;

        navigation = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();

        SetUpNavigation();

        StartCoroutine("AITick");
    }

    private void SetUpNavigation()
    {
        navigation.speed = stats.walkingSpeed;
        navigation.angularSpeed = stats.turnSpeed;
        navigation.acceleration = stats.acceleration;
        navigation.avoidancePriority = Random.Range(20, 50);
        navigation.enabled = true;
    }

    private IEnumerator AITick()
    {
        yield return new WaitForSeconds(Random.Range(0.0f, stats.AITickSpeed));

        while(true)
        {
            if (IsDead)
            {
                yield break;
            }

            if (currentTarget && (currentTarget.transform.position - transform.position).magnitude <= stats.attackRadius)
            {
                if (!IsStunned)
                {
                    navigation.enabled = false;
                    Attack();
                }
            }
            else
            {
                if (!IsStunned)
                {
                    navigation.enabled = true;
                    CheckForTargets();
                }
            }

            yield return new WaitForSeconds(stats.AITickSpeed);
        }
    }

    private void CheckForTargets()
    {
        if (IsDead)
            return;

        if (IsAttacking)
            return;

        if (currentTarget != null)
        {
            if ((currentTarget.transform.position - transform.position).magnitude > stats.targetRetentionRadius)
            {
                currentTarget = null;
                if(navigation.enabled)
                    navigation.isStopped = true;
                enemyAnimations.SetBool("Moving", false);
            }
        }

        if (currentTarget == null)
        {
            Collider[] targets = Physics.OverlapSphere(transform.position, stats.detectionRadius, stats.targetingLayer);

            if (targets.Length <= 0)
            {
                return;
            }

            currentTarget = targets[0];

            if(navigation.enabled)
                navigation.isStopped = false;
            enemyAnimations.SetBool("Moving", true);
        }

        if(navigation.isActiveAndEnabled && navigation)
            navigation.SetDestination(currentTarget.transform.position);
    }

    public float GetXp()
    {
        return stats.XPvalue;
    }

    private void Kill()
    {
        IsDead = true;
        DropResources();

        StopAllCoroutines();
        navigation.enabled = false;
        healthBar.gameObject.SetActive(false);

        enemyAnimations.SetTrigger("Death");

        StartCoroutine("DespawnTimer");
        
    }

    private IEnumerator DespawnTimer()
    {
        yield return null;
        navigation.enabled = false;
        capCollider.enabled = false;
        rb.useGravity = false;
        rb.isKinematic = true;


        yield return new WaitForSeconds(stats.despawnTime);

        ObjectPoolScript.instance.ReturnEnemy(gameObject, stats.type);
        StopAllCoroutines();

        yield break;
    }

    private void DropResources()
    {
        PlayerLeveling.instance.GainXP(stats.XPvalue);
        foreach (Drops item in stats.drops.Drops)
        {
            if (item.dropChance >= Random.Range(0.0f, 100.0f))
                PlayerInventory.Instance.AddItem(item.item, item.GetDropAmount());
        }
    }

    public void TakeDamage(float damage)
    {
        if (IsDead)
            return;

        currentHealth -= damage;

        healthBar.UpdateBar(currentHealth / stats.MaxHealth);

        StartCoroutine("Stun");

        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    private IEnumerator Stun()
    {
        IsStunned = true;
        if (navigation.enabled)
        {
            navigation.isStopped = true;
        }
        enemyAnimations.SetTrigger("Damaged");
        enemyAnimations.SetBool("Stunned", true);

        yield return new WaitForSeconds(stats.stunDuration);

        enemyAnimations.ResetTrigger("Damaged");
        enemyAnimations.SetBool("Stunned", false);

        if (navigation.enabled)
        {
            navigation.isStopped = false;
        }

        IsStunned = false;
            yield break;
    }

    private void Attack()
    {
        if (IsAttacking == false)
        {
            enemyAnimations.SetTrigger("Attack");
            IsAttacking = true;

            float damage = Random.Range(stats.attackDamage.x, stats.attackDamage.y);

            Collider[] collisions = Physics.OverlapSphere(transform.position, stats.attackRange, stats.attackingLayers);

            foreach (Collider col in collisions)
            {
                PlayerHealthController playerHealth = col.GetComponent<PlayerHealthController>();

                if (playerHealth)
                {
                    playerHealth.TakeDamage(damage);
                    continue;
                }

                BuildingScript building = col.GetComponent<BuildingScript>();

                if(building)
                {
                    building.TakeDamage(damage);
                    continue;
                }
            }

            Invoke("ResetAttackCoolDown", stats.attackCooldown);
        }
    }

    private void ResetAttackCoolDown()
    {
        IsAttacking = false;
    }
}
