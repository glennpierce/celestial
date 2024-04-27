using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;


public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Scrollbar healthScrollbar;
    public float initialIncrementDelay = 1f;
    public float incrementInterval = 1f;
    public float incrementAmount = 1.0f;

    private void Start()
    {
        currentHealth = maxHealth;
        // InvokeRepeating("IncrementSliderValue", initialIncrementDelay, incrementInterval);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        
        float normalisedCurrentHealth = ConvertToNormalizedValue(currentHealth);

        Debug.Log("Player Health scrollar: " + normalisedCurrentHealth);

        // healthScrollbar.value = normalisedCurrentHealth;
        // Update the fill amount of the health scrollbar
        healthScrollbar.GetComponent<Image>().fillAmount = normalisedCurrentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle enemy death (e.g., destroy enemy GameObject)
        Destroy(gameObject);
    }

    private float ConvertToNormalizedValue(float value)
    {
        // Clamp the value between 0 and 100 to ensure it stays within the desired range
        value = Mathf.Clamp(value, 0f, 100f);

        // Convert the value to a normalized range between 0 and 1
        float normalizedValue = value / 100f;

        return normalizedValue;
    }

    // private void IncrementSliderValue()
    // {
    //     healthSlider.value -= incrementAmount;

    //     Console.WriteLine(healthSlider.value);

    //     // Ensure the slider value doesn't exceed its maximum value
    //     if (healthSlider.value <= 0.0f)
    //     {
    //         healthSlider.value = 0.0f;
    //         CancelInvoke("IncrementSliderValue");

    //         // Load and show the scene by name
    //         SceneManager.LoadScene("Fail");
    //     }
    // }
}