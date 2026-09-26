using UnityEngine;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    [Header("Vida")]
    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth;

    [Header("Audio")]
    [SerializeField] AudioClip damageSound;

    [Header("Feedback")]
    [SerializeField] PlayerDamageFeedback damageFeedback;

    [Header("UI")]
    [SerializeField] PlayerHealthUI healthUI;

    AudioSource audioSource;

    /// <summary>
    /// obtiene los componentes necesarios
    /// </summary>
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// carga la vida inicial del jugador
    /// </summary>
    void Start()
    {
        currentHealth = maxHealth;

        healthUI.UpdateHealth(currentHealth, maxHealth);
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

        healthUI.UpdateHealth(currentHealth, maxHealth);

        audioSource.PlayOneShot(damageSound);
        damageFeedback.PlayFeedback();

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
