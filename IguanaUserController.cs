using UnityEngine;

public class IguanaUserController : MonoBehaviour
{
    IguanaCharacter iguanaCharacter;
    HealthBar healthSystem;

    void Start()
    {
        iguanaCharacter = GetComponent<IguanaCharacter>();
        healthSystem = GetComponent<HealthBar>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
            iguanaCharacter.Attack();

        if (Input.GetKeyDown(KeyCode.H))
        {
            iguanaCharacter.Hit();
            healthSystem.TakeDamage(20f); // H basınca 20 damage
        }

        if (Input.GetKeyDown(KeyCode.E))
            iguanaCharacter.Eat();

        if (Input.GetKeyDown(KeyCode.K))
            iguanaCharacter.Death();

        if (Input.GetKeyDown(KeyCode.R))
            iguanaCharacter.Rebirth();
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        iguanaCharacter.Move(v, h);
    }
}
