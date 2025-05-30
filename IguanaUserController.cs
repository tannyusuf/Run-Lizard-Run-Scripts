using UnityEngine;

/// <summary>
/// Handles player input and maps it to character actions
/// </summary>
public class IguanaUserController : MonoBehaviour
{
    // Component references
    IguanaCharacter iguanaCharacter;   // Character controller reference
    HealthBar healthSystem;            // Health system reference

    /// <summary>
    /// Initialize component references
    /// </summary>
    void Start()
    {
        iguanaCharacter = GetComponent<IguanaCharacter>();
        healthSystem = GetComponent<HealthBar>();
    }

    /// <summary>
    /// Handle player input each frame
    /// </summary>
    void Update()
    {
        // Attack with left mouse button
        if (Input.GetButtonDown("Fire1"))
            iguanaCharacter.Attack();

        // Test damage with H key (debug)
        if (Input.GetKeyDown(KeyCode.H))
        {
            iguanaCharacter.Hit();
            healthSystem.TakeDamage(20f); // H key inflicts 20 damage
        }

        // Eat animation with E key
        if (Input.GetKeyDown(KeyCode.E))
            iguanaCharacter.Eat();

        // Test death with K key (debug)
        if (Input.GetKeyDown(KeyCode.K))
            iguanaCharacter.Death();

        // Test rebirth with R key (debug)
        if (Input.GetKeyDown(KeyCode.R))
            iguanaCharacter.Rebirth();
    }

    /// <summary>
    /// Handle movement input in fixed intervals
    /// </summary>
    void FixedUpdate()
    {
        // Get horizontal and vertical input axes
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        
        // Pass input values to character controller
        iguanaCharacter.Move(v, h);
    }
}
