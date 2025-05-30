using UnityEngine;
using UnityEngine.UI;  // Required for UI elements

/// <summary>
/// Manages an enemy's health system and death sequence
/// </summary>
public class EnemyHealth : MonoBehaviour
{
    // Health parameters
    public float maxHealth = 100f;      // Maximum possible health
    private float currentHealth;        // Current health points
    private Animator animator;          // Reference to animator
    
    // UI reference
    public Image healthFillImage;       // UI image for health bar fill

    /// <summary>
    /// Initialize health and components
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        UpdateHealthBar();
    }

    /// <summary>
    /// Apply damage to the enemy
    /// </summary>
    /// <param name="amount">Amount of damage to take</param>
    public void TakeDamage(float amount)
    {
        // Skip if already dead
        if (currentHealth <= 0) return;

        // Reduce health by damage amount
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        // Update health bar UI
        UpdateHealthBar();

        // Handle hit reaction or death
        if (currentHealth > 0)
        {
            // Play hit animation
            animator.SetTrigger("GetHit");
            animator.SetInteger("AnimationID", 5); // Set to walk animation after hit
        }
        else
        {
            // Play death animation and handle death logic
            Die();
        }
    }

    /// <summary>
    /// Update the visual health bar to match current health
    /// </summary>
    void UpdateHealthBar()
    {
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = currentHealth / maxHealth;
        }
    }

    /// <summary>
    /// Handle enemy death sequence
    /// </summary>
    public void Die()
    {
        // Play death animation
        animator.SetTrigger("Die");
        animator.SetInteger("AnimationID", 7); // Set to death animation
        
        // Stop any audio playing
        AudioSource source = GetComponent<AudioSource>();
        if (source != null && source.isPlaying)
            source.Stop();
            
        // Disable all scripts except this one for clean death
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            if (script != this) script.enabled = false;
        }

        // Disable this script last
        this.enabled = false;
        
        // Destroy enemy object after delay
        Destroy(gameObject, 10f);
    }
}
