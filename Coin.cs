using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Coin : MonoBehaviour
{
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
        // Check if the other object has the IguanaCharacter component
        IguanaCharacter iguana = other.GetComponent<IguanaCharacter>();
        iguana.cointCount++;
        Debug.Log($"coin added {iguana.cointCount}");
        if (iguana != null)
        {
            // You can call a method on the IguanaCharacter if needed
            // e.g., iguana.CollectCoin();
            AudioManager.instance.PlaySFX(AudioManager.instance.coinSound);
            // Destroy the coin object
            Destroy(gameObject);
        }
    }
}
