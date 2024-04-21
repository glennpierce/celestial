using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    // Static reference to the GameManager instance
    private static GameManager _instance;

    // Property to access the GameManager instance
    public static GameManager Instance
    {
        get
        {
            // If no instance exists, find or create one
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                // If still no instance exists, create a new GameObject and add the GameManager component
                if (_instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    _instance = obj.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    // Your game management functionality goes here
    // For example:
    // public int score;

    // Optional: Ensure that the GameManager persists across scene changes
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Reference to the camera
    public CinemachineFreeLook mainCharacterCamera;
}
