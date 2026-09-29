using UnityEngine;

public class Hazard : MonoBehaviour
{
    [Header("Daño")]
    [SerializeField] int damage = 1;

    bool hasHitPlayer;

    /// <summary>
    /// aplica daño cuando el obstaculo toca al jugador
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (hasHitPlayer)
        {
            return;
        }

        if (!other.CompareTag("Player"))
        {
            return;
        }

        ITakeDamage damageable = other.GetComponentInParent<ITakeDamage>();

        if (damageable == null)
        {
            return;
        }

        damageable.TakeDamage(damage);
        hasHitPlayer = true;
    }
}
