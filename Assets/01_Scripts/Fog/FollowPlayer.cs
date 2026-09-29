using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [Tooltip("Arrastra aquí la Main Camera (o el XR Origin).")]
    [SerializeField] Transform target;

    [Tooltip("Altura fija de la niebla (la altura del piso).")]
    [SerializeField] float floorHeight = 0.3f;

    void LateUpdate()
    {
        if (target == null) return;

        transform.position = new Vector3(target.position.x, floorHeight, target.position.z);
        transform.rotation = Quaternion.identity;
    }
}