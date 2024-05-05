using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public string fadePanelName = "FadePanel"; // Name of the fade panel GameObject
    public float fadeDuration = 3f;
    public float delayBeforeFadeIn = 5f;

    private GameObject fadePanel;

    private void Start()
    {
        // Find the GameObject with PlayerHealth script
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();

        // Subscribe to the OnPlayerDeath event
        if (playerHealth != null)
        {
            playerHealth.OnPlayerDeath += HandlePlayerDeath;
        }
    }

    private void HandlePlayerDeath()
    {
        Debug.Log("SceneTransitionManager: Player DIED");

        // Delay the scene load
        Invoke("LoadFailSceneWithFadeIn", delayBeforeFadeIn);
    }

    private void LoadFailSceneWithFadeIn()
    {
        // Load the "CelestialFail" scene
        SceneManager.LoadScene("CelestialFail", LoadSceneMode.Additive);

        // Find the fade panel GameObject in the loaded scene
        fadePanel = GameObject.Find(fadePanelName);

        // Trigger the fade-in animation after a delay
        FadeInPanel();
    }

    private void FadeInPanel()
    {
        if (fadePanel == null)
        {
            Debug.LogWarning("Fade panel not found!");
            return;
        }

        // Ensure the fadePanel is active
        fadePanel.SetActive(true);

        // Start fading the panel in using a coroutine
        StartCoroutine(FadePanelCoroutine());
    }

    private IEnumerator FadePanelCoroutine()
    {
        Image panelImage = fadePanel.GetComponent<Image>();
        if (panelImage == null)
        {
            Debug.LogWarning("Fade panel doesn't contain an Image component!");
            yield break;
        }

        float elapsedTime = 0f;
        Color startColor = panelImage.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 0f);

        while (elapsedTime < fadeDuration)
        {
            panelImage.color = Color.Lerp(startColor, endColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure panel fully fades out
        panelImage.color = endColor;
    }
}
