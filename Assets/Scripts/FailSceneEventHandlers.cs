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

    void OnRestartButtonPressed()
    {
        SceneManager.LoadScene("Title");
    }

    void OnQuitButtonPressed()
    {
        Application.Quit();
    }
}
