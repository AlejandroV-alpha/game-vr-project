using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float tileSpeed = 5f;

    void Awake()
    {
        instance = this;
    }
}
