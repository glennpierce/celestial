using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    public GameObject deathParticlesPrefab;
    public int maxHealth = 100;
    private int currentHealth;
    [SerializeField] public Slider healthSlider;
    public float initialIncrementDelay = 1f;
    public float incrementInterval = 1f;
    public float incrementAmount = 1.0f;
    public float delayBeforeDestroy = 2.0f; // Delay in seconds before destroying the enemy game object

    private void Start()
    {
        currentHealth = maxHealth;
        //InvokeRepeating("IncrementSliderValue", initialIncrementDelay, incrementInterval);
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        float normalisedCurrentHealth = ConvertToNormalizedValue(currentHealth);

        healthSlider.value = normalisedCurrentHealth;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // private void Die()
    // {
    //     // Handle enemy death (e.g., destroy enemy GameObject)
    //     Destroy(gameObject);
    // }


    // public void Die()
    // {
    //     // Check if the particle system prefab is assigned
    //     if (deathParticlesPrefab != null)
    //     {
    //         // Instantiate the particle system at the enemy's position
    //         Debug.Log("DYING");

    //         GameObject particlesInstance = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
            
    //         // Optionally, parent the particle system instance to the same parent as the enemy
    //         particlesInstance.transform.parent = transform.parent;
            
    //         // Destroy the particle system after its duration (if needed)
    //         Destroy(particlesInstance, particlesInstance.GetComponent<ParticleSystem>().main.duration);
    //     }

    //     // Handle other death-related logic (e.g., play death animation, deactivate GameObject, etc.)
    //     Destroy(gameObject);
    // }

    private float ConvertToNormalizedValue(float value)
    {
        // Clamp the value between 0 and 100 to ensure it stays within the desired range
        value = Mathf.Clamp(value, 0f, 100f);

        // Convert the value to a normalized range between 0 and 1
        float normalizedValue = value / 100f;

        return normalizedValue;
    }

    public void Die()
    {
        if (deathParticlesPrefab != null)
        {
            GameObject particlesInstance = Instantiate(deathParticlesPrefab, transform.position, Quaternion.identity);
            particlesInstance.transform.parent = transform.parent;

            // Determine the duration of the particle system
            float particleSystemDuration = particlesInstance.GetComponent<ParticleSystem>().main.duration;

            // Start a coroutine to delay the destruction of the enemy game object
            StartCoroutine(DestroyAfterDelay(particleSystemDuration + delayBeforeDestroy));
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private IEnumerator DestroyAfterDelay(float delay)
    {
        // Wait for the specified delay
        yield return new WaitForSeconds(delay);

        // Destroy the enemy game object
        Destroy(gameObject);
    }
}