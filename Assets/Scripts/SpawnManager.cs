using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab
    public float spawnInterval = 2f; // Interval between spawns
    public float minDistanceFromPlayer = 5f; // Minimum distance from the player
    public float maxDistanceFromPlayer = 10f; // Maximum distance from the player
    public LayerMask floorLayer; // Layer mask for the floor objects
    public float raycastDistance = 100f; // Maximum distance to cast ray for floor detection
    public int maxEnemies = 20; // Maximum number of enemies

    private Transform player; // Reference to the player's transform
    [SerializeField] public Slider healthSlider;

    private int currentEnemyCount = 0; // Current number of enemies

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Find the player GameObject and get its transform

        // Start spawning enemies
        StartCoroutine(FindPlayerAndSpawnEnemies());
    }

    IEnumerator FindPlayerAndSpawnEnemies()
    {
        // Wait until the player GameObject is found
        while (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
            yield return null; // Wait for the next frame
        }

        // while (player == null)
        // {
        //     player = GameObject.FindGameObjectWithTag("Player")?.transform;
        //     yield return null; // Wait for the next frame
        // }

        // Start spawning enemies
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        // Infinite loop to continuously spawn enemies
        while (true)
        {
            // Check if the current enemy count is less than the maximum limit
            if (currentEnemyCount < maxEnemies)
            {
                // Find a random point on the floor within the specified range from the player
                Vector3 spawnPoint = FindRandomPointOnFloor();

                // Instantiate the enemy prefab at the chosen spawn point
                GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint, Quaternion.identity);

                // Debug.Log("Spawn Player: " + player);

                //EnemyPawnAi enemyAI = newEnemy.GetComponent<EnemyPawnAi>();
                EnemyAi enemyAI = newEnemy.GetComponentInChildren<EnemyAi>();

                // Debug.Log("EnemyPawnAi: " + enemyAI);

                if (enemyAI != null)
                {
                    enemyAI.player = player;
                }

                EnemyHealth heath = newEnemy.GetComponentInChildren<EnemyHealth>();

                // Debug.Log("EnemyHealth: " + heath);

                if (heath != null)
                {
                    Debug.Log("Setting heath: " + heath + "  healthSlider: " + healthSlider);
                    heath.healthSlider = healthSlider;
                }

                // Increase the current enemy count
                currentEnemyCount++;
            }

            // Wait for the specified spawn interval before spawning the next enemy
            yield return new WaitForSeconds(spawnInterval);

            // Debug.Log("In Loop");
        }
    }

    Vector3 FindRandomPointOnFloor()
    {
        Vector3 randomPoint = Vector3.zero;

        // Generate random offsets within the specified range
        float offsetX = Random.Range(-maxDistanceFromPlayer, maxDistanceFromPlayer);
        float offsetZ = Random.Range(-maxDistanceFromPlayer, maxDistanceFromPlayer);

        // Calculate the random point relative to the player's position
        randomPoint = player.position + new Vector3(offsetX, -1.5f, offsetZ);

        return randomPoint;
    }

    // Vector3 FindRandomPointOnFloor()
    // {
    //     Vector3 randomPoint = Vector3.zero;

    //     RaycastHit hit;
    //     if (Physics.Raycast(player.position, Vector3.down, out hit, raycastDistance, floorLayer))
    //     {
    //         // Detected the floor, now adjust the height based on terrain unevenness
    //         randomPoint = hit.point;

    //         // Optionally, you can further adjust the height by casting another ray downwards
    //         RaycastHit terrainHit;
    //         if (Physics.Raycast(randomPoint + Vector3.up * 10f, Vector3.down, out terrainHit, 20f, floorLayer))
    //         {
    //             randomPoint = terrainHit.point;
    //         }
    //     }
    //     else
    //     {
    //         // Fallback to player's position if floor not detected
    //         randomPoint = player.position;
    //     }

    //     // Apply random offsets within the specified range
    //     float offsetX = Random.Range(-maxDistanceFromPlayer, maxDistanceFromPlayer);
    //     float offsetZ = Random.Range(-maxDistanceFromPlayer, maxDistanceFromPlayer);

    //     randomPoint += new Vector3(offsetX, 0f, offsetZ); // Only apply horizontal offset

    //     return randomPoint;
    // }

    // Vector3 FindRandomPointOnFloor()
    // {
    //     Vector3 randomPoint = Vector3.zero;

    //     // Calculate random point in the vicinity of the player
    //     float offsetX = Random.Range(-maxDistanceFromPlayer, maxDistanceFromPlayer);
    //     float offsetZ = Random.Range(-maxDistanceFromPlayer, maxDistanceFromPlayer);
    //     Vector3 potentialPoint = player.position + new Vector3(offsetX, 50, offsetZ); // Start from above

    //     RaycastHit hit;
    //     // Cast a ray downward from the calculated point to find the actual ground
    //     if (Physics.Raycast(potentialPoint, Vector3.down, out hit, 100, floorLayer))
    //     {
    //         randomPoint = hit.point;
    //     }
    //     else
    //     {
    //         Debug.DrawRay(potentialPoint, Vector3.down * 100, Color.red, 5.0f);
    //         Debug.Log("No ground found for spawning at " + potentialPoint);
    //         return player.position; // Fallback to player position
    //     }

    //     // Draw a debug line (visible in Scene view in Unity)
    //     Debug.DrawLine(potentialPoint, randomPoint, Color.green, 5.0f);

    //     return randomPoint;
    // }


    // Method to check if all enemies have been spawned
    public bool AllEnemiesSpawned()
    {
        return currentEnemyCount >= maxEnemies;
    }
}