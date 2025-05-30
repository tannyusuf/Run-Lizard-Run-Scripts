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
            Debug.Log("Kapý anahtarý alýndý!");
        }
        else if (keyType == KeyType.GarageKey)
        {
            door.hasGarageKey = true;
            Debug.Log("Garaj anahtarý alýndý!");
        }
        else if (keyType == KeyType.GardenKey)
        {
            door.hasGardenKey = true;
            Debug.Log("Bahçe anahtarý alýndý!");
        }
        AudioManager.instance.PlaySFX(AudioManager.instance.keyFound);

        Destroy(gameObject); // Anahtarý yok et
    }
}
