using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

[Serializable]
public class RankingEntry
{
    public float seconds;     // segundos transcurridos en la partida
    public string dateTime;   // fecha y hora (la posicion NO se guarda: es el indice de la lista)
}

[Serializable]
public class RankingData
{
    public List<RankingEntry> entries = new List<RankingEntry>();
}

public class RankingManager : MonoBehaviour
{
    public static RankingManager Instance;

    [Tooltip("Desactivado: gana quien dura MAS segundos (supervivencia). Activado: gana quien tarda MENOS.")]
    [SerializeField] private bool menorTiempoEsMejor = false;

    private string path;
    public RankingData data = new RankingData();

    public event Action OnChanged;

    [ContextMenu("Guardar ahora")]
    void GuardarAhora() { Save(); }

    // ---- Herramientas de prueba: clic derecho en el titulo del componente (SOLO en Play) ----

    [ContextMenu("Borrar ranking")]
    void BorrarRanking()
    {
        data = new RankingData();
        Save();
        RefrescarPaneles();
    }

    [ContextMenu("Agregar registro de prueba")]
    void AgregarPrueba()
    {
        AddScore(UnityEngine.Random.Range(10f, 120f)); // UnityEngine. porque "using System" tambien tiene Random
        RefrescarPaneles();
    }

    void RefrescarPaneles()
    {
        foreach (var ui in FindObjectsByType<RankingUI>(FindObjectsInactive.Include, FindObjectsSortMode.None))
            ui.Refresh();
    }
    // ------------------------------------------------------------------------------------------

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
        Debug.Log("Ranking JSON en: " + path); // asi sabes donde esta el archivo
        Load();
    }

    public void AddScore(float seconds)
    {
        data.entries.Add(new RankingEntry
        {
            seconds = seconds,
            dateTime = DateTime.Now.ToString("dd/MM/yyyy HH:mm")
        });

        // El sort define el ranking: la posicion 1 es el indice 0 de la lista.
        var ordenado = menorTiempoEsMejor
            ? data.entries.OrderBy(e => e.seconds)
            : data.entries.OrderByDescending(e => e.seconds);

        data.entries = ordenado.Take(10).ToList();
        Save();
    }

    public void Save()
    {
        try
        {
            File.WriteAllText(path, JsonUtility.ToJson(data, true));
            Debug.Log($"Ranking guardado ({data.entries.Count} registros) en: {path}");
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