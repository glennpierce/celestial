using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class OptionsManager : MonoBehaviour
{
    // Declare variables for UI elements
    public Button exitButton;
    public Button mainMenuButton;
    public Slider volumeSlider;
    public UnityEngine.UI.Dropdown resolutionDropdown;

    // Reference to the Canvas component
    private Canvas canvas;

    void Start()
    {
        // Get the Canvas component attached to this GameObject
        canvas = GetComponent<Canvas>();

        // Debug.Log("HERY:" + canvas);

        // Disable the Canvas component
        canvas.enabled = false;

        PopulateDropdown();

        // Add listeners to handle button click event
        exitButton.onClick.AddListener(OnExitButtonClick);

        mainMenuButton.onClick.AddListener(OnMainMenuButtonClick);

        // Add listener to handle slider value change event
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderValueChanged);

        // Add listener to handle dropdown value change event
        resolutionDropdown.onValueChanged.AddListener(OnResolutionChanged);

        if (volumeSlider != null)
        {
            // Set the slider's value to match the current volume level
            volumeSlider.value = AudioListener.volume;
        }
        else
        {
            Debug.LogWarning("Volume slider reference not set!");
        }
    }

    void Update()
    {

        // Check each button on each controller
        for (int i = 1; i <= 4; i++) // Assuming there are 4 controllers
        {
            for (int j = 0; j < 20; j++) // Assuming there are 20 buttons on each controller
            {
                string buttonName = "joystick " + i + " button " + j;
                if (Input.GetKeyDown(buttonName))
                {
                    Debug.Log("Controller " + i + " Button " + j + " pressed");
                }
            }
        }

        // Check if the 'O' key is pressed
        if (Input.GetKeyDown(KeyCode.O) || Input.GetKeyDown("joystick button 7"))
        {
            

            // Enable the Canvas component
            canvas.enabled = !canvas.enabled;
        }
    }

    // Method to handle button click event
    void OnExitButtonClick()
    {
        Debug.LogError("EXIT Button clicked!");
        // Exit the application
        Application.Quit();
    }

    void OnMainMenuButtonClick()
    {
       Debug.LogError("OnMainMenuButtonClick Button clicked!");
       SceneManager.LoadScene("Title");
    }

    // Method to handle slider value change event
    void OnVolumeSliderValueChanged(float value)
    {
        Debug.LogError("OnVolumeSliderValueChanged");
        AudioListener.volume = volumeSlider.value;
    }

    void PopulateDropdown()
    {
        // Clear existing options
        resolutionDropdown.ClearOptions();

        // Get available resolutions
        Resolution[] resolutions = Screen.resolutions;

        // List to store resolution options
        List<string> options = new List<string>();

        Debug.LogError("resolutions:" + resolutions);

        // Add each resolution option to the list
        foreach (var resolution in resolutions)
        {
            options.Add(resolution.width + "x" + resolution.height);
        }

        // Set options to the dropdown
        resolutionDropdown.AddOptions(options);

        // Set the default value to the current resolution
        resolutionDropdown.value = GetCurrentResolutionIndex(resolutions);
    }

    int GetCurrentResolutionIndex(Resolution[] resolutions)
    {
        // Get the current resolution
        Resolution currentResolution = Screen.currentResolution;

        // Find the index of the current resolution in the array
        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width == currentResolution.width && resolutions[i].height == currentResolution.height)
            {
                return i;
            }
        }

        // Return default value if current resolution not found
        return 0;
    }

    public void OnResolutionChanged(int index)
    {
        

        // Get selected resolution from dropdown options
        Resolution[] resolutions = Screen.resolutions;
        Resolution selectedResolution = resolutions[index];

        Debug.LogError("selectedResolution:" + selectedResolution);

        // Change the screen resolution
        // Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
        Screen.SetResolution(selectedResolution.width, selectedResolution.height, Screen.fullScreen);
    }
}
