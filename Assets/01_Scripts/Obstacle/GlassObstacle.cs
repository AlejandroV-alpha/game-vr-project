using UnityEngine;

public class GlassObstacle : MonoBehaviour
{
    [Header("Vida del vidrio")]
    public int vida = 1; // cuántos disparos aguanta antes de romperse

    [Header("Daño al jugador (si lo toca sin romperlo)")]
    public int daño = 1;

    [Header("Efectos al romperse")]
    public GameObject efectoRotura;   // prefab de partículas / fragmentos (opcional)
    public AudioClip sonidoRotura;    // sonido de vidrio roto (opcional)

    [Header("Tags esperados en la escena")]
    public string tagBala = "Bullet";
    public string tagJugador = "Player";

    private bool roto = false;
    private Collider col;
    private Renderer rend;

    void Awake()
    {
        col = GetComponent<Collider>();
        rend = GetComponent<Renderer>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (roto) return;

        if (other.CompareTag(tagBala))
        {
            RecibirDisparo();
            Destroy(other.gameObject); // la bala se consume al impactar
        }
        else if (other.CompareTag(tagJugador))
        {
            // El vidrio sigue intacto: si el jugador no lo esquivó ni lo rompió, le pega
            other.SendMessage("TakeDamage", daño, SendMessageOptions.DontRequireReceiver);
        }
    }

    void RecibirDisparo()
    {
        vida--;

        if (vida <= 0)
        {
            Romper();
        }
    }

    void Romper()
    {
        roto = true;

        if (efectoRotura != null)
            Instantiate(efectoRotura, transform.position, transform.rotation);

        if (sonidoRotura != null)
            AudioSource.PlayClipAtPoint(sonidoRotura, transform.position);

        // Ya no bloquea ni hace daño: desaparece del camino
        if (col != null) col.enabled = false;
        if (rend != null) rend.enabled = false;

        Destroy(gameObject, 0.15f); // pequeño margen para que se escuche/vea el efecto
    }
}
