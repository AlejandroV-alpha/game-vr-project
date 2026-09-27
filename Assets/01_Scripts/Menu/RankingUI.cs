using UnityEngine;

public class RankingUI : MonoBehaviour
{
    [Tooltip("El Transform padre donde se van a instanciar las filas (ej: el 'Content' del ScrollView).")]
    public Transform contentParent;

    [Tooltip("El Prefab de la fila (RankingRowPrefab).")]
    public GameObject rowPrefab;

    void OnEnable()
    {
        Refresh();
    }

    public void Refresh()
    {
        foreach (Transform child in contentParent)
            Destroy(child.gameObject);

        var entries = RankingManager.Instance.data.entries;
        for (int i = 0; i < entries.Count; i++)
        {
            var row = Instantiate(rowPrefab, contentParent);
            row.GetComponent<RankingRowUIs>().SetData(i + 1, entries[i]);
        }
    }
}