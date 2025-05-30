using UnityEngine;
using UnityEngine.UI;  // UI için gerekli

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;
    private Animator animator;

    // Health bar için
    public Image healthFillImage; // Unity Editor'den atanacak

    void Start()
    {
        currentHealth = maxHealth;
        animator = GetComponent<Animator>();
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        if (currentHealth <= 0) return;

        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        UpdateHealthBar();

        if (currentHealth > 0)
        {
            animator.SetTrigger("GetHit");
            animator.SetInteger("AnimationID", 5); // Walk
        }
        else
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        if (healthFillImage != null)
        {
            healthFillImage.fillAmount = currentHealth / maxHealth;
        }
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        animator.SetInteger("AnimationID", 7); // Walk
        AudioSource source = GetComponent<AudioSource>();
        if (source != null && source.isPlaying)
            source.Stop();
        MonoBehaviour[] scripts = GetComponents<MonoBehaviour>();
        foreach (var script in scripts)
        {
            if (script != this) script.enabled = false;
        }

        this.enabled = false;
        Destroy(gameObject, 10f);
    }
}
