using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class RollingCredits : MonoBehaviour
{
    public float scrollSpeed = 30f;
    public float creditsDuration = 50f; // Adjust this value based on the duration of your credits
    private RectTransform rectTransform;
    private bool isCreditsFinished = false;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        StartCoroutine(RollCredits());
    }

    IEnumerator RollCredits()
    {
        float elapsedTime = 0f;
        while (elapsedTime < creditsDuration)
        {
            rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Credits finished rolling
        isCreditsFinished = true;
    }

    void Update()
    {
        if (isCreditsFinished && Input.anyKeyDown) // Change this condition if you want to handle a different input to proceed
        {
            // Load the next scene here
            SceneManager.LoadScene("Title");
        }
    }
}

       