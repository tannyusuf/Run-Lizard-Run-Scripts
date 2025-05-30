using UnityEngine;

public class SpiderMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float detectionRange = 10f;
    public float stopDistance = 2f; // Örümceðin duracaðý mesafe
    public float attackCooldown = 2f; // 2 saniyede bir saldýrý
    private float lastAttackTime;

    public Transform target;
    private Animator animator;
    private CharacterController characterController;
    IguanaCharacter iguanaCharacter;
    HealthBar healthSystem;


    void Start()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        lastAttackTime = -attackCooldown;
    }


    void Update()
    {
        if (target == null) return;

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance <= detectionRange)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
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
                    animator.SetTrigger("IsAttacking"); // Trigger daha doðru olur
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
    void SpiderDealDamage()
    {
        IguanaCharacter targetCharacter = target.GetComponent<IguanaCharacter>();
        HealthBar targetHealth = target.GetComponent<HealthBar>();
        AudioManager.instance.PlaySFX(AudioManager.instance.punch);
        if (targetCharacter != null) targetCharacter.Hit();
        if (targetHealth != null) targetHealth.TakeDamage(20f);
    }

}
