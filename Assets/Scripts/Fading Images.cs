using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    public enum FadeState { DELAY, FADEIN, DISPLAY, FADEOUT, COMPLETE }

    public float fadeSpeed = 1.0f;
    public float startDelay = 1.0f; // Delay in seconds before fading starts
    public float fadeInDuration = 1.0f; // Duration in seconds for fade in
    public float displayDuration = 3.0f; // Duration in seconds to display the image
    public float fadeOutDuration = 1.0f; // Duration in seconds for fade out
    private Image image;
    private float delayTimer;
    private float fadeInTimer;
    private float displayTimer;
    private float fadeOutTimer;
    private FadeState currentState;

    void Start()
    {
        image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, 0f); // Start with image hidden
        currentState = FadeState.DELAY;
        delayTimer = startDelay;
    }

    void Update()
    {
        UpdateState();
        UpdateImageAlpha();
    }

    void UpdateState()
    {
        switch (currentState)
        {
            case FadeState.DELAY:
                delayTimer -= Time.deltaTime;
                if (delayTimer <= 0)
                {
                    currentState = FadeState.FADEIN;
                    fadeInTimer = fadeInDuration;
                }
                break;
            case FadeState.FADEIN:
                fadeInTimer -= Time.deltaTime;
                if (fadeInTimer <= 0)
                {
                    currentState = FadeState.DISPLAY;
                    displayTimer = displayDuration;
                }
                break;
            case FadeState.DISPLAY:
                displayTimer -= Time.deltaTime;
                Debug.Log("Display Timer: " + displayTimer);
                if (displayTimer <= 0.01f) // Adding a small buffer value for precision
                {
                    Debug.Log("Transitioning to FADEOUT");
                    currentState = FadeState.FADEOUT;
                    fadeOutTimer = fadeOutDuration;
                }
                break;
            case FadeState.FADEOUT:
                fadeOutTimer -= Time.deltaTime;
                if (fadeOutTimer <= 0)
                {
                    currentState = FadeState.COMPLETE;
                    // gameObject.SetActive(false); // Disable the GameObject after fading out
                }
                break;
        }
    }

    void UpdateImageAlpha()
    {
        switch (currentState)
        {
            case FadeState.FADEIN:
                //Debug.Log("FADEIN");
                float fadeInAlpha = 1f - (fadeInTimer / fadeInDuration);
                image.color = new Color(image.color.r, image.color.g, image.color.b, fadeInAlpha);
                break;
            case FadeState.DISPLAY:
                //Debug.Log("DISPLAY");
                // No need to update alpha during display state
                break;
            case FadeState.FADEOUT:
                Debug.Log("FADEOUT");
                float fadeOutAlpha = fadeOutTimer / fadeOutDuration;
                image.color = new Color(image.color.r, image.color.g, image.color.b, fadeOutAlpha);
                break;
        }
    }
}
