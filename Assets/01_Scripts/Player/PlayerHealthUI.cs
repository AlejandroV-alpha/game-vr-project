using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Image healthBarFill;

    /// <summary>
    /// actualiza la barra segun la vida del jugador
    /// </summary>
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        float healthPercent = (float)currentHealth / maxHealth;

        healthBarFill.fillAmount = healthPercent;
    }
}
