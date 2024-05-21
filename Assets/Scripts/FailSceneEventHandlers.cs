using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FailSceneEventHandlers : MonoBehaviour
{
    public Button restartButton; // You can drag and drop your button in the inspector
    public Button quitButton;

    void Start()
    {
        restartButton.onClick.AddListener(OnRestartButtonPressed);
        quitButton.onClick.AddListener(OnQuitButtonPressed);
    }

    public void RestartGame()
    {
        // Reset any static variables or singletons here if needed
        ResetGameState();

        // Reload the initial scene
        SceneManager.LoadScene("Title");
    }

    void ResetGameState()
    {
        // Example of resetting a singleton instance
        if (InventoryManager.instance != null)
        {
            InventoryManager.instance.ResetInventory();
        }

        if (OptionsManager.instance != null)
        {
            OptionsManager.instance.ResetOptions();
        }

        // Reset any static variables
        // GameState.Reset();
    }

    void OnRestartButtonPressed()
    {
        RestartGame();
    }

    void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
