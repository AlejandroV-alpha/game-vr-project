using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Image healthBarFill;
    [SerializeField] TMP_Text healthValue;

    [Header("Colores")]
    [SerializeField] Color highHealthColor = Color.green;
    [SerializeField] Color mediumHealthColor = Color.yellow;
    [SerializeField] Color lowHealthColor = Color.red;

    /// <summary>
    /// actualiza la barra segun la vida del jugador
    /// </summary>
    public void UpdateHealth(int currentHealth, int maxHealth)
    {
        float healthPercent = (float)currentHealth / maxHealth;

        healthBarFill.fillAmount = healthPercent;
        healthValue.text = currentHealth + " / " + maxHealth;

        if (healthPercent > 0.6f)
        {
            healthBarFill.color = highHealthColor;
        }
        else if (healthPercent > 0.3f)
        {
            healthBarFill.color = mediumHealthColor;
        }
        else
        {
            healthBarFill.color = lowHealthColor;
        }
    }
}
