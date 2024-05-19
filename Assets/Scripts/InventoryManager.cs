using UnityEngine;
using UnityEngine.UI;
using System.Linq;

// public class InventoryManager : MonoBehaviour
// {
  

//     // Reference to the Canvas component
//     private Canvas canvas;

//     void Start()
//     {
//         // Get the Canvas component attached to this GameObject
//         canvas = GetComponent<Canvas>();

//         Debug.Log("HERY:" + canvas);

//         // Disable the Canvas component
//         canvas.enabled = false;

      
//     }
// }


public class InventoryManager : MonoBehaviour
{
    // Static reference to the InventoryManager instance
    public static InventoryManager instance;
    private Canvas canvas;

    private void Awake()
    {
        bool created = false;
        
        // Check if an instance already exists
        if (instance == null)
        {
            // If not, set the instance to this
            instance = this;
            created = true;
        }
        else if (instance != this)
        {
            // If an instance already exists and it's not this, destroy this GameObject
            Destroy(gameObject);
            return;
        }

        // Get the Canvas component attached to this GameObject
        canvas = GetComponent<Canvas>();

        // Ensure the Canvas component exists
        if (canvas != null)
        {
            // Disable the Canvas component
            canvas.enabled = false;
        }
        else
        {
            // If the Canvas component is not found, log an error
            Debug.LogError("Canvas component not found on GameObject: " + gameObject.name);
        }

        if (created) {
            instance.SetPawnCountText(0);
            instance.SetNMCountText(0);
            instance.SetCoinCountText(0);
        }

        // Ensure that the InventoryManager GameObject persists between scenes
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {

    }

    void Update()
    {

        // Check each button on each controller
        // for (int i = 1; i <= 4; i++) // Assuming there are 4 controllers
        // {
        //     for (int j = 0; j < 20; j++) // Assuming there are 20 buttons on each controller
        //     {
        //         string buttonName = "joystick " + i + " button " + j;
        //         if (Input.GetKeyDown(buttonName))
        //         {
        //             Debug.Log("Controller " + i + " Button " + j + " pressed");
        //         }
        //     }
        // }

        // Check if the 'O' key is pressed
        if (Input.GetKeyDown(KeyCode.I) || Input.GetKeyDown("joystick button 3"))
        {
            // Enable the Canvas component
            canvas.enabled = !canvas.enabled;
        }
    }

    public void SetPawnCountText(int count) {
  
            // Access the Text component of the PawnCount entry in InventoryManager
        Text pawnCountText = this.canvas.GetComponentsInChildren<Text>(true)
                    .FirstOrDefault(text => text.name == "PawnCount");

        // Check if the Text component is found
        if (pawnCountText != null)
        {
            // Set the text of the PawnCount entry
            pawnCountText.text = count.ToString();
        }
        else
        {
            // Log an error if the Text component is not found
            Debug.LogError("PawnCount Text component not found on InventoryManager");
        }
    }

    public void SetNMCountText(int count) {
  
            // Access the Text component of the PawnCount entry in InventoryManager
        Text nmCountText = this.canvas.GetComponentsInChildren<Text>(true)
                    .FirstOrDefault(text => text.name == "NMCount");

        // Check if the Text component is found
        if (nmCountText != null)
        {
            // Set the text of the PawnCount entry
            nmCountText.text = count.ToString();
        }
        else
        {
            // Log an error if the Text component is not found
            Debug.LogError("NMCount Text component not found on InventoryManager");
        }
    }

    public void SetCoinCountText(int count) {
  
            // Access the Text component of the PawnCount entry in InventoryManager
        Text coinCountText = this.canvas.GetComponentsInChildren<Text>(true)
                    .FirstOrDefault(text => text.name == "CoinCount");

        // Check if the Text component is found
        if (coinCountText != null)
        {
            // Set the text of the PawnCount entry
            coinCountText.text = count.ToString();
        }
        else
        {
            // Log an error if the Text component is not found
            Debug.LogError("CoinCount Text component not found on InventoryManager");
        }
    }
}

