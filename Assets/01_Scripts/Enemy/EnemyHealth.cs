using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    [Header("Datos")]
    public EnemyData enemyData;

    [Header("Vida")]
    [SerializeField] int currentHealth;

    /// <summary>
    /// obtiene la vida inicial desde los datos del enemigo
    /// </summary>
    void Start()
    {
        currentHealth = enemyData.baseHealth;
    }

    /// <summary>
    /// reduce la vida del enemigo
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        Debug.Log("vida enemigo: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// destruye al enemigo cuando se queda sin vida
    /// </summary>
    void Die()
    {
        Destroy(gameObject);
    }
}
