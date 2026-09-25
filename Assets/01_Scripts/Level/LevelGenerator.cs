using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;

    [Header("Zonas")]
    public ZoneData[] zones;

    [Header("Generacion")]
    public GameObject lastTile;
    public int tilesPerZone = 5;
    public float segmentLength = 40f;

    ZoneData currentZone;
    int currentZoneIndex = -1;
    int tilesGeneratedInCurrentZone;
    bool transitionGenerated;

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
        if (currentZone == null)
        {
            SelectNextZone();
            GenerateStartTile();
            return;
        }

        if (transitionGenerated)
        {
            DifficultyManager.instance.IncreaseStage();
            SelectNextZone();
            GenerateStartTile();
            transitionGenerated = false;
            return;
        }

        if (tilesGeneratedInCurrentZone < tilesPerZone)
        {
            GenerateNormalTile();
            return;
        }

        GenerateTransitionTile();
        transitionGenerated = true;
    }

    /// <summary>
    /// genera el tile inicial fijo de la zona
    /// </summary>
    void GenerateStartTile()
    {
        GameObject newTile = CreateTile(currentZone.StartPrefab);

        lastTile = newTile;
    }

    /// <summary>
    /// genera un tile aleatorio de la zona actual
    /// </summary>
    void GenerateNormalTile()
    {
        GameObject prefab = SelectRandomPrefab();

        GameObject newTile = CreateTile(prefab);

        lastTile = newTile;
        tilesGeneratedInCurrentZone++;
    }

    /// <summary>
    /// genera el tile final de la zona
    /// </summary>
    void GenerateTransitionTile()
    {
        GameObject newTile = CreateTile(currentZone.TransitionPrefab);

        lastTile = newTile;
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