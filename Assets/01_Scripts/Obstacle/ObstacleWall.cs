using UnityEngine;

public class ObstacleWall : MonoBehaviour
{
    [Header("Ciclo de vida")]
    [Tooltip("Segundos antes de autodestruirse. Poner 0 para desactivar la autodestruccion.")]
    [SerializeField] float lifeTime = 0f;

    void Start()
    {
        if (lifeTime > 0f)
        {
            Destroy(gameObject, lifeTime);
        }
    }
}