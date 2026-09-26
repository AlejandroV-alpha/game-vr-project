using UnityEngine;

public class DroneController : MonoBehaviour
{
    [Header("Datos")]
    [SerializeField] DroneData droneData;

    Vector3 initialPosition;
    Vector3 targetPosition;

    Transform player;

    /// <summary>
    /// guarda la posicion inicial y obtiene al jugador
    /// </summary>
    void Start()
    {
        initialPosition = transform.localPosition;

        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            player = mainCamera.transform;
        }

        ChooseNewTarget();
    }

    /// <summary>
    /// mueve el dron y lo orienta hacia el jugador
    /// </summary>
    void Update()
    {
        MoveDrone();
        RotateTowardsPlayer();
    }

    /// <summary>
    /// mueve el dron dentro de sus limites
    /// </summary>
    void MoveDrone()
    {
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition,
            targetPosition,
            droneData.movementSpeed * Time.deltaTime
        );

        float distance = Vector3.Distance(
            transform.localPosition,
            targetPosition
        );

        if (distance <= 0.05f)
        {
            ChooseNewTarget();
        }
    }

    /// <summary>
    /// elige una nueva posicion aleatoria dentro de los limites
    /// </summary>
    void ChooseNewTarget()
    {
        float randomX = Random.Range(
            -droneData.horizontalRange,
            droneData.horizontalRange
        );

        float randomY = Random.Range(
            -droneData.verticalRange,
            droneData.verticalRange
        );

        targetPosition = new Vector3(
            initialPosition.x + randomX,
            initialPosition.y + randomY,
            initialPosition.z
        );
    }

    /// <summary>
    /// gira horizontalmente hacia el jugador
    /// </summary>
    void RotateTowardsPlayer()
    {
        if (player == null)
        {
            return;
        }

        Vector3 direction = player.position - transform.position;

        direction.y = 0f;

        if (direction.sqrMagnitude <= 0.001f)
        {
            return;
        }

        Quaternion targetRotation = Quaternion.LookRotation(direction);

        transform.rotation = Quaternion.RotateTowards(
            transform.rotation,
            targetRotation,
            droneData.rotationSpeed * Time.deltaTime
        );
    }
}
