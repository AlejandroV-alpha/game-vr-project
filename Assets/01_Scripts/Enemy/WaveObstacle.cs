using UnityEngine;
using System.Collections;

public class WaveObstacle : MonoBehaviour
{
    public Transform[] cubos;

    public float altura = 2f;
    public float velocidad = 2f;
    public float espera = 0.2f;

    private Vector3[] posicionesIniciales;
    private Coroutine rutina;

    void OnEnable()
    {
        posicionesIniciales = new Vector3[cubos.Length];
        for (int i = 0; i < cubos.Length; i++)
            posicionesIniciales[i] = cubos[i].localPosition; // LOCAL, no mundial

        if (rutina != null) StopCoroutine(rutina);
        rutina = StartCoroutine(MoverOla());
    }

    void OnDisable()
    {
        if (rutina != null) StopCoroutine(rutina);
    }

    IEnumerator MoverOla()
    {
        while (true)
        {
            for (int i = 0; i < cubos.Length; i++)
            {
                yield return StartCoroutine(MoverCubo(cubos[i], posicionesIniciales[i]));
                yield return new WaitForSeconds(espera);
            }
        }
    }

    IEnumerator MoverCubo(Transform cubo, Vector3 posicionInicial)
    {
        Vector3 arriba = posicionInicial + Vector3.up * altura;

        while (Vector3.Distance(cubo.localPosition, arriba) > 0.01f)
        {
            cubo.localPosition = Vector3.MoveTowards(cubo.localPosition, arriba, velocidad * Time.deltaTime);
            yield return null;
        }

        while (Vector3.Distance(cubo.localPosition, posicionInicial) > 0.01f)
        {
            cubo.localPosition = Vector3.MoveTowards(cubo.localPosition, posicionInicial, velocidad * Time.deltaTime);
            yield return null;
        }
    }
}