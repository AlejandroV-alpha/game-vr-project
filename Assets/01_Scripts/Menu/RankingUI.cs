using UnityEngine;

public class RankingUI : MonoBehaviour
{
    [Tooltip("El Transform padre donde se instancian las filas (el objeto 'Content').")]
    public Transform contentParent;

    [Tooltip("El Prefab de la fila (RankingRow). Debe ser un PREFAB del Project, no un objeto de la escena.")]
    public GameObject rowPrefab;

    [Tooltip("Cuántas filas mostrar (0 = todas).")]
    public int maxRows = 10;

    void OnEnable()
    {
        Subscribe();
        Refresh();
    }

    void Start()
    {
        Subscribe();
        Refresh();
    }

    public void Refresh()
    {
        if (contentParent == null || rowPrefab == null)
        {
            Debug.LogWarning("RankingUI: falta asignar Content Parent o Row Prefab.");
            return;
        }
        if (RankingManager.Instance == null) return;

        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        var entries = RankingManager.Instance.data.entries;
        int count = maxRows > 0 ? Mathf.Min(entries.Count, maxRows) : entries.Count;

        for (int i = 0; i < count; i++)
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
        RankingManager.Instance.OnChanged -= Refresh;
        RankingManager.Instance.OnChanged += Refresh;
    }
}