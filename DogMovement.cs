using UnityEngine;

public class DogMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 22f;
    public float stopDistance = 1f;
    public float attackCooldown = 4f;
    private float lastAttackTime;

    public Transform target;
    private Animator animator;
    private CharacterController characterController;
    private float gravity = -9.81f;
    private float verticalVelocity = 0f;
    private AudioSource dogAudioSource;

    private bool hasBarked = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        lastAttackTime = -attackCooldown;
        dogAudioSource = GetComponent<AudioSource>();
if (dogAudioSource == null)
    dogAudioSource = gameObject.AddComponent<AudioSource>();

dogAudioSource.clip = AudioManager.instance.dogBark;
dogAudioSource.loop = false;
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= detectionRange)
        {
            // Yalnızca yatay düzlemde bakış
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

            if (distance > stopDistance)
            {
                // Hareket yönü (XZ düzlemi)
                if (!hasBarked)
                {
                    dogAudioSource.Play();
                    hasBarked = true;
                }

                Vector3 direction = new Vector3(
                    target.position.x - transform.position.x,
                    0f,
                    target.position.z - transform.position.z
                ).normalized;

                // Yerçekimi
                if (characterController.isGrounded)
                {
                    verticalVelocity = -1f;
                }
                else
                {
                    verticalVelocity += gravity * Time.deltaTime;
                }

                Vector3 move = direction * moveSpeed * Time.deltaTime;
                move.y = verticalVelocity * Time.deltaTime;

                characterController.Move(move);

                // Sadece yürüyüş animasyonu
                animator.SetInteger("AnimationID", 4); // Walk
            }
            else
            {
                // Saldırı zamanı geldiyse
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    animator.SetInteger("AnimationID", 6); // Attack
                    lastAttackTime = Time.time;
                    DogDealDamage();
                }
                else if (Time.time - lastAttackTime < 0.5f)
                {
                    // Saldırı animasyonu oynanıyor
                    return;
                }
            }
        }
        else
        {
            animator.SetInteger("AnimationID", 1); // Idle
            hasBarked = false;

        }
    }

    void DogDealDamage()
    {
        IguanaCharacter targetCharacter = target.GetComponent<IguanaCharacter>();
        HealthBar targetHealth = target.GetComponent<HealthBar>();
        AudioManager.instance.PlaySFX(AudioManager.instance.punch);
        if (targetCharacter != null) targetCharacter.Hit();
        if (targetHealth != null) targetHealth.TakeDamage(25f);
    }
}
