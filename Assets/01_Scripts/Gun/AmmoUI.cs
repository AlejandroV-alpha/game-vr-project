using TMPro;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] TextMeshProUGUI ammoValue;

    [Header("Configuracion")]
    [SerializeField] int lowAmmoThreshold = 3;

    [Header("Colores")]
    [SerializeField] Color normalAmmoColor = Color.white;
    [SerializeField] Color lowAmmoColor = Color.yellow;
    [SerializeField] Color emptyAmmoColor = Color.red;

    /// <summary>
    /// actualiza el texto y el color de la municion
    /// </summary>
    public void UpdateAmmo(int currentAmmo, int maxAmmo)
    {
        ammoValue.text = currentAmmo + " / " + maxAmmo;

        if (currentAmmo <= 0)
        {
            ammoValue.color = emptyAmmoColor;
            return;
        }

        if (currentAmmo <= lowAmmoThreshold)
        {
            ammoValue.color = lowAmmoColor;
            return;
        }

        ammoValue.color = normalAmmoColor;
    }
}
