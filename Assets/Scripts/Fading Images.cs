using UnityEngine;
using UnityEngine.UI;

public class ImageFader : MonoBehaviour
{
    public float fadeSpeed = 1.0f;
    private Image image;
    private bool fadeIn = true;

    void Start()
    {
        image = GetComponent<Image>();
    }

    void Update()
    {
        if (fadeIn)
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(image.color.a, 1f, fadeSpeed * Time.deltaTime));
            if (image.color.a >= 0.99f)
                fadeIn = false;
        }
        else
        {
            image.color = new Color(image.color.r, image.color.g, image.color.b, Mathf.Lerp(image.color.a, 0f, fadeSpeed * Time.deltaTime));
            if (image.color.a <= 0.01f)
                fadeIn = true;
        }
    }
}




