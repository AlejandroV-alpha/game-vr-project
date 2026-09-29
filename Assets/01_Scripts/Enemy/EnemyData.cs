using UnityEngine;

[CreateAssetMenu(menuName = "Enemy/Enemy Data", fileName = "Enemy_")]
public class EnemyData : ScriptableObject
{
    [Header("Estadisticas")]
    public int pointsOnDeath = 5;
    public int baseHealth;
    public int damage;

    [Header("Disparo")]
    public float firstShotDelay;
    public float fireCooldown;
    public float projectileSpeed;
    public float attackRange;
}
