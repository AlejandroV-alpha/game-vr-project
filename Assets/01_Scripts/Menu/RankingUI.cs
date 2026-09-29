using UnityEngine;

public class RankingUI : MonoBehaviour
{
    [Tooltip("El Transform padre donde se instancian las filas (el objeto 'Content').")]
    public Transform contentParent;

    [Tooltip("El Prefab de la fila (RankingRow). Debe ser un PREFAB del Project, no un objeto de la escena.")]
    public GameObject rowPrefab;


    void OnEnable()
    {
        Subscribe();
        Refresh();
    }

    void Start()
    {
        // Segundo intento: si RankingManager aun no existia en OnEnable, aqui ya existe.
        Subscribe();
        Refresh();
    }

    public void Refresh()
    {
        // Si falta algo, no hacemos nada (y no borramos lo que haya en Content).
        if (contentParent == null || rowPrefab == null)
        {
            Debug.LogWarning("RankingUI: falta asignar Content Parent o Row Prefab.");
            return;
        }
        if (RankingManager.Instance == null) return;

        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        var entries = RankingManager.Instance.data.entries;
        for (int i = 0; i < Mathf.Min(entries.Count, 5); i++)
        {
            var row = Instantiate(rowPrefab, contentParent);
            row.GetComponent<RankingRowUIs>().SetData(i + 1, entries[i]);
        }
    }


    void OnDisable()
    {
        if (RankingManager.Instance != null)
            RankingManager.Instance.OnChanged -= Refresh;
    }

    void Subscribe()
    {
        if (RankingManager.Instance == null) return;
        RankingManager.Instance.OnChanged -= Refresh; // evita duplicados
        RankingManager.Instance.OnChanged += Refresh;
    }
}