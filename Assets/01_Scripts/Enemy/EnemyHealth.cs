using UnityEngine;

public class EnemyHealth : MonoBehaviour, ITakeDamage
{
    [Header("Datos")]
    public EnemyData enemyData;

    [Header("Vida")]
    [SerializeField] int maxHealth;
    [SerializeField] int currentHealth;

    [Header("Efectos")]
    [SerializeField] GameObject enemyExplosionPrefab;

    /// <summary>
    /// calcula la vida inicial del enemigo
    /// </summary>
    void Start()
    {
        int healthBonus = DifficultyManager.instance.GetHealthBonus();

        maxHealth = enemyData.baseHealth + healthBonus;
        currentHealth = maxHealth;
    }

    /// <summary>
    /// reduce la vida del enemigo
    /// </summary>
    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth < 0)
        {
            currentHealth = 0;
        }

        Debug.Log("vida enemigo: " + currentHealth + "/" + maxHealth);

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
        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoints(enemyData.pointsOnDeath);
            Debug.Log($"[Puntos] Enemigo murió: +{enemyData.pointsOnDeath} pts | Total: {ScoreManager.Instance.CurrentPoints}");
        }
        else
        {
            Debug.LogWarning("[Puntos] ScoreManager.Instance es null: no se sumaron puntos. ¿Está en GameScene?");
        }

        if (enemyExplosionPrefab != null)
        {
            Instantiate(enemyExplosionPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
