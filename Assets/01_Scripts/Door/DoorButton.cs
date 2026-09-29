using UnityEngine;

public class DoorButton : MonoBehaviour, IShootable
{
    [Header("Referencias")]
    [SerializeField] DoorController doorController;

    [Header("Puntos")]
    [SerializeField] int pointsOnOpen = 10;

    bool pointsGiven;

    /// <summary>
    /// abre la puerta al recibir un disparo
    /// </summary>
    public void OnShot()
    {
        doorController.Open();

        if (pointsGiven) return;
        pointsGiven = true;

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.AddPoints(pointsOnOpen);
            Debug.Log($"[Puntos] Puerta abierta: +{pointsOnOpen} pts | Total: {ScoreManager.Instance.CurrentPoints}");
        }
        else
        {
            Debug.LogWarning("[Puntos] ScoreManager.Instance es null: no se sumaron puntos.");
        }
    }
}