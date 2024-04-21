using System;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public Slider volumeSlider;
    public Slider fovSlider;
    public Button volumeResetButton;

    // Start is called before the first frame update
    void Start()
    {
        // volumeSlider.value = AudioListener.volume;

        // Add a listener to the slider's value change event
        volumeSlider.onValueChanged.AddListener(delegate { ChangeVolume(); });

        fovSlider.onValueChanged.AddListener(delegate { ChangeFOV(); });

        volumeResetButton.onClick.AddListener(OnResetVolumeClicked);

        Debug.Log("Hello");
    }

    // Function to change the volume based on the slider value
    void ChangeVolume()
    {
        // Adjust the volume using AudioListener
        AudioListener.volume = volumeSlider.value;
    }

    void OnResetVolumeClicked()
    {
        Debug.Log("Hello");
        AudioListener.volume = 0.7f;
    }

    // Function to change the FOV based on the slider value
    void ChangeFOV()
    {
        Debug.Log("Camera: " + GameManager.Instance.mainCharacterCamera);
        // Adjust the FOV of the camera using the slider's value
        //GameManager.Instance.mainCharacterCamera.fieldOfView = fovSlider.value;
    }
}
