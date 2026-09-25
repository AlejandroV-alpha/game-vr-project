using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    public static DifficultyManager instance;

    [Header("Vida")]
    [SerializeField] int stagesPerHealthIncrease = 3;
    [SerializeField] int maxHealthBonus = 2;

    [Header("Velocidad de proyectiles")]
    [SerializeField] float projectileSpeedIncreasePerStage = 0.25f;
    [SerializeField] float maxProjectileSpeedBonus = 2f;

    [Header("Estado")]
    [SerializeField] int currentStage = 1;

    /// <summary>
    /// guarda la instancia del manager
    /// </summary>
    void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// aumenta la etapa de dificultad
    /// </summary>
    public void IncreaseStage()
    {
        currentStage++;

        Debug.Log("nivel de dificultad: " + currentStage);
    }

    /// <summary>
    /// devuelve la etapa actual
    /// </summary>
    public int GetCurrentStage()
    {
        return currentStage;
    }

    /// <summary>
    /// devuelve el aumento de vida segun la etapa
    /// </summary>
    public int GetHealthBonus()
    {
        int completedStages = currentStage - 1;

        int healthBonus = completedStages / stagesPerHealthIncrease;

        return Mathf.Min(healthBonus, maxHealthBonus);
    }

    /// <summary>
    /// devuelve el aumento de velocidad de los proyectiles
    /// </summary>
    public float GetProjectileSpeedBonus()
    {
        int completedStages = currentStage - 1;

        float speedBonus = completedStages * projectileSpeedIncreasePerStage;

        return Mathf.Min(speedBonus, maxProjectileSpeedBonus);
    }
}
