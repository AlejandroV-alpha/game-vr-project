using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Turret Data", fileName = "Turret_")]
public class TurretData : EnemyData
{
    [Header("Rotacion horizontal")]
    public float horizontalRotationLimit;
    public float horizontalRotationSpeed;

    [Header("Rotacion vertical")]
    public float minVerticalAngle;
    public float maxVerticalAngle;
    public float verticalRotationSpeed;
}
