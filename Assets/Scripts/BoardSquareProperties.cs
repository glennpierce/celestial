using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSquareProperties : MonoBehaviour
{
    // Enum to represent colors
    public enum Colour
    {
        Black,
        White
    }

    // Public variable to store the selected color
    public Colour selectedColour;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("GAME: " + other);

        if (other.CompareTag("Enemy"))
        {
            // EnemyPawnAi enemy = other.GetComponent
            Debug.Log("hello");
        }
    }

}