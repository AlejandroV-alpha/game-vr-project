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
            nextShotTime = Time.time + enemyData.fireCooldown;
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
        Vector3 direction =
            (player.position - firePoint.position).normalized;

        float distanceToPlayer = Vector3.Distance(
            firePoint.position,
            player.position
        );

        float maxDistance = distanceToPlayer + 1.5f;

        float projectileSpeedBonus = DifficultyManager.instance.GetProjectileSpeedBonus();

        float currentProjectileSpeed = enemyData.projectileSpeed + projectileSpeedBonus;

        audioSource.PlayOneShot(shootSound);

        EnemyProjectile projectile = Instantiate(
            projectilePrefab,
            firePoint.position,
            firePoint.rotation
        );

        projectile.Initialize(
            direction,
            currentProjectileSpeed,
            firePoint.position,
            maxDistance,
            enemyData.damage
        );
    }
}
