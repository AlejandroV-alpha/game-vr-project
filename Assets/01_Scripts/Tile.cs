using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Tile : MonoBehaviour
{
    public float zLimitToDestroy = -30f;

    Rigidbody rb;

    /// <summary>
    /// configura el rigidbody del tile
    /// </summary>
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;
        rb.useGravity = false;
    }

    /// <summary>
    /// mueve el tile y lo destruye al pasar el limite
    /// </summary>
    void FixedUpdate()
    {
        float moveSpeed = GameManager.instance.tileSpeed;

        Vector3 movement = Vector3.back * moveSpeed * Time.fixedDeltaTime;

        rb.MovePosition(rb.position + movement);

        if (rb.position.z < zLimitToDestroy)
        {
            Destroy(gameObject);
        }
    }
}
