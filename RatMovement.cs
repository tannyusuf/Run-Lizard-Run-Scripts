using UnityEngine;

/// <summary>
/// Controls the AI movement and attack behavior for rat enemies
/// </summary>
public class RatMovement : MonoBehaviour
{
    // Movement and detection parameters
    public float moveSpeed = 3f;        // Rat movement speed
    public float detectionRange = 8f;   // How far the rat can detect the player
    public float stopDistance = 1.5f;   // Distance at which rat stops to attack
    public float attackCooldown = 1.5f; // Time between attacks
    private float lastAttackTime;       // Timestamp of last attack
    private AudioSource ratAudioSource; // Audio source for rat sounds
    
    // Target reference
    public Transform target;            // Reference to player transform
    
    // Component references
    private Animator animator;          // Rat's animator component
    private CharacterController characterController; // Movement controller
    HealthBar healthSystem;             // Reference to player health system
    IguanaCharacter iguanaCharacter;    // Reference to player character

    /// <summary>
    /// Initialize components, audio, and attack timer
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        lastAttackTime = -attackCooldown;  // Allow immediate attack on first encounter
        
        // Setup audio source
        ratAudioSource = GetComponent<AudioSource>();
        if (ratAudioSource == null)
            ratAudioSource = gameObject.AddComponent<AudioSource>();

        // Configure audio properties
        ratAudioSource.clip = AudioManager.instance.mouseSqueak;
        ratAudioSource.loop = false;
    }

    /// <summary>
    /// Handle rat AI behavior each frame
    /// </summary>
    void Update()
    {
        if (target == null) return;  // Exit if no target

        // Calculate distance to target
        float distance = Vector3.Distance(transform.position, target.position);

        // If player is within detection range
        if (distance <= detectionRange)
        {
            // Make rat face the player (horizontal plane only)
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            // Adjust based on rat model orientation
            transform.Rotate(0, 180, 0);

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
                    
                    // Play rat sound if not already playing
                    if (!ratAudioSource.isPlaying)
                        ratAudioSource.Play();
                        
                    RatDealDamage();      // Apply damage
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
    /// Deal damage to player - can be called from attack animation event
    /// </summary>
    void RatDealDamage()
    {
        IguanaCharacter targetCharacter = target.GetComponent<IguanaCharacter>();
        HealthBar targetHealth = target.GetComponent<HealthBar>();
        
        // Play attack sound
        AudioManager.instance.PlaySFX(AudioManager.instance.punch);
        
        // Apply damage effects
        if (targetCharacter != null) targetCharacter.Hit();
        if (targetHealth != null) targetHealth.TakeDamage(20f); // Slightly less damage than other enemies
    }
}
