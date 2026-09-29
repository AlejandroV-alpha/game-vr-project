using System;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Tooltip("Puntos que se ganan por cada segundo jugado.")]
    [SerializeField] private float pointsPerSecond = 10f;

    private float pointsFloat;
    private float elapsed;
    private int lastShown = -1;
    private bool running = true;

    public int CurrentPoints => Mathf.FloorToInt(pointsFloat);
    public float ElapsedSeconds => elapsed;

    /// Úsalo para actualizar tu texto de puntaje en pantalla.
    public event Action<int> OnPointsChanged;

    void Awake() { Instance = this; }

    void Update()
    {
        if (!running) return;

        elapsed += Time.deltaTime;                    // respeta la pausa (timeScale = 0)
        pointsFloat += pointsPerSecond * Time.deltaTime;

        if (CurrentPoints != lastShown)
        {
            lastShown = CurrentPoints;
            OnPointsChanged?.Invoke(lastShown);
        }
    }

    /// Para puntos extra (enemigos, items, etc.)
    public void AddPoints(int amount)
    {
        pointsFloat += amount;
        Debug.Log($"[ScoreManager] +{amount} pts (total: {CurrentPoints})");
    }

    public void StartRun()
    {
        pointsFloat = 0f;
        elapsed = 0f;
        lastShown = -1;
        running = true;
    }

    /// Llámalo cuando el jugador muera / termine la partida.
    public void EndRun()
    {
        if (!running) return;
        running = false;
        if (RankingManager.Instance != null)
            RankingManager.Instance.AddScore(CurrentPoints, elapsed);
    }
}