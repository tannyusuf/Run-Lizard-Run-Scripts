using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the UI elements and logic for character upgrades
/// </summary>
public class UpgradeUI : MonoBehaviour
{
    // UI references
    public GameObject upgradePanel;    // Main panel containing upgrade options
    public Button card1Button;         // Button for damage upgrade
    public Button card2Button;         // Button for health upgrade
    public GameObject healthBar;       // Reference to player health UI
    public GameObject statsBar;        // Reference to player stats UI

    // Game balance properties
    public int upgradeCost = 20;       // Cost in coins for each upgrade

    // Player reference
    private IguanaCharacter player;    // Reference to player character

    /// <summary>
    /// Initialize UI elements and find player reference
    /// </summary>
    private void Start()
    {
        // Hide upgrade panel at start
        upgradePanel.SetActive(false);

        // Find the player object in scene
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        player = playerObj.GetComponent<IguanaCharacter>();

        // Find UI elements if not already assigned
        if (healthBar == null)
            healthBar = GameObject.Find("HealthBar"); // Must match object name in scene

        if (statsBar == null)
            statsBar = GameObject.Find("StatsDisplay"); // Must match object name in scene

        // Add click listeners to upgrade buttons
        card1Button.onClick.AddListener(() => ApplyUpgrade(1)); // Damage upgrade
        card2Button.onClick.AddListener(() => ApplyUpgrade(2)); // Health upgrade
    }

    /// <summary>
    /// Display upgrade options panel and hide other UI elements
    /// </summary>
    public void ShowUpgradeOptions()
    {
        upgradePanel.SetActive(true);
        healthBar.SetActive(false);
        statsBar.SetActive(false);
        Debug.Log("Upgrade Panel Açılıyor!");
    }

    /// <summary>
    /// Apply the selected upgrade if player has enough coins
    /// </summary>
    /// <param name="index">1 for damage upgrade, 2 for health upgrade</param>
    private void ApplyUpgrade(int index)
    {
        // Check if player has enough coins
        if (player.cointCount < upgradeCost)
        {
            Debug.Log("Yeterli coin yok!");
            return;
        }

        // Deduct cost
        player.cointCount -= upgradeCost;

        // Apply the appropriate upgrade based on index
        if (index == 1)
        {
            // Damage upgrade (20% increase)
            float increase = player.damageAmount * 0.2f;
            player.damageAmount += increase;
            Debug.Log("Damage upgraded to: " + player.damageAmount);
        }
        else if (index == 2)
        {
            // Health upgrade (20% increase)
            player.healthSystem.IncreaseMaxHealth(0.2f);
            Debug.Log("Health upgraded to: " + player.healthSystem.GetMaxHealth());
        }

        // Hide the panel and resume game
        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    /// <summary>
    /// Hide upgrade panel and restore other UI elements
    /// </summary>
    public void HideUpgradeOptions()
    {
        upgradePanel.SetActive(false);
        healthBar.SetActive(true);
        statsBar.SetActive(true);
        Time.timeScale = 1f;
    }
}
