using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Rigidbody rb;
    LineRenderer lineRenderer;

    Vector3 direction;
    Vector3 startPoint;

    float speed;
    float maxDistance;
    int damage;
    bool initialized;

    /// <summary>
    /// obtiene los componentes del proyectil
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lineRenderer = GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
    }

    /// <summary>
    /// recibe los datos necesarios para iniciar el disparo
    /// </summary>
    public void Initialize(
        Vector3 shootDirection,
        float projectileSpeed,
        Vector3 shootPoint,
        float projectileMaxDistance,
        int projectileDamage
    )
    {
        direction = shootDirection.normalized;

        transform.rotation = Quaternion.LookRotation(direction);

        speed = projectileSpeed;
        startPoint = shootPoint;
        maxDistance = projectileMaxDistance;
        damage = projectileDamage;

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, transform.position);

        initialized = true;
    }

    /// <summary>
    /// mueve el proyectil en linea recta
    /// </summary>
    void FixedUpdate()
    {
        if (!initialized)
        {
            return;
        }

        Vector3 nextPosition = rb.position + direction * speed * Time.fixedDeltaTime;

        rb.MovePosition(nextPosition);

        CheckDistance();
    }

    /// <summary>
    /// destruye el proyectil cuando supera su distancia maxima
    /// </summary>
    void CheckDistance()
    {
        float traveledDistance = Vector3.Distance(
            startPoint,
            rb.position
        );

        if (traveledDistance >= maxDistance)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// actualiza el extremo del laser
    /// </summary>
    void LateUpdate()
    {
        if (!initialized)
        {
            return;
        }

        lineRenderer.SetPosition(0, startPoint);
        lineRenderer.SetPosition(1, transform.position);
    }

    /// <summary>
    /// detecta cuando el proyectil golpea al jugador
    /// </summary>
    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }

        ITakeDamage damageable =
            other.GetComponentInParent<ITakeDamage>();

        if (damageable != null)
        {
            damageable.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
