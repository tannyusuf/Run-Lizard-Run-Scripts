using UnityEngine;

/// <summary>
/// Controls the player's character movement and jumping
/// </summary>
public class Movement : MonoBehaviour
{
    // Movement parameters
    public float moveSpeed = 5f;       // Walking/running speed
    public float gravity = -9.81f;     // Gravity force
    public float jumpHeight = 2f;      // Jump height
    
    // Component references
    private CharacterController controller;  // Character controller component
    private Vector3 velocity;          // Current movement velocity
    private bool isGrounded;           // Is player touching ground?
    
    // Ground check parameters
    public Transform groundCheck;      // Position to check for ground
    public float groundDistance = 0.4f; // Radius of ground check sphere
    public LayerMask groundMask;       // Which layers count as ground
    private bool isWalking = false;    // Is player currently walking?

    /// <summary>
    /// Initialize components
    /// </summary>
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    /// <summary>
    /// Handle player movement, jumping, and gravity each frame
    /// </summary>
    void Update()
    {
        // Check if player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        // Reset downward velocity when grounded
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // Small negative value to keep grounded
        }

        // Get horizontal and vertical input axes
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calculate movement direction relative to player orientation
        Vector3 move = transform.right * x + transform.forward * z;

        // Apply movement
        controller.Move(move * moveSpeed * Time.deltaTime);
        
        // Handle walking sound effects
        if (move.magnitude > 0.1f && isGrounded)
        {
            if (!isWalking)
            {
                // Start walking sound when player starts walking on ground
                AudioManager.instance.PlayLizardWalk();
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                // Stop walking sound when player stops moving
                AudioManager.instance.StopLizardWalk();
                isWalking = false;
            }
        }
        
        // Handle jumping
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            // Apply upward velocity using jump height formula
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Apply gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
