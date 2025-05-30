using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Main character controller for the iguana player
/// Manages player stats, animations, and actions
/// </summary>
public class IguanaCharacter : MonoBehaviour
{
    // Component references
    Animator iguanaAnimator;           // Animator controller
    public Transform respawnPoint;     // Where player respawns after death
    public HealthBar healthSystem;     // Health system reference
    
    // Player stats
    public int cointCount = 0;         // Current coin count
    public float damageAmount = 5f;    // Base damage for attacks

    // Static variables to persist values between deaths/respawns
    private static bool hasStoredValues = false;  // Whether values are stored
    private static int storedCointCount;          // Stored coin count
    private static float storedDamageAmount;      // Stored damage amount
    private static float storedMaxHealth;         // Stored max health

    /// <summary>
    /// Initialize components and restore saved values if they exist
    /// </summary>
    void Start()
    {
        iguanaAnimator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthBar>();

        // Restore previous values if they exist
        if (hasStoredValues)
        {
            cointCount = storedCointCount;
            damageAmount = storedDamageAmount;
            if (healthSystem != null)
            {
                healthSystem.maxHealth = storedMaxHealth;
                healthSystem.RestoreFullHealth();
            }
        }
    }

    /// <summary>
    /// Trigger the attack animation and apply damage
    /// </summary>
    public void Attack()
    {
        iguanaAnimator.SetTrigger("Attack");
        AudioManager.instance.PlaySFX(AudioManager.instance.punch);
        Invoke("TryDamageEnemy", 0.3f);  // Delay damage to sync with animation
    }

    /// <summary>
    /// Apply damage to enemies within range
    /// </summary>
    public void TryDamageEnemy()
    {
        // Define attack range
        float damageRange = 2f;
        
        // Find all colliders within range
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, damageRange);
        
        // Check each collider to see if it's an enemy
        foreach (Collider col in hitEnemies)
        {
            if (col.CompareTag("Enemy"))
            {
                EnemyHealth enemy = col.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount);
                }
            }
        }
    }

    /// <summary>
    /// Trigger hit reaction animation
    /// </summary>
    public void Hit() => iguanaAnimator.SetTrigger("Hit");
    
    /// <summary>
    /// Trigger eat animation
    /// </summary>
    public void Eat() => iguanaAnimator.SetTrigger("Eat");

    /// <summary>
    /// Handle player death and respawn
    /// </summary>
    public void Death()
    {
        // Save current values before death
        StoreCurrentValues();
        
        // Play death animation
        iguanaAnimator.SetTrigger("Death");
        
        // Respawn after delay
        Invoke("Rebirth", 2f);
    }

    /// <summary>
    /// Store current player stats for persistence after death
    /// </summary>
    private void StoreCurrentValues()
    {
        storedCointCount = cointCount;
        storedDamageAmount = damageAmount;
        if (healthSystem != null)
        {
            storedMaxHealth = healthSystem.maxHealth;
        }
        hasStoredValues = true;
    }

    /// <summary>
    /// Reload the current scene to respawn player
    /// </summary>
    public void Rebirth()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Values are restored in Start() after scene reloads
    }

    /// <summary>
    /// Handle root motion for animations
    /// </summary>
    void OnAnimatorMove()
    {
        if (iguanaAnimator && iguanaAnimator.applyRootMotion)
        {
            // Apply animation movement with multiplier
            transform.position += iguanaAnimator.deltaPosition * 2f;
            transform.rotation *= iguanaAnimator.deltaRotation;
        }
    }

    /// <summary>
    /// Process movement inputs
    /// </summary>
    /// <param name="v">Forward/backward movement (-1 to 1)</param>
    /// <param name="h">Left/right turning (-1 to 1)</param>
    public void Move(float v, float h)
    {
        iguanaAnimator.SetFloat("Forward", v);
        iguanaAnimator.SetFloat("Turn", h);
    }

    /// <summary>
    /// Add coins to player inventory
    /// </summary>
    /// <param name="amount">Number of coins to add</param>
    public void AddCoins(int amount)
    {
        cointCount += amount;
    }

    /// <summary>
    /// Increase player's attack damage
    /// </summary>
    /// <param name="amount">Amount to increase damage by</param>
    public void IncreaseDamage(float amount)
    {
        damageAmount += amount;
    }

    /// <summary>
    /// Increase player's maximum health
    /// </summary>
    /// <param name="amount">Amount to increase max health by</param>
    public void IncreaseMaxHealth(float amount)
    {
        if (healthSystem != null)
        {
            healthSystem.IncreaseMaxHealth(amount);
        }
    }
}