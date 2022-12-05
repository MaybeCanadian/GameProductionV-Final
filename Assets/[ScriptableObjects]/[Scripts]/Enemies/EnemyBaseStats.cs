using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemies/Enemy")]
public class EnemyBaseStats : ScriptableObject
{
    [Tooltip("The name shown in any ui's")]
    public string EnemyName = "";
    [Tooltip("The base value given when the player kills the enemy"), Min(0.0f)]
    public float XPvalue = 0.0f;
    [Tooltip("The drop table for the enemy")]
    public ResourceDropTable drops;

    [Header("Health stats")]
    [Min(0.0f), Tooltip("Starting max health of the enemy")]
    public float MaxHealth = 0.0f;
    [Tooltip("How long attacks stun for")]
    public float stunDuration = 1.0f;

    [Header("Pathfinding settings")]
    public float walkingSpeed = 3.5f;
    public float turnSpeed = 120.0f;
    public float acceleration = 8;

    [Header("AI settings")]
    [Tooltip("From how far away should the AI be able to detect something")]
    public float detectionRadius;
    public float targetRetentionRadius;
    [Tooltip("Which layers should the ai be able to target to attack?")]
    public LayerMask targetingLayer;
    [Tooltip("How long should the ai wait between ai ticks?")]
    public float AITickSpeed = 0.1f;

    [Header("Lifecycle")]
    [Tooltip("How long after this enemy dies does it despawn?"), Min(0.0f)]
    public float despawnTime = 10.0f;

    [Header("Enemy Attacking")]
    [Tooltip("from how far away they will try attacking")]
    public float attackRadius = 1.0f;
    [Tooltip("from how far away they can hit")]
    public float attackRange = 0.5f;
    [Tooltip("What layers they will attack")]
    public LayerMask attackingLayers;
    [Tooltip("How much damage it does on attacks")]
    public Vector2 attackDamage = new Vector2(5.0f, 10.0f);
    [Tooltip("How long it waits between attacks")]
    public float attackCooldown = 1.0f;

}
