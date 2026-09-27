using UnityEngine;

public class ObstacleReveal : MonoBehaviour
{
    [Header("Que se revela")]
    [Tooltip("Renderers que se activan cuando el obstaculo se hace visible (la malla de la pared, por ejemplo)")]
    [SerializeField] Renderer[] renderersToReveal;

    [Header("Opciones")]
    [SerializeField] bool revealOnce = true;

    bool alreadyRevealed = false;

    void Reset()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    void Awake()
    {
        SetVisible(false);
    }

    void OnEnable()
    {
        alreadyRevealed = false;
        SetVisible(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (revealOnce && alreadyRevealed) return;

        if (!other.CompareTag("Player")) return;

        SetVisible(true);
        alreadyRevealed = true;
    }

    void SetVisible(bool visible)
    {
        foreach (Renderer rend in renderersToReveal)
        {
            if (rend != null)
            {
                rend.enabled = visible;
            }
        }
    }
}
