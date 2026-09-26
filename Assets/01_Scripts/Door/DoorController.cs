using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Transform leftDoor;
    [SerializeField] Transform rightDoor;

    [Header("Apertura")]
    [SerializeField] float openDistance = 4f;
    [SerializeField] float openSpeed = 4f;

    [Header("Audio")]
    [SerializeField] AudioClip openSound;

    Vector3 leftClosedPosition;
    Vector3 rightClosedPosition;

    Vector3 leftOpenPosition;
    Vector3 rightOpenPosition;

    AudioSource audioSource;

    bool isOpen;

    /// <summary>
    /// obtiene los componentes necesarios
    /// </summary>
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    /// <summary>
    /// guarda las posiciones de la puerta
    /// </summary>
    void Start()
    {
        leftClosedPosition = leftDoor.localPosition;
        rightClosedPosition = rightDoor.localPosition;

        leftOpenPosition = leftClosedPosition + Vector3.left * openDistance;
        rightOpenPosition = rightClosedPosition + Vector3.right * openDistance;
    }

    /// <summary>
    /// mueve las puertas cuando estan abiertas
    /// </summary>
    void Update()
    {
        if (!isOpen)
        {
            return;
        }

        leftDoor.localPosition = Vector3.MoveTowards(leftDoor.localPosition, leftOpenPosition, openSpeed * Time.deltaTime);
        rightDoor.localPosition = Vector3.MoveTowards(rightDoor.localPosition, rightOpenPosition, openSpeed * Time.deltaTime);
    }

    /// <summary>
    /// abre las dos puertas
    /// </summary>
    public void Open()
    {
        if (isOpen)
        {
            return;
        }

        isOpen = true;
        audioSource.PlayOneShot(openSound);
    }
}
