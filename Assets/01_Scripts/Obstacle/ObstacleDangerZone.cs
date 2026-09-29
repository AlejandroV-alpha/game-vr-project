using UnityEngine;

public class ObstacleDangerZone : MonoBehaviour
{
    [Header("Daño")]
    [SerializeField] int damage = 1;

    [Header("Opciones")]
    [Tooltip("Si esta activo, esta zona solo puede golpear una vez por activacion")]
    [SerializeField] bool hitOnce = true;

    bool alreadyHit = false;

    /// <summary>
    /// Se ejecuta al agregar el componente en el editor: fuerza el collider a Trigger
    /// </summary>
    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    /// <summary>
    /// Util si reutilizas el obstaculo con object pooling en vez de Instantiate/Destroy
    /// </summary>
    void OnEnable()
    {
        alreadyHit = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (hitOnce && alreadyHit) return;

        ITakeDamage target = other.GetComponentInParent<ITakeDamage>();

        if (target == null) return;

        target.TakeDamage(damage);
        alreadyHit = true;
    }
}
