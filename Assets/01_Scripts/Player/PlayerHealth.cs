using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    [Header("Vida")]
    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth;

    /// <summary>
    /// carga la vida inicial del jugador
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;
    }

    /// <summary>
    /// reduce la vida del jugador
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        Debug.Log("vida jugador: " + currentHealth + "/" + maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// controla cuando el jugador se queda sin vida
    /// </summary>
    void Die()
    {
        Debug.Log("jugador sin vida");
    }
}
