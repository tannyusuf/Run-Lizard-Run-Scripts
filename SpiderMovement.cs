using UnityEngine;

/// <summary>
/// Controls the AI movement and attack behavior for spider enemies
/// </summary>
public class SpiderMovement : MonoBehaviour
{
    // Movement and detection parameters
    public float moveSpeed = 2f;         // Spider movement speed
    public float detectionRange = 10f;   // How far the spider can detect the player
    public float stopDistance = 2f;      // Distance at which spider stops to attack
    public float attackCooldown = 2f;    // Time between attacks
    private float lastAttackTime;        // Timestamp of last attack

    // Target reference
    public Transform target;             // Reference to player transform
    
    // Component references
    private Animator animator;           // Spider's animator component
    private CharacterController characterController;  // Movement controller
    IguanaCharacter iguanaCharacter;     // Reference to player for damage
    HealthBar healthSystem;              // Reference to player's health system

    /// <summary>
    /// Initialize components and attack timer
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        lastAttackTime = -attackCooldown;  // Allow immediate attack on first encounter
    }

    /// <summary>
    /// Handle spider AI behavior each frame
    /// </summary>
    void Update()
    {
        if (target == null) return;  // Exit if no target

        // Calculate distance to target
        float distance = Vector3.Distance(transform.position, target.position);

        // If player is within detection range
        if (distance <= detectionRange)
        {
            // Make spider face the player (horizontal plane only)
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            transform.Rotate(0, 180, 0);  // Adjust based on spider model orientation

            // If not close enough to attack, move toward player
            if (distance > stopDistance)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                Vector3 move = direction * moveSpeed * Time.deltaTime;
                characterController.Move(move);

                // Play walking animation
                animator.SetBool("IsWalking", true);
                animator.SetBool("IsAttacking", false);
                lastAttackTime = Time.time;  // Reset attack timer when moving
            }
            else  // Close enough to attack
            {
                // Stop walking
                animator.SetBool("IsWalking", false);

                // Attack if cooldown elapsed
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    animator.SetTrigger("IsAttacking");  // Trigger attack animation
                    lastAttackTime = Time.time;  // Record attack time
                }
            }
        }
        else  // Player outside detection range
        {
            // Return to idle state
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsAttacking", false);
        }
    }
    
    /// <summary>
    /// Deal damage to player - called from attack animation event
    /// </summary>
    void SpiderDealDamage()
    {
        IguanaCharacter targetCharacter = target.GetComponent<IguanaCharacter>();
        HealthBar targetHealth = target.GetComponent<HealthBar>();
        
        // Play attack sound
        AudioManager.instance.PlaySFX(AudioManager.instance.punch);
        
        // Apply damage effects
        if (targetCharacter != null) targetCharacter.Hit();
        if (targetHealth != null) targetHealth.TakeDamage(20f);
    }
}
