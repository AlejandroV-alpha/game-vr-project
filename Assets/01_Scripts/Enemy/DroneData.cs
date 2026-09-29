using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Drone Data", fileName = "Drone_")]
public class DroneData : EnemyData
{
    [Header("Movimiento")]
    public float movementSpeed;
    public float horizontalRange;
    public float verticalRange;

    [Header("Rotacion")]
    public float rotationSpeed;
}
