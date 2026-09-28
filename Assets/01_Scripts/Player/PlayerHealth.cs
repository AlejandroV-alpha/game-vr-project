using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    [Header("Vida")]
    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth;
    private float tiempoInicio;

    [Header("Audio")]
    [SerializeField] AudioClip damageSound;

    [Header("Feedback")]
    [SerializeField] PlayerDamageFeedback damageFeedback;

    [Header("UI")]
    [SerializeField] PlayerHealthUI healthUI;

    [Header("Menú")]
    [SerializeField] GameManagerMenu gameStartManager;

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
        tiempoInicio = Time.time;
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
        int tiempoSobrevivido = Mathf.FloorToInt(Time.time - tiempoInicio);
        RankingManager.Instance.AddScore("Jugador", tiempoSobrevivido);
        Invoke(nameof(GoToMenu), 1.5f);
    }

    void GoToMenu()
    {
        // currentHealth = maxHealth;
        // healthUI.UpdateHealth(currentHealth, maxHealth);
        //
        // if (gameStartManager != null)
        //     gameStartManager.ShowGameOverMenu();

        // NUEVO: la vida se resetea sola porque GameScene se carga de nuevo al iniciar.
        Time.timeScale = 1f;
        SceneManager.LoadScene("MenuScene");
    }
}
