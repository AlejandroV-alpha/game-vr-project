using UnityEngine;

public class TurretController : MonoBehaviour
{
    [Header("Datos")]
    [SerializeField] TurretData turretData;

    [Header("Referencias")]
    [SerializeField] Transform horizontalPivot;
    [SerializeField] Transform verticalPivot;

    Transform player;

    Quaternion initialHorizontalRotation;
    Quaternion initialVerticalRotation;

    /// <summary>
    /// obtiene al jugador y guarda las rotaciones iniciales
    /// </summary>
    void Start()
    {
        Camera mainCamera = Camera.main;

        if (mainCamera != null)
        {
            player = mainCamera.transform;
        }

        initialHorizontalRotation = horizontalPivot.localRotation;
        initialVerticalRotation = verticalPivot.localRotation;
    }

    /// <summary>
    /// hace que la torreta siga al jugador
    /// </summary>
    void Update()
    {
        RotateHorizontal();
        RotateVertical();
    }

    /// <summary>
    /// gira el cuello hacia el jugador respetando el limite
    /// </summary>
    void RotateHorizontal()
    {
        if (player == null)
        {
            return;
        }

        Vector3 direction = player.position - horizontalPivot.position;
        Vector3 localDirection = transform.InverseTransformDirection(direction);

        float targetAngle = Mathf.Atan2(
            localDirection.x,
            localDirection.z
        ) * Mathf.Rad2Deg;

        float halfLimit = turretData.horizontalRotationLimit / 2f;

        targetAngle = Mathf.Clamp(
            targetAngle,
            -halfLimit,
            halfLimit
        );

        Quaternion targetRotation =
            initialHorizontalRotation *
            Quaternion.Euler(0f, targetAngle, 0f);

        horizontalPivot.localRotation = Quaternion.RotateTowards(
            horizontalPivot.localRotation,
            targetRotation,
            turretData.horizontalRotationSpeed * Time.deltaTime
        );
    }

    /// <summary>
    /// gira la cabeza hacia el jugador respetando los limites
    /// </summary>
    void RotateVertical()
    {
        if (player == null)
        {
            return;
        }

        Vector3 direction = player.position - verticalPivot.position;

        Vector3 localDirection =
            horizontalPivot.InverseTransformDirection(direction);

        float horizontalDistance = Mathf.Sqrt(
            localDirection.x * localDirection.x +
            localDirection.z * localDirection.z
        );

        float targetAngle = Mathf.Atan2(
            localDirection.y,
            horizontalDistance
        ) * Mathf.Rad2Deg;

        targetAngle = -targetAngle;

        targetAngle = Mathf.Clamp(
            targetAngle,
            turretData.minVerticalAngle,
            turretData.maxVerticalAngle
        );

        Quaternion targetRotation =
            initialVerticalRotation *
            Quaternion.Euler(targetAngle, 0f, 0f);

        verticalPivot.localRotation = Quaternion.RotateTowards(
            verticalPivot.localRotation,
            targetRotation,
            turretData.verticalRotationSpeed * Time.deltaTime
        );
    }
}
