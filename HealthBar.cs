using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the player's health system and UI representation
/// </summary>
public class HealthBar : MonoBehaviour
{
    // Health parameters
    public float maxHealth = 100f;      // Maximum possible health
    public float currentHealth;         // Current health points
    
    // UI reference
    public Image healthBarFill;         // UI image for health bar fill

    /// <summary>
    /// Initialize health to maximum on start
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    /// <summary>
    /// Apply damage to the health system
    /// </summary>
    /// <param name="amount">Amount of damage to take</param>
    public void TakeDamage(float amount)
    {
        // Reduce health by damage amount
        currentHealth -= amount;
        
        // Ensure health stays within valid range
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        
        // Update the UI representation
        UpdateHealthUI();

        // Check for death
        if (currentHealth <= 0)
        {
            // Trigger death sequence on the attached character
            GetComponent<IguanaCharacter>().Death();
        }
    }

    /// <summary>
    /// Restore health to maximum
    /// </summary>
    public void RestoreFullHealth()
    {
        currentHealth = maxHealth;
        UpdateHealthUI();
    }

    /// <summary>
    /// Update the visual health bar to match current health
    /// </summary>
    void UpdateHealthUI()
    {
        if (healthBarFill != null)
            healthBarFill.fillAmount = currentHealth / maxHealth;
    }
    
    /// <summary>
    /// Increase maximum health by percentage and heal by the same amount
    /// </summary>
    /// <param name="percentage">Percentage to increase (0.2 = 20%)</param>
    public void IncreaseMaxHealth(float percentage)
    {
        // Calculate health to add
        float addedHealth = maxHealth * percentage;
        
        // Increase max health
        maxHealth += addedHealth;
        
        // Increase current health as well
        currentHealth += addedHealth;
        
        // Ensure health stays within valid range
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
        
        // Update the UI representation
        UpdateHealthUI();
    }

    /// <summary>
    /// Get the current maximum health value
    /// </summary>
    /// <returns>Current maximum health</returns>
    public float GetMaxHealth()
    {
        return maxHealth;
    }
}

