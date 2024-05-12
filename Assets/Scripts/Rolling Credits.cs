using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingCredits : MonoBehaviour
{
    public float scrollSpeed = 30f; 
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        Debug.Log("HERE");
    }

    // Update is called once per frame
    void Update()
    {
        rectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
    }
}
