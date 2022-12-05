using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStatsUIControllerScript : MonoBehaviour
{
    public GameObject statsUI;

    public TMP_Text MaxHealthValue;
    public TMP_Text CurrentHealth;
    public TMP_Text DamageMultiplier;
    public TMP_Text MoveSpeed;
    public TMP_Text RangedDamage;

    private void OnEnable()
    {
        PlayerStatsScript.onStatChanged += OnStatChanged;
    }
    private void OnDisable()
    {
        PlayerStatsScript.onStatChanged -= OnStatChanged;
    }

    public void OnStatsButtonPresed()
    {
        if (GameStateMachine.GetInstance().GetState() == GameStates.GAME)
        {
            if (statsUI.activeInHierarchy)
            {
                statsUI.SetActive(false);
            }
            else
            {
                statsUI.SetActive(true);
                UpdateStats();
            }
        }
    }

    public void UpdateStats()
    {
        MaxHealthValue.text = PlayerStatsScript.instance.GetStat(StatTypes.MAXHEALTH).ToString();
        DamageMultiplier.text = PlayerStatsScript.instance.GetStat(StatTypes.DAMAGE).ToString();
        MoveSpeed.text = PlayerStatsScript.instance.GetStat(StatTypes.MOVESPEED).ToString();
        RangedDamage.text = PlayerStatsScript.instance.GetStat(StatTypes.RANGEDDAMAGE).ToString();
        CurrentHealth.text = PlayerStatsScript.instance.GetStat(StatTypes.CURRENTHEALTH).ToString();
    }

    public void OnStatChanged(StatTypes stat, float amount)
    {
        UpdateStats();
    }
}
