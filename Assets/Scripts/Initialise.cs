using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class SliderInitializer : MonoBehaviour
{
    public Slider healthSlider;
    public float initialIncrementDelay = 1f;
    public float incrementInterval = 1f;
    public float incrementAmount = 1.0f;

    private void Start()
    {
        Console.WriteLine("Hello");
        InvokeRepeating("IncrementSliderValue", initialIncrementDelay, incrementInterval);
    }

    private void IncrementSliderValue()
    {
        healthSlider.value -= incrementAmount;

        Console.WriteLine(healthSlider.value);

        // Ensure the slider value doesn't exceed its maximum value
        if (healthSlider.value <= 0.0f)
        {
            healthSlider.value = 0.0f;
            CancelInvoke("IncrementSliderValue");

            // Load and show the scene by name
            SceneManager.LoadScene("Fail");
        }
    }
}
