using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public InputActionReference shootAction;
    public float range = 100f;

    /// <summary>
    /// revisa si se presiono el gatillo
    /// </summary>
    void Update()
    {
        if (shootAction.action.WasPressedThisFrame())
        {
            Shoot();
        }
    }

    /// <summary>
    /// dispara un raycast desde el firepoint
    /// </summary>
    public void Shoot()
    {
        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("impacto: " + hit.collider.name);
        }
    }
}
