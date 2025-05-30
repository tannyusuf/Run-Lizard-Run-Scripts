using UnityEngine;

public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 2f;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    private bool isWalking = false;


    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Zemin kontrolü
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // yere yapýþýk kalmasý için küçük negatif bir deðer
        }

        // Hareket giriþleri
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);
        if (move.magnitude > 0.1f && isGrounded)
        {
            if (!isWalking)
            {
                AudioManager.instance.PlayLizardWalk();
                isWalking = true;
            }
        }
        else
        {
            if (isWalking)
            {
                AudioManager.instance.StopLizardWalk();
                isWalking = false;
            }
        }
        // Zýplama
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Yerçekimi
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
