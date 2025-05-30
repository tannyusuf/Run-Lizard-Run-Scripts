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
        // Zemin kontrol�
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;  // yere yap���k kalmas� i�in k���k negatif bir de�er
        }

        // Hareket giri�leri
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
        // Z�plama
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Yer�ekimi
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
