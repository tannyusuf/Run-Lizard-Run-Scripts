using UnityEngine;

/// <summary>
/// Controls the AI movement and attack behavior for dog enemies
/// </summary>
public class DogMovement : MonoBehaviour
{
    // Movement and detection parameters
    public float moveSpeed = 3f;        // Dog movement speed
    public float detectionRange = 22f;  // How far the dog can detect the player
    public float stopDistance = 1f;     // Distance at which dog stops to attack
    public float attackCooldown = 4f;   // Time between attacks
    private float lastAttackTime;       // Timestamp of last attack

    // Target reference
    public Transform target;            // Reference to player transform
    
    // Component references
    private Animator animator;          // Dog's animator component
    private CharacterController characterController;  // Movement controller
    private float gravity = -9.81f;     // Gravity force
    private float verticalVelocity = 0f; // Vertical movement velocity
    private AudioSource dogAudioSource;  // Audio source for dog sounds
    
    // State tracking
    private bool hasBarked = false;     // Whether dog has barked this encounter

    /// <summary>
    /// Initialize components, audio, and attack timer
    /// </summary>
    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        lastAttackTime = -attackCooldown;  // Allow immediate attack on first encounter
        dogAudioSource = GetComponent<AudioSource>();
        
        // Setup audio source if not already present
        if (dogAudioSource == null)
            dogAudioSource = gameObject.AddComponent<AudioSource>();

        // Configure audio properties
        dogAudioSource.clip = AudioManager.instance.dogBark;
        dogAudioSource.loop = false;
    }

    /// <summary>
    /// Handle dog AI behavior each frame
    /// </summary>
    void Update()
    {
        if (target == null) return;  // Exit if no target

        // Calculate distance to target
        float distance = Vector3.Distance(transform.position, target.position);

        // If player is within detection range
        if (distance <= detectionRange)
        {
            // Make dog face the player (horizontal plane only)
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

            // If not close enough to attack, move toward player
            if (distance > stopDistance)
            {
                // Play bark sound when first detected
                if (!hasBarked)
                {
                    dogAudioSource.Play();
                    hasBarked = true;
                }

                // Calculate movement direction (ignoring Y axis)
                Vector3 direction = new Vector3(
                    target.position.x - transform.position.x,
                    0f,
                    target.position.z - transform.position.z
                ).normalized;

                // Apply gravity
                if (characterController.isGrounded)
                {
                    verticalVelocity = -1f;  // Small downward force when grounded
                }
                else
                {
                    verticalVelocity += gravity * Time.deltaTime;  // Accelerate downward
                }

                // Combine horizontal movement with vertical velocity
                Vector3 move = direction * moveSpeed * Time.deltaTime;
                move.y = verticalVelocity * Time.deltaTime;

                // Move the character
                characterController.Move(move);

                // Play walking animation
                animator.SetInteger("AnimationID", 4); // Walk
            }
            else  // Close enough to attack
            {
                // Attack if cooldown elapsed
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    // Play attack animation
                    animator.SetInteger("AnimationID", 6); // Attack
                    lastAttackTime = Time.time;  // Record attack time
                    DogDealDamage();  // Apply damage
                }
                else if (Time.time - lastAttackTime < 0.5f)
                {
                    // Still in attack animation, don't change state
                    return;
                }
            }
        }
        else  // Player outside detection range
        {
            // Return to idle state
            animator.SetInteger("AnimationID", 1); // Idle
            hasBarked = false;  // Reset bark flag
        }
    }

    /// <summary>
    /// Deal damage to player - can be called from attack animation event
    /// </summary>
    void DogDealDamage()
    {
        IguanaCharacter targetCharacter = target.GetComponent<IguanaCharacter>();
        HealthBar targetHealth = target.GetComponent<HealthBar>();
        
        // Play attack sound
        AudioManager.instance.PlaySFX(AudioManager.instance.punch);
        
        // Apply damage effects
        if (targetCharacter != null) targetCharacter.Hit();
        if (targetHealth != null) targetHealth.TakeDamage(25f);  // Dogs deal more damage
    }
}
