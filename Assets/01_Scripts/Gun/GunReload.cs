using UnityEngine;

[RequireComponent(typeof(Gun))]
public class GunReload : MonoBehaviour
{
    [Header("Recarga")]
    [SerializeField] float reloadSpeed = 15f;

    [Range(0f, 90f)]
    [SerializeField] float maxAngleFromDown = 40f;

    [SerializeField] float reloadCooldown = 0.5f;

    Gun gun;
    Vector3 previousPosition;
    float nextReloadTime;

    /// <summary>
    /// obtiene el arma y guarda la posicion inicial
    /// </summary>
    void Start()
    {
        gun = GetComponent<Gun>();
        previousPosition = transform.position;
    }

    /// <summary>
    /// detecta un movimiento rapido principalmente hacia abajo
    /// </summary>
    void Update()
    {
        Vector3 movement = transform.position - previousPosition;
        Vector3 velocity = movement / Time.deltaTime;

        float speed = velocity.magnitude;
        float angleFromDown = Vector3.Angle(velocity, Vector3.down);

        if (speed >= reloadSpeed && angleFromDown <= maxAngleFromDown && Time.time >= nextReloadTime)
        {
            gun.Reload();
            nextReloadTime = Time.time + reloadCooldown;
        }

        previousPosition = transform.position;
    }
}
