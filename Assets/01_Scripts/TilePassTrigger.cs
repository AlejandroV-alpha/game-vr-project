using UnityEngine;

public class TilePassTrigger : MonoBehaviour
{
    bool triggered;

    /// <summary>
    /// detecta cuando el jugador atraviesa el trigger y genera el siguiente tile
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        if (triggered)
        {
            return;
        }

        triggered = true;

        LevelGenerator.instance.GenerateNextTile();
    }
}
