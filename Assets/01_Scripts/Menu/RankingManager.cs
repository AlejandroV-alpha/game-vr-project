using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class RankingEntry
{
    public int points;        // puntos obtenidos en la partida
    public float seconds;     // tiempo que duró la partida (dato extra)
    public string dateTime;   // fecha y hora
}

[Serializable]
public class RankingData
{
    public List<RankingEntry> entries = new List<RankingEntry>();
}

public class RankingManager : MonoBehaviour
{
    public static RankingManager Instance;

    [Tooltip("Cuántos registros se guardan en total (0 = sin límite).")]
    [SerializeField] private int maxEntries = 10;

    private string path;
    public RankingData data = new RankingData();

    public event Action OnChanged;

    [ContextMenu("Guardar ahora")]
    void GuardarAhora() { Save(); }

    [ContextMenu("Borrar ranking")]
    void BorrarRanking()
    {
        data = new RankingData();
        Save();
    }

    [ContextMenu("Agregar registro de prueba")]
    void AgregarPrueba()
    {
        AddScore(UnityEngine.Random.Range(100, 2000), UnityEngine.Random.Range(10f, 120f));
    }

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        path = Path.Combine(Application.persistentDataPath, "ranking.json");
        Debug.Log("Ranking JSON en: " + path);
        Load();
    }

    public void AddScore(int points, float seconds = 0f)
    {
        data.entries.Add(new RankingEntry
        {
            points = points,
            seconds = seconds,
            dateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
        });

        // Más puntos = mejor posición (la posición 1 es el índice 0)
        var ordenado = data.entries.OrderByDescending(e => e.points);
        data.entries = (maxEntries > 0 ? ordenado.Take(maxEntries) : ordenado).ToList();

        Save();
    }

    public void Save()
    {
        try
        {
            File.WriteAllText(path, JsonUtility.ToJson(data, true));
        }
        catch (Exception e)
        {
            Debug.LogError("No se pudo guardar ranking.json: " + e);
        }
        OnChanged?.Invoke();
    }

    public void Load()
    {
        data = new RankingData();
        if (!File.Exists(path)) return;

        try
        {
            var loaded = JsonUtility.FromJson<RankingData>(File.ReadAllText(path));
            if (loaded != null && loaded.entries != null) data = loaded;
        }
        catch (Exception e)
        {
            Debug.LogWarning("No se pudo leer ranking.json: " + e.Message);
        }
    }
}