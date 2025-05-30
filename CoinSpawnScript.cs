using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the spawning of collectible coins throughout the level
/// </summary>
public class CoinSpawnScript : MonoBehaviour
{
    // Spawn settings
    public GameObject coinPrefab;                // Coin prefab to spawn
    public List<Transform> spawnPoints;          // Potential spawn locations
    public int numberOfCoinsToSpawn = 100;       // Number of coins to spawn per round
    
    // Tracking of active coins
    private List<GameObject> currentCoins = new List<GameObject>(); // Currently active coins

    /// <summary>
    /// Spawn coins when the scene starts
    /// </summary>
    void Start()
    {
        SpawnCoins();
    }

    /// <summary>
    /// Spawns coins at random spawn points
    /// </summary>
    public void SpawnCoins()
    {
        // Clean up any existing coins
        foreach (GameObject coin in currentCoins)
        {
            Destroy(coin);
        }
        currentCoins.Clear();

        // Randomize spawn points order
        List<Transform> shuffledPoints = new List<Transform>(spawnPoints);
        Shuffle(shuffledPoints);
        
        // Fixed height for all coins
        float fixedY = 0.15f;

        // Consistent rotation for all coins
        Quaternion coinRotation = Quaternion.Euler(90f, -443.304f, 994.083f);

        // Spawn coins at selected points
        for (int i = 0; i < numberOfCoinsToSpawn; i++)
        {
            // Get position from spawn point
            Vector3 spawnPos = shuffledPoints[i].position;
            spawnPos.y = fixedY;  // Use consistent height
            
            // Create the coin
            GameObject newCoin = Instantiate(coinPrefab, shuffledPoints[i].position, coinPrefab.transform.rotation);
            
            // Set final position with correct height
            newCoin.transform.position = new Vector3(spawnPos.x, fixedY, spawnPos.z);
            
            // Configure the coin's collider
            Collider col = newCoin.GetComponent<Collider>();
            if (col != null)
            {
                col.isTrigger = true;  // Set to trigger for collection

                // If using capsule collider, adjust its properties
                if (col is CapsuleCollider capsule)
                {
                    capsule.radius = 1.5f;   // Width
                    capsule.height = 2.0f;   // Height
                    capsule.direction = 1;   // Y-axis orientation
                }
            }

            // Track the coin
            currentCoins.Add(newCoin);
        }
    }

    /// <summary>
    /// Randomize a list using Fisher-Yates shuffle algorithm
    /// </summary>
    /// <param name="list">List to be shuffled</param>
    void Shuffle(List<Transform> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            // Get random index
            int randomIndex = Random.Range(i, list.Count);
            
            // Swap elements
            Transform temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
