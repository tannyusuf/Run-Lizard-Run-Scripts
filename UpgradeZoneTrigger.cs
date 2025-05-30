using UnityEngine;

/// <summary>
/// Controls the trigger zone for player upgrades.
/// When player enters this zone, upgrade UI appears.
/// </summary>
public class UpgradeZoneTrigger : MonoBehaviour
{
    // Reference to the UI controller that handles upgrade options
    public UpgradeUI upgradeUI;  // Assigned through Unity Inspector

    /// <summary>
    /// Called when a collider enters the trigger zone
    /// </summary>
    /// <param name="other">The collider that entered the trigger</param>
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering object is the player
        if (other.CompareTag("Player"))
        {
            // Show upgrade options UI when player enters
            upgradeUI.ShowUpgradeOptions(); // Display the upgrade cards
        }
    }

    /// <summary>
    /// Called when a collider exits the trigger zone
    /// </summary>
    /// <param name="other">The collider that exited the trigger</param>
    private void OnTriggerExit(Collider other)
    {
        // Check if the exiting object is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Oyuncu upgrade alanından çıktı.");
            // Hide the upgrade UI when player leaves the zone
            upgradeUI.HideUpgradeOptions(); // Close the panel
        }
    }
}

