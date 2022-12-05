using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class ResourceScript : MonoBehaviour
{
    public ResourceNodes defaultValues;
    public float currentHealth;
    public GameObject normalModel;
    public GameObject depletedModel;

    public BoxCollider boxCollider;

    public bool IsDepleted = false;

    private void Start()
    {
        boxCollider = GetComponent<BoxCollider>();

        ResetResource();
    }

    public void Hit(float damage)
    {
        if (IsDepleted == false)
        {
            currentHealth -= damage;

            if (currentHealth <= 0)
            {
                Debug.Log("Oof");
                currentHealth = 0;
                DropResources();
                DepleteResource();
            }
        }
    }
    private void DropResources()
    {
        foreach(Drops item in defaultValues.drops.Drops)
        {
            if(item.dropChance > UnityEngine.Random.Range(0.0f, 100.0f))
                PlayerInventory.Instance.AddItem(item.item, item.GetDropAmount());
        }
    }

    private void DepleteResource()
    {
        IsDepleted = true;
        normalModel.SetActive(false);
        depletedModel.SetActive(true);
        if(defaultValues.DisableCollisionsWhenDepleted)
        {
            boxCollider.enabled = false;
        }

        Invoke("ResetResource", defaultValues.respawnTime);
    }

    private void ResetResource()
    {
        IsDepleted = false;
        normalModel.SetActive(true);
        depletedModel.SetActive(false);
        currentHealth = defaultValues.maxHealth;

        if(defaultValues.DisableCollisionsWhenDepleted)
        {
            boxCollider.enabled = true;
        }
    }
}
