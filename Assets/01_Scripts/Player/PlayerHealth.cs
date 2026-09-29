using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour, ITakeDamage
{
    [Header("Vida")]
    [SerializeField] int maxHealth = 3;
    [SerializeField] int currentHealth;
    // private float tiempoInicio;

    [Header("Audio")]
    [SerializeField] AudioClip damageSound;

    [Header("Feedback")]
    [SerializeField] PlayerDamageFeedback damageFeedback;

    [Header("UI")]
    [SerializeField] PlayerHealthUI healthUI;

    [Header("Menu")]
    [SerializeField] GameManagerMenu gameStartManager;
    [Tooltip("Nombre exacto de la escena del menu.")]
    [SerializeField] string menuSceneName = "MenuScene";
    [Tooltip("Segundos que se ve el ranking antes de pasar al menu.")]
    [SerializeField] float segundosAntesDelMenu = 3f;

    AudioSource audioSource;
    bool muerto;

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
        // tiempoInicio = Time.time;
    }

    /// <summary>
    /// reduce la vida del jugador
    /// </summary>
    public void TakeDamage(int damage)
    {
        if (muerto) return; // ya murio: ignora golpes extra

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
    /// controla cuando el jugador se queda sin vida:
    /// guarda el tiempo en el ranking y despues de unos segundos va al menu
    /// </summary>
    //void Die()
    //{
    //    if (muerto) return;
    //    muerto = true;

    //    Debug.Log("jugador sin vida");
    //    var gm = FindFirstObjectByType<GameManagerMenu>();
    //    if (gm != null) gm.ShowGameOverMenu();
    //    else Debug.LogError("No hay un GameManagerMenu en esta escena.");

    //    // Guarda los segundos en el JSON y refresca el ranking
    //    if (gameStartManager != null)
    //        gameStartManager.ShowGameOverMenu();

    //    //int tiempoSobrevivido = Mathf.FloorToInt(Time.time - tiempoInicio);
    //    //RankingManager.Instance.AddScore("Jugador", tiempoSobrevivido);

    //    Invoke(nameof(GoToMenu), segundosAntesDelMenu);
    //}
    void Die()
    {
        if (muerto) return;
        muerto = true;

        Debug.Log("jugador sin vida");

        // Si el campo del Inspector esta vacio, lo busca en la escena
        if (gameStartManager == null)
            gameStartManager = FindFirstObjectByType<GameManagerMenu>();

        if (gameStartManager != null)
            gameStartManager.ShowGameOverMenu();
        else
            Debug.LogError("PlayerHealth: no hay GameManagerMenu en la escena, no se guarda el ranking.");

        Invoke(nameof(GoToMenu), segundosAntesDelMenu);
    }

    void GoToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);

        //currentHealth = maxHealth;
        //healthUI.UpdateHealth(currentHealth, maxHealth);
        //if (gameStartManager != null)
        //    gameStartManager.ShowGameOverMenu();
    }
}