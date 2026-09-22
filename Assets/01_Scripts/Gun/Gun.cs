using UnityEngine;
using UnityEngine.InputSystem;

public class Gun : MonoBehaviour
{
    public Transform firePoint;
    public InputActionReference shootAction;

    public float range = 100f;

    public int magazineSize = 6;
    public int currentAmmo;

    /// <summary>
    /// carga la municion inicial
    /// </summary>
    void Start()
    {
        currentAmmo = magazineSize;
    }

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
    /// dispara un raycast y consume una bala
    /// </summary>
    public void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("sin municion");
            return;
        }

        currentAmmo--;

        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            Debug.Log("impacto: " + hit.collider.name);
        }

        Debug.Log("municion: " + currentAmmo + "/" + magazineSize);
    }

    /// <summary>
    /// recarga el cargador
    /// </summary>
    public void Reload()
    {
        if (currentAmmo >= magazineSize)
        {
            return;
        }

        currentAmmo = magazineSize;

        Debug.Log("arma recargada: " + currentAmmo + "/" + magazineSize);
    }
}
