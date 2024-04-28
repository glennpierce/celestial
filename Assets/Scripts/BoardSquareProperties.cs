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
    public BoardSquareProperties.Colour selectedColour = BoardSquareProperties.Colour.Black;

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

        if (other.CompareTag("EnemyPawn"))
        {
            EnemyPawnAi enemy = other.GetComponent<EnemyPawnAi>();
            enemy.currentSquareColour = this.selectedColour;
            //Debug.Log("enemy.currentSquareColour setting: " + enemy.currentSquareColour);
            //Debug.Log("hello");
        }
    }

}