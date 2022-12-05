using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarScript : MonoBehaviour
{
    public Slider healthBar;
    public GameObject healthBarObject;

    private void Start()
    {
        //healthBar = healthBarObject.GetComponent<Slider>();
        healthBar.value = 100.0f;
        healthBarObject.SetActive(false);
    }

    public void UpdateBar(float percentage)
    {
        if (!healthBarObject.activeInHierarchy)
            healthBarObject.SetActive(true);

        healthBar.value = percentage;  
    }
}
