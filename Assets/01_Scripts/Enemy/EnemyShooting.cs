using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    [Header("Datos")]
    [SerializeField] EnemyData enemyData;

    [Header("Referencias")]
    [SerializeField] Transform firePoint;
    [SerializeField] EnemyProjectile projectilePrefab;

    [Header("Audio")]
    [SerializeField] AudioClip shootSound;

    Transform player;
    AudioSource audioSource;

    float nextShotTime;
    bool playerInRange;

    /// <summary>
    /// obtiene los componentes necesarios
    /// </summary>
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// obtiene al jugador
    /// </summary>
    void Start()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            player = mainCamera.transform;
        }
    }

    /// <summary>
    /// controla el rango y el tiempo entre disparos
    /// </summary>
    void Update()
    {
        if (player == null)
        {
            return;
        }

        if (!IsPlayerInFront())
        {
            playerInRange = false;
            return;
        }

        float distanceToPlayer = Vector3.Distance(
            transform.position,
            player.position
        );

        if (distanceToPlayer > enemyData.attackRange)
        {
            playerInRange = false;
            return;
        }

        if (!playerInRange)
        {
            playerInRange = true;

            nextShotTime = Time.time + enemyData.firstShotDelay;

            return;
        }

        if (Time.time >= nextShotTime)
        {
            Shoot();

            nextShotTime = Time.time + enemyData.fireCooldown;
        }
    }

    /// <summary>
    /// revisa si el enemigo sigue delante del jugador
    /// </summary>
    bool IsPlayerInFront()
    {
        if (transform.position.z <= player.position.z)
        {
            return false;
        }

        return true;
    }

    /// <summary>
    /// crea un proyectil dirigido hacia la posicion actual del jugador
    /// </summary>
    void Shoot()
    {
        float distanceToPlayer = Vector3.Distance(
            firePoint.position,
            player.position
        );

        float maxDistance = distanceToPlayer + 1.5f;

        float projectileSpeedBonus = DifficultyManager.instance.GetProjectileSpeedBonus();

        float currentProjectileSpeed = enemyData.projectileSpeed + projectileSpeedBonus;

        Vector3 enemyVelocity = Vector3.back * GameManager.instance.tileSpeed;

        Vector3 direction =
            CalculateShootDirection(
                currentProjectileSpeed,
                enemyVelocity
            );

        audioSource.PlayOneShot(shootSound);

        EnemyProjectile projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        projectile.Initialize(
            direction,
            currentProjectileSpeed,
            enemyVelocity,
            firePoint.position,
            maxDistance,
            enemyData.damage
        );
    }

    /// <summary>
    /// calcula la direccion necesaria para alcanzar al jugador
    /// </summary>
    Vector3 CalculateShootDirection(
        float projectileSpeed,
        Vector3 enemyVelocity
    )
    {
        // distancia y direccion desde el arma hasta el jugador
        Vector3 relativePosition =
            player.position - firePoint.position;


        // como el enemigo se esta moviendo,
        // desde su punto de vista el jugador parece moverse
        // en la direccion contraria
        Vector3 targetRelativeVelocity =
            -enemyVelocity;


        // estos tres valores se usan para calcular
        // cuanto tiempo tardaria la bala en alcanzar al jugador
        float a =
            targetRelativeVelocity.sqrMagnitude -
            projectileSpeed * projectileSpeed;

        float b =
            2f * Vector3.Dot(
                relativePosition,
                targetRelativeVelocity
            );

        float c =
            relativePosition.sqrMagnitude;


        // revisa si existe un tiempo valido
        // en el que la bala pueda alcanzar al jugador
        float discriminant =
            b * b - 4f * a * c;

        if (discriminant < 0f)
        {
            // si no encontramos una solucion,
            // simplemente dispara directamente hacia el jugador
            return relativePosition.normalized;
        }


        // obtenemos la raiz que necesitamos
        // para calcular los posibles tiempos de impacto
        float squareRoot =
            Mathf.Sqrt(discriminant);


        // pueden existir dos tiempos posibles
        // en los que la bala podria llegar al jugador
        float time1 =
            (-b - squareRoot) / (2f * a);

        float time2 =
            (-b + squareRoot) / (2f * a);


        // inicialmente no tenemos ningun tiempo valido
        float timeToHit = -1f;


        // si el primer tiempo ocurre en el futuro, lo usamos
        if (time1 > 0f)
        {
            timeToHit = time1;
        }


        // si el segundo tiempo tambien sirve,
        // elegimos el que ocurra primero
        if (time2 > 0f)
        {
            if (timeToHit < 0f || time2 < timeToHit)
            {
                timeToHit = time2;
            }
        }


        // si ninguno de los tiempos sirve,
        // dispara directamente hacia el jugador
        if (timeToHit <= 0f)
        {
            return relativePosition.normalized;
        }


        // calculamos hacia donde debemos apuntar
        // teniendo en cuenta cuanto se moveran
        // el enemigo y el jugador durante ese tiempo
        Vector3 compensatedPosition =
            relativePosition +
            targetRelativeVelocity * timeToHit;


        // devolvemos solamente la direccion final
        return compensatedPosition.normalized;
    }
}
