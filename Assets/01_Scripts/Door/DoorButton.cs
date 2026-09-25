using UnityEngine;

public class DoorButton : MonoBehaviour, IShootable
{
    [Header("Referencias")]
    [SerializeField] DoorController doorController;

    /// <summary>
    /// abre la puerta al recibir un disparo
    /// </summary>
    public void OnShot()
    {
        doorController.Open();
    }
}
