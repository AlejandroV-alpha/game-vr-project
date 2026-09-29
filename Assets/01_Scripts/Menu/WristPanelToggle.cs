using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class WristPanelToggle : MonoBehaviour
{
    [Tooltip("La camara del jugador (Main Camera).")]
    [SerializeField] private Transform head;

    [Tooltip("Que tan de frente hay que mirar la muneca para que aparezca (grados).")]
    [Range(10f, 90f)]
    [SerializeField] private float maxAngle = 40f;

    [Tooltip("Marcalo si el panel aparece cuando NO lo miras (cara invertida).")]
    [SerializeField] private bool invertir = false;

    private CanvasGroup grupo;

    private void Awake()
    {
        grupo = GetComponent<CanvasGroup>();
        if (head == null && Camera.main != null) head = Camera.main.transform;
    }

    private void Update()
    {
        if (head == null) return;

        Vector3 haciaCabeza = (head.position - transform.position).normalized;

        Vector3 caraDelPanel = invertir ? transform.forward : -transform.forward;

        bool mirando = Vector3.Angle(caraDelPanel, haciaCabeza) < maxAngle;
        grupo.alpha = mirando ? 1f : 0f;
    }
}