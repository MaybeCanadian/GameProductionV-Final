using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrapScript : BuildingScript
{
    public float activateCooldown = 2.0f;
    public bool onCooldown = false;
    public float triggerDelay = 0.5f;
    public bool triggering = false;
    public LayerMask triggeringMask;
    public Vector2 damage = new Vector2(45.0f, 60.0f);
    public EffectList activateSoundEffect;
    public GameObject Spikes;

    public int MaxUses = 10;
    public int Uses;

    public List<Collider> currentlyInTrap;
    private new void Start()
    {
        base.Start();
        currentlyInTrap = new List<Collider>();
        Uses = MaxUses;
        healthBar.UpdateBar(1.0f);
        Spikes.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyScript enemy = other.GetComponent<EnemyScript>();

        if(enemy != null)
        {
            if (triggering == false && onCooldown == false)
            {
                onCooldown = true;
                triggering = true;
                Invoke("TriggerTrap", triggerDelay);
            }
        }

        currentlyInTrap.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        currentlyInTrap.Remove(other);
    }

    private void TriggerTrap()
    {
        triggering = false;
        Spikes.SetActive(true);
        buildingAudio.PlayOneShot(SoundManager.instance.GetFXClip(activateSoundEffect));
        float trapDamage = Random.Range(damage.x, damage.y);

        foreach(Collider col in currentlyInTrap)
        {
            EnemyScript enemy = col.GetComponent<EnemyScript>();

            if(enemy)
            {
                enemy.TakeDamage(trapDamage);
            }
        }

        Invoke("ResetCooldown", activateCooldown);
    }

    private void ResetCooldown()
    {
        Spikes.SetActive(false);
        onCooldown = false;

        Uses--;
        healthBar.UpdateBar(Uses * 1.0f / MaxUses * 1.0f);

        if(Uses <= 0)
        {
            OutOfUses();
        }
    }

    private void OutOfUses()
    {
        Destroy(gameObject);
    }
}
