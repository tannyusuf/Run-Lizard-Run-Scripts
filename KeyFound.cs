using SojaExiles;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyFound : MonoBehaviour
{
    public enum KeyType { DoorKey, GarageKey, GardenKey}
    public KeyType keyType;

    public opencloseDoor door;

    private void OnMouseDown()
    {
        if (keyType == KeyType.DoorKey)
        {
            door.hasKey = true;
            Debug.Log("Kap� anahtar� al�nd�!");
        }
        else if (keyType == KeyType.GarageKey)
        {
            door.hasGarageKey = true;
            Debug.Log("Garaj anahtar� al�nd�!");
        }
        else if (keyType == KeyType.GardenKey)
        {
            door.hasGardenKey = true;
            Debug.Log("Bah�e anahtar� al�nd�!");
        }
        AudioManager.instance.PlaySFX(AudioManager.instance.keyFound);

        Destroy(gameObject); // Anahtar� yok et
    }
}
