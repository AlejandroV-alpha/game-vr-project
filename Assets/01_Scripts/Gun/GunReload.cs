using UnityEngine;

[RequireComponent(typeof(Gun))]
public class GunReload : MonoBehaviour
{
    public float reloadSpeed = 15f;
    public float maxAngleFromDown = 40f;
    public float reloadCooldown = 0.5f;

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
