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
        //Debug.LogError("pooooo" + other.tag);

        if (other.CompareTag("EnemyPawn") || other.CompareTag("EnemyLocation"))
        {
            EnemyAi enemy = other.GetComponentInParent<EnemyAi>();
           // Debug.LogError("EnemyAi" + enemy);
            enemy.CurrentChessSquareColour = this.selectedColour;
            //Debug.LogError("enemy.CurrentChessSquareColour:" + enemy.CurrentChessSquareColour);
        }
    }

}