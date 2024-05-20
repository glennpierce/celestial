using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartSceneEventHandlers : MonoBehaviour
{
    public Button startButton; // You can drag and drop your button in the inspector

    void Start()
    {
        if (startButton == null)
            startButton = GetComponent<Button>(); // Try to get the button component attached to the same GameObject

        // Register a callback function for when the button is clicked
        if (startButton != null)
            startButton.onClick.AddListener(OnButtonPressed);
        else
            Debug.LogError("No button component found on the GameObject.");
    }

    void OnButtonPressed()
    {
        Debug.LogError("Button was pressed!");
        
        SceneManager.LoadScene("Chess Board Full");
    }

    void OnDestroy()
    {
        // Make sure to unregister the listener when the script is destroyed
        if (startButton != null)
            startButton.onClick.RemoveListener(OnButtonPressed);
    }
}
