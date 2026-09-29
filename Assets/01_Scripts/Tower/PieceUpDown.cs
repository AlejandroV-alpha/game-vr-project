using UnityEngine;

public class PieceUpDown : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private bool canMove = true;
    [SerializeField] private float maxHeight = 2f;
    [SerializeField] private float speed = 1f;

    private Vector3 startPosition;
    private float randomOffset;

    void Start()
    {
        startPosition = transform.localPosition;
        randomOffset = Random.Range(0f, 10f);
    }

    void Update()
    {
        if (!canMove)
            return;

        float offset = Mathf.PingPong(
            (Time.time + randomOffset) * speed,
            maxHeight
        );

        transform.localPosition =
            startPosition + Vector3.up * offset;
    }
}