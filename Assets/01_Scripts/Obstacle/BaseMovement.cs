using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class BaseMovement : MonoBehaviour
{
    [Header("Movimiento")]
    public float velocidad = 8f;
    public float suavizado = 10f;

    [Header("Límites del carril (eje X)")]
    public float limiteIzquierdo = -3f;
    public float limiteDerecho = 3f;

    private float posicionObjetivoX;

    void Start()
    {
        posicionObjetivoX = transform.position.x;
    }

    void Update()
    {
        float input = ObtenerInputHorizontal();

        posicionObjetivoX += input * velocidad * Time.deltaTime;
        posicionObjetivoX = Mathf.Clamp(posicionObjetivoX, limiteIzquierdo, limiteDerecho);

        Vector3 pos = transform.position;
        float nuevaX = Mathf.Lerp(pos.x, posicionObjetivoX, Time.deltaTime * suavizado);
        transform.position = new Vector3(nuevaX, pos.y, pos.z);
    }

    private float ObtenerInputHorizontal()
    {
        float input = 0f;

#if ENABLE_INPUT_SYSTEM
        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
                input -= 1f;
            if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
                input += 1f;
        }
#else
        input = Input.GetAxis("Horizontal");
#endif

        return input;
    }
}