using TMPro;
using UnityEngine;

// Va SOLO en GameScene (en un GameObject vacio "GameManager").
public class GameManagerMenu : MonoBehaviour
{
    [Header("Ranking (siempre visible en GameScene)")]
    [Tooltip("Panel del ranking que tiene el script RankingUI.")]
    [SerializeField] private GameObject panelRanking;

    [Tooltip("Opcional: texto que muestra cuantos segundos llevas sobreviviendo.")]
    [SerializeField] private TMP_Text tiempoText;

    // ================== YA NO SE USA (comentado) ==================
    // [SerializeField] private GameObject menuUI;
    // [SerializeField] private GameObject panelInicio;
    // [SerializeField] private GameObject panelGameOver;
    // [SerializeField] private Behaviour[] componentesABloquear;
    // private GameObject panelAnterior;
    // ==============================================================

    private float tiempo;
    private bool jugando;

    private void Awake()
    {
        // Ya no se pausa el juego.
        Time.timeScale = 1f;

        // --- Codigo anterior (ya no se usa) ---
        // MostrarPanel(panelInicio);
        // SetBloqueo(true);
    }

    private void Start()
    {
        tiempo = 0f;
        jugando = true;
    }

    private void Update()
    {
        if (!jugando) return;

        tiempo += Time.deltaTime;
        if (tiempoText != null)
            tiempoText.text = $"Tiempo: {tiempo:0.0}s";
    }

    /// <summary>
    /// Llamalo desde PlayerHealth cuando el jugador muere:
    /// guarda los segundos en el JSON y refresca el ranking (que ya esta visible).
    /// </summary>
    public void ShowGameOverMenu()
    {
        if (!jugando) return; // evita guardar dos veces
        jugando = false;

        if (RankingManager.Instance != null)
            RankingManager.Instance.AddScore(tiempo);

        if (panelRanking != null)
        {
            var rankingUI = panelRanking.GetComponentInChildren<RankingUI>(true);
            if (rankingUI != null) rankingUI.Refresh();
        }

        // --- Codigo anterior (ya no se usa) ---
        // MostrarPanel(panelGameOver);
        // SetBloqueo(true);
    }

    // ================== YA NO SE USA (comentado) ==================
    // public void StartGame() { ... }
    // private void MostrarPanel(GameObject panelAMostrar) { ... }
    // private void SetBloqueo(bool bloqueado) { ... }
    // public void ShowRanking() { ... }
    // public void CloseRanking() { ... }
    // ==============================================================
}