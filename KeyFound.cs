using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles the key pickup functionality
/// </summary>
public class KeyFound : MonoBehaviour
{
    // Define different key types
    public enum KeyType { DoorKey, GarageKey, GardenKey}
    
    // Current key type of this object
    public KeyType keyType;
    
    // Reference to the door that will use this key
    public opencloseDoor door;

    /// <summary>
    /// Handle mouse click on the key object
    /// </summary>
    private void OnMouseDown()
    {
        // Grant the appropriate key based on type
        if (keyType == KeyType.DoorKey)
        {
            door.hasKey = true;
            Debug.Log("Kapı anahtarı alındı!");
        }
        else if (keyType == KeyType.GarageKey)
        {
            door.hasGarageKey = true;
            Debug.Log("Garaj anahtarı alındı!");
        }
        else if (keyType == KeyType.GardenKey)
        {
            door.hasGardenKey = true;
            Debug.Log("Bahçe anahtarı alındı!");
        }
        
        // Play key found sound effect
        AudioManager.instance.PlaySFX(AudioManager.instance.keyFound);

        // Remove key from the scene
        Destroy(gameObject); // Remove the key object
    }
}
