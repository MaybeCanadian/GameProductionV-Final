using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Player Base Stats", menuName = "Players/Stats")]
public class PlayerBaseStats : ScriptableObject
{
    [Header("Base Stats")]
    [Tooltip("The max starting max health of the player")]
    public float maxHealth;
    [Tooltip("A percent increase in all damage the player does")]
    public float damageMultiplier;
    [Tooltip("How fast the player moves, a multiplier")]
    public float moveSpeedMultiplier;
    [Tooltip("How fast the player gets xp, a multiplier")]
    public float xpGainMultiplier;
    [Header("Ranged")]
    [Tooltip("How many projectiles the player makes when make projectiles")]
    public float projectileAmount;
    [Tooltip("How many times projectiles pierce")]
    public float pierceAmount;
    [Tooltip("A flat increase to damage on ranged attacks")]
    public float rangedDamage;
    [Tooltip("How fast the player can attack, a multiplier")]
    public float rangedAttackSpeedMultiplier;
    [Tooltip("How fast the player projectiles move, a multiplier")]
    public float rangedProjectileSpeedMultiplier;
    [Header("Melee")]
    [Tooltip("How far melee attacks hit from the player, a multiplier")]
    public float meleeAttackRangeMultiplier;
    [Tooltip("How large of an attack area the player hits, a multiplier")]
    public float meleeAttackRangeArcMulitiplier;
    [Tooltip("flat increase to damage of melee attacks")]
    public float meleeDamage;
    [Tooltip("How fast the player attacks with melee")]
    public float meleeAttackSpeedMultiplier;
}
