using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    public static PlayerStatsScript instance;
    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
            

            SetUpDictionary();
            return;
        }
    }

    public delegate void OnStatChanged(StatTypes stat, float amount);
    public static OnStatChanged onStatChanged;

    public PlayerBaseStats baseStats;

    public Dictionary<StatTypes, float> statTypeDict = new Dictionary<StatTypes, float>();

    private void SetUpDictionary()
    {
        statTypeDict.Add(StatTypes.MAXHEALTH, baseStats.maxHealth);
        statTypeDict.Add(StatTypes.MOVESPEED, baseStats.moveSpeedMultiplier);
        statTypeDict.Add(StatTypes.DAMAGE, baseStats.damageMultiplier);
        statTypeDict.Add(StatTypes.XP, baseStats.xpGainMultiplier);
        statTypeDict.Add(StatTypes.PROJECTILE, baseStats.projectileAmount);
        statTypeDict.Add(StatTypes.PIERCE, baseStats.pierceAmount);
        statTypeDict.Add(StatTypes.RANGEDDAMAGE, baseStats.rangedDamage);
        statTypeDict.Add(StatTypes.RANGEDSPEED, baseStats.rangedAttackSpeedMultiplier);
        statTypeDict.Add(StatTypes.MELEEDAMAGE, baseStats.meleeDamage);
        statTypeDict.Add(StatTypes.MELEERANGE, baseStats.meleeAttackRangeMultiplier);
        statTypeDict.Add(StatTypes.MELEEARC, baseStats.meleeAttackRangeArcMulitiplier);
        statTypeDict.Add(StatTypes.MELEESPEED, baseStats.meleeAttackSpeedMultiplier);
        statTypeDict.Add(StatTypes.RANGEDPROJSPEED, baseStats.rangedProjectileSpeedMultiplier);
        statTypeDict.Add(StatTypes.CURRENTHEALTH, baseStats.maxHealth);
    }
    public void ModifyStat(StatTypes stat, float amount, StatChangeMethod method)
    {
        if (method == StatChangeMethod.ADDITIVE)
        {
            onStatChanged?.Invoke(stat, amount);
            statTypeDict[stat] += amount;
            return;
        }

        statTypeDict[stat] *= 1.0f + (amount / 100.0f); //so it inceases by the percent given, ie 0.3 is 30% -0.3 is -30%
        onStatChanged?.Invoke(stat, statTypeDict[stat] * amount);
        return;
    }

    public float GetStat(StatTypes stat)
    {
        return statTypeDict[stat];
    } 
}

[System.Serializable]
public enum StatTypes
{
    MAXHEALTH,
    CURRENTHEALTH,
    MOVESPEED,
    DAMAGE,
    XP,
    PROJECTILE,
    PIERCE,
    RANGEDDAMAGE,
    RANGEDSPEED,
    RANGEDPROJSPEED,
    MELEEDAMAGE,
    MELEERANGE,
    MELEEARC,
    MELEESPEED
}

public enum StatChangeMethod
{
    ADDITIVE,
    MULTIPLY
}
