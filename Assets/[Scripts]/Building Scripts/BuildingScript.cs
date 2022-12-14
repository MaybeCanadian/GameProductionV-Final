using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;
    public HealthBarScript healthBar;
    public EffectList hitEffect;
    public EffectList destroyEffect;
    public AudioSource buildingAudio;
    protected void Awake()
    {
        buildingAudio = GetComponent<AudioSource>();
    }
    public virtual void Activate() { }
    protected void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateBar(1.0f);
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateBar(currentHealth / maxHealth);
        buildingAudio.PlayOneShot(SoundManager.instance.GetFXClip(hitEffect));

        if(currentHealth <= 0)
        {
            DestoryBuilding();
        }
    } 
    private void DestoryBuilding()
    {
        buildingAudio.PlayOneShot(SoundManager.instance.GetFXClip(destroyEffect));
        Destroy(gameObject);
    }
}
