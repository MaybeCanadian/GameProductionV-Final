using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{

    public float maxHealth;
    public float currentHealth;
    public HealthBarScript healthBar;
    public virtual void Activate() { }
    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.UpdateBar(1.0f);
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        healthBar.UpdateBar(currentHealth / maxHealth);

        if(currentHealth <= 0)
        {
            DestoryBuilding();
        }
    } 
    private void DestoryBuilding()
    {
        Destroy(gameObject);
    }
}
