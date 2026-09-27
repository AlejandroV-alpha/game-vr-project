using UnityEngine;

public class DronePropellerRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 1500f;
    [SerializeField] private bool reverseDirection = false;

    private void Update()
    {
        float direction = reverseDirection ? -1f : 1f;

        transform.Rotate(
            Vector3.up,
            rotationSpeed * direction * Time.deltaTime,
            Space.World
        );
    }
}