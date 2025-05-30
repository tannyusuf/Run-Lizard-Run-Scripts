using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles coin collection when player touches a coin
/// </summary>
public class Coin : MonoBehaviour
{
    // Empty Start and Update methods can be removed if not used
    void Start()
    {
        // Initialization can be added here if needed
    }

    void Update()
    {
        // Any coin behavior updates would go here
    }

    /// <summary>
    /// Detects when player enters the coin's trigger collider
    /// </summary>
    /// <param name="other">The collider that entered the trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        IguanaCharacter iguana = other.GetComponent<IguanaCharacter>();
        
        // If it's the player, process the coin collection
        if (iguana != null)
        {
            // Increment the player's coin count
            iguana.cointCount++;
            
            // Log the coin collection (for debugging)
            Debug.Log($"coin added {iguana.cointCount}");
            
            // Play coin collection sound effect
            AudioManager.instance.PlaySFX(AudioManager.instance.coinSound);
            
            // Remove the coin from the game
            Destroy(gameObject);
        }
    }
}
