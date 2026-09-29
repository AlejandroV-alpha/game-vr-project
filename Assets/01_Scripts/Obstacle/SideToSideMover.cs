using UnityEngine;

public class SideToSideMover : MonoBehaviour
{
    [Header("Movimiento lateral")]
    public float distancia = 2f;   // cuánto se aleja hacia cada lado desde el punto inicial
    public float velocidad = 2f;   // qué tan rápido oscila

    [Header("Eje de movimiento (local al objeto/padre)")]
    public bool usarEjeX = true;   // true = mueve en X local, false = mueve en Z local

    private Vector3 posicionInicial;

    void OnEnable()
    {
        // Se toma en OnEnable (no en Start) por si el objeto se recicla/reposiciona (pooling)
        posicionInicial = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * velocidad) * distancia;

        Vector3 direccion = usarEjeX ? Vector3.right : Vector3.forward;
        transform.localPosition = posicionInicial + direccion * offset;
    }
}
