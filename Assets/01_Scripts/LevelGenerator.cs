using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;

    public ZoneData[] zones;
    public GameObject lastTile;
    public int tilesPerZone = 5;
    public float segmentLength = 20f;

    ZoneData currentZone;
    int currentZoneIndex = -1;
    int tilesGeneratedInCurrentZone;

    /// <summary>
    /// guarda la instancia del generador
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// genera el siguiente tile
    /// </summary>
    public void GenerateNextTile()
    {
        if (ShouldChangeZone())
        {
            SelectNextZone();
        }

        GameObject prefab = SelectRandomPrefab();
        GameObject newTile = CreateTile(prefab);

        lastTile = newTile;
        tilesGeneratedInCurrentZone++;
    }

    /// <summary>
    /// indica si se debe cambiar de zona
    /// </summary>
    bool ShouldChangeZone()
    {
        return currentZone == null || tilesGeneratedInCurrentZone >= tilesPerZone;
    }

    /// <summary>
    /// selecciona una nueva zona diferente a la anterior
    /// </summary>
    void SelectNextZone()
    {
        int newZoneIndex = Random.Range(0, zones.Length);

        if (zones.Length > 1)
        {
            while (newZoneIndex == currentZoneIndex)
            {
                newZoneIndex = Random.Range(0, zones.Length);
            }
        }

        currentZoneIndex = newZoneIndex;
        currentZone = zones[currentZoneIndex];
        tilesGeneratedInCurrentZone = 0;

        Debug.Log("nueva zona: " + currentZone.Name);
    }

    /// <summary>
    /// selecciona un prefab aleatorio de la zona actual
    /// </summary>
    GameObject SelectRandomPrefab()
    {
        int randomIndex = Random.Range(0, currentZone.Prefabs.Length);

        return currentZone.Prefabs[randomIndex];
    }

    /// <summary>
    /// crea un tile despues del ultimo tile
    /// </summary>
    GameObject CreateTile(GameObject prefab)
    {
        float positionZ = lastTile.transform.position.z + segmentLength;
        Vector3 position = new Vector3(0f, -0.1f, positionZ);

        return Instantiate(prefab, position, Quaternion.identity);
    }
}