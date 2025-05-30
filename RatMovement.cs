using UnityEngine;

public class RatMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float detectionRange = 8f;
    public float stopDistance = 1.5f; // Ratonun duracaðý mesafe
    public float attackCooldown = 1.5f; // 1.5 saniyede bir saldýrý
    private float lastAttackTime;
    private AudioSource ratAudioSource;

    public Transform target;
    private Animator animator;
    private CharacterController characterController;
    HealthBar healthSystem;
    IguanaCharacter iguanaCharacter;


    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        lastAttackTime = -attackCooldown;
        ratAudioSource = GetComponent<AudioSource>();

        if (ratAudioSource == null)
            ratAudioSource = gameObject.AddComponent<AudioSource>();

        ratAudioSource.clip = AudioManager.instance.mouseSqueak;
        ratAudioSource.loop = false;
    }

    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= detectionRange)
        {
            // Sadece yatay eksende bak (y sabit)
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
            // Eðer model 180 derece dönükse
            transform.Rotate(0, 180, 0);

            if (distance > stopDistance)
            {
                Vector3 direction = (target.position - transform.position).normalized;
                Vector3 move = direction * moveSpeed * Time.deltaTime;
                characterController.Move(move);

                animator.SetBool("IsWalking", true);
                animator.SetBool("IsAttacking", false);
                lastAttackTime = Time.time;
            }
            else
            {
                animator.SetBool("IsWalking", false);

                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    animator.SetTrigger("IsAttacking");
                    if (!ratAudioSource.isPlaying)
                        ratAudioSource.Play();
                    RatDealDamage();
                    lastAttackTime = Time.time;
                }
            }
        }
        else
        {
            animator.SetBool("IsWalking", false);
            animator.SetBool("IsAttacking", false);
        }
    }

    // Bu fonksiyonu animasyon event ile çaðýrabilirsin
    void RatDealDamage()
    {
        IguanaCharacter targetCharacter = target.GetComponent<IguanaCharacter>();
        HealthBar targetHealth = target.GetComponent<HealthBar>();
        AudioManager.instance.PlaySFX(AudioManager.instance.punch);
        if (targetCharacter != null) targetCharacter.Hit();
        if (targetHealth != null) targetHealth.TakeDamage(20f); // Örümcekten biraz daha az hasar örnek
    }
}
