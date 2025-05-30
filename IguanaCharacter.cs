using UnityEngine;
using UnityEngine.SceneManagement;

public class IguanaCharacter : MonoBehaviour
{
    Animator iguanaAnimator;
    public Transform respawnPoint;
    public HealthBar healthSystem;
    public int cointCount = 0;
    public float damageAmount = 5f;

    // Değerleri korumak için static değişkenler
    private static bool hasStoredValues = false;
    private static int storedCointCount;
    private static float storedDamageAmount;
    private static float storedMaxHealth;

    void Start()
    {
        iguanaAnimator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthBar>();

        // Eğer daha önce kaydedilmiş değerler varsa onları geri yükle
        if (hasStoredValues)
        {
            cointCount = storedCointCount;
            damageAmount = storedDamageAmount;
            if (healthSystem != null)
            {
                healthSystem.maxHealth = storedMaxHealth;
                healthSystem.RestoreFullHealth();
            }
        }
    }

    public void Attack()
    {
        iguanaAnimator.SetTrigger("Attack");
        AudioManager.instance.PlaySFX(AudioManager.instance.punch);
        Invoke("TryDamageEnemy", 0.3f);
    }

    public void TryDamageEnemy()
    {
        float damageRange = 2f;
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, damageRange);
        foreach (Collider col in hitEnemies)
        {
            if (col.CompareTag("Enemy"))
            {
                EnemyHealth enemy = col.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damageAmount);
                }
            }
        }
    }

    public void Hit() => iguanaAnimator.SetTrigger("Hit");
    public void Eat() => iguanaAnimator.SetTrigger("Eat");

    public void Death()
    {
        // Ölmeden önce mevcut değerleri kaydet
        StoreCurrentValues();
        iguanaAnimator.SetTrigger("Death");
        Invoke("Rebirth", 2f);
    }

    private void StoreCurrentValues()
    {
        storedCointCount = cointCount;
        storedDamageAmount = damageAmount;
        if (healthSystem != null)
        {
            storedMaxHealth = healthSystem.maxHealth;
        }
        hasStoredValues = true;
    }

    public void Rebirth()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // Scene yüklendiğinde Start() metodunda değerler otomatik olarak geri yüklenecek
    }

    void OnAnimatorMove()
    {
        if (iguanaAnimator && iguanaAnimator.applyRootMotion)
        {
            transform.position += iguanaAnimator.deltaPosition * 2f;
            transform.rotation *= iguanaAnimator.deltaRotation;
        }
    }

    public void Move(float v, float h)
    {
        iguanaAnimator.SetFloat("Forward", v);
        iguanaAnimator.SetFloat("Turn", h);
    }

    // Coin ekleme metodu (upgrade sistemleri için)
    public void AddCoins(int amount)
    {
        cointCount += amount;
    }

    // Damage artırma metodu (upgrade sistemleri için)
    public void IncreaseDamage(float amount)
    {
        damageAmount += amount;
    }

    // Can artırma metodu (upgrade sistemleri için)
    public void IncreaseMaxHealth(float amount)
    {
        if (healthSystem != null)
        {
            healthSystem.IncreaseMaxHealth(amount);
        }
    }
}