using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealthController : MonoBehaviour
{
    public float currentHealth;
    [Tooltip("How long between getting hurt can you not get hurt")]
    public float ISeconds = 1.0f;
    public bool InISecconds = false;
    public bool IsDead = false;
    public EffectList playerHitSoundEffect;

    public HealthBarScript healthBar;

    public PlayerAnimationScript playerAnimation;

    public PlayerAudioScript playerAudio;

    private void Start()
    {
        IsDead = false;
        InISecconds = false;
        currentHealth = PlayerStatsScript.instance.GetStat(StatTypes.MAXHEALTH);
        healthBar.UpdateBar(currentHealth / PlayerStatsScript.instance.GetStat(StatTypes.MAXHEALTH));
        playerAudio = GetComponent<PlayerAudioScript>();
    }

    private void OnEnable()
    {
        PlayerStatsScript.onStatChanged += OnStatChanged;
    }

    private void OnDisable()
    {
        PlayerStatsScript.onStatChanged -= OnStatChanged;
    }

    public void OnStatChanged(StatTypes stat, float amount)
    {
        if (stat == StatTypes.MAXHEALTH)
        {
            currentHealth += amount;
            return;
        }
    }

    public void TakeDamage(float Damage)
    {
        if (!InISecconds)
        {
            currentHealth -= Damage;

            PlayerStatsScript.instance.ModifyStat(StatTypes.CURRENTHEALTH, -Damage, StatChangeMethod.ADDITIVE);

            healthBar.UpdateBar(currentHealth / PlayerStatsScript.instance.GetStat(StatTypes.MAXHEALTH));

            InISecconds = true;

            playerAudio.PlayHitSound();
            Invoke("ResetISecconds", ISeconds);

            if (currentHealth <= 0)
            {
                Kill();
            }
        }
    }

    private void ResetISecconds()
    {
        InISecconds = false;
    }

    private void Kill()
    {
        if (!IsDead)
        {
            playerAnimation.Die();
            IsDead = true;

            Invoke("GoToGameOver", 3.0f);

        }
    }

    private void GoToGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }
}
