using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class Gun : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Transform firePoint;
    [SerializeField] LineRenderer bulletTracer;
    [SerializeField] ParticleSystem muzzleFlash;
    [SerializeField] GameObject impactEffectPrefab;
    [SerializeField] InputActionReference shootAction;

    [Header("Audio")]
    [SerializeField] AudioClip gunShotSound;
    [SerializeField] AudioClip gunReloadSound;

    [Header("Disparo")]
    [SerializeField] float range = 100f;
    [SerializeField] float tracerDuration = 0.05f;
    [SerializeField] float impactEffectDuration = 1f;

    [Header("Municion")]
    [SerializeField] int magazineSize = 6;
    [SerializeField] int currentAmmo;

    [Header("Vibracion")]
    [Range(0f, 1f)]
    [SerializeField] float shootHapticAmplitude = 0.5f;
    [SerializeField] float shootHapticDuration = 0.08f;

    [Header("Retroceso")]
    [SerializeField] float recoilDistance = 0.03f;
    [SerializeField] float recoilKickDuration = 0.03f;
    [SerializeField] float recoilReturnDuration = 0.06f;

    Transform pistolModel;
    Vector3 pistolInitialPosition;

    AudioSource audioSource;
    HapticImpulsePlayer hapticImpulsePlayer;

    Coroutine tracerCoroutine;
    Coroutine recoilCoroutine;

    /// <summary>
    /// obtiene los componentes necesarios
    /// </summary>
    void Awake()
    {
        audioSource = GetComponentInChildren<AudioSource>();
        hapticImpulsePlayer = GetComponentInParent<HapticImpulsePlayer>();

        pistolModel = transform.Find("Pistol");
    }

    /// <summary>
    /// carga la municion inicial y guarda la posicion del modelo
    /// </summary>
    void Start()
    {
        currentAmmo = magazineSize;
        bulletTracer.enabled = false;

        if (pistolModel != null)
        {
            pistolInitialPosition = pistolModel.localPosition;
        }
    }

    /// <summary>
    /// revisa si se presiono el gatillo
    /// </summary>
    void Update()
    {
        if (shootAction.action.WasPressedThisFrame())
        {
            Shoot();
        }
    }

    /// <summary>
    /// dispara un raycast y consume una bala
    /// </summary>
    public void Shoot()
    {
        if (currentAmmo <= 0)
        {
            Debug.Log("sin municion");
            return;
        }

        currentAmmo--;

        audioSource.PlayOneShot(gunShotSound);
        muzzleFlash.Play();

        PlayShootHaptic();
        PlayRecoil();

        Vector3 endPoint = firePoint.position + firePoint.forward * range;

        Ray ray = new Ray(firePoint.position, firePoint.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, range))
        {
            endPoint = hit.point;

            ShowImpactEffect(hit);

            Debug.Log("impacto: " + hit.collider.name);
        }

        ShowTracer(endPoint);

        Debug.Log("municion: " + currentAmmo + "/" + magazineSize);
    }

    /// <summary>
    /// reproduce la vibracion del controlador al disparar
    /// </summary>
    void PlayShootHaptic()
    {
        if (hapticImpulsePlayer == null)
        {
            return;
        }

        hapticImpulsePlayer.SendHapticImpulse(
            shootHapticAmplitude,
            shootHapticDuration
        );
    }

    /// <summary>
    /// inicia el retroceso visual del arma
    /// </summary>
    void PlayRecoil()
    {
        if (pistolModel == null)
        {
            return;
        }

        if (recoilCoroutine != null)
        {
            StopCoroutine(recoilCoroutine);
            pistolModel.localPosition = pistolInitialPosition;
        }

        recoilCoroutine = StartCoroutine(RecoilRoutine());
    }

    /// <summary>
    /// mueve el modelo hacia atras y luego lo devuelve
    /// </summary>
    IEnumerator RecoilRoutine()
    {
        Vector3 startPosition = pistolInitialPosition;

        Vector3 recoilPosition =
            pistolInitialPosition + Vector3.back * recoilDistance;

        float elapsedTime = 0f;

        while (elapsedTime < recoilKickDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / recoilKickDuration;

            pistolModel.localPosition = Vector3.Lerp(
                startPosition,
                recoilPosition,
                progress
            );

            yield return null;
        }

        pistolModel.localPosition = recoilPosition;

        elapsedTime = 0f;

        while (elapsedTime < recoilReturnDuration)
        {
            elapsedTime += Time.deltaTime;

            float progress = elapsedTime / recoilReturnDuration;

            pistolModel.localPosition = Vector3.Lerp(
                recoilPosition,
                startPosition,
                progress
            );

            yield return null;
        }

        pistolModel.localPosition = startPosition;
        recoilCoroutine = null;
    }

    /// <summary>
    /// crea el efecto en el punto de impacto
    /// </summary>
    void ShowImpactEffect(RaycastHit hit)
    {
        Vector3 position = hit.point + hit.normal * 0.01f;
        Quaternion rotation = Quaternion.LookRotation(hit.normal);

        GameObject impactEffect = Instantiate(
            impactEffectPrefab,
            position,
            rotation
        );

        Destroy(impactEffect, impactEffectDuration);
    }

    /// <summary>
    /// muestra la linea del disparo por un corto tiempo
    /// </summary>
    void ShowTracer(Vector3 endPoint)
    {
        if (tracerCoroutine != null)
        {
            StopCoroutine(tracerCoroutine);
        }

        tracerCoroutine = StartCoroutine(TracerRoutine(endPoint));
    }

    /// <summary>
    /// dibuja y oculta la linea del disparo
    /// </summary>
    IEnumerator TracerRoutine(Vector3 endPoint)
    {
        bulletTracer.SetPosition(0, firePoint.position);
        bulletTracer.SetPosition(1, endPoint);

        bulletTracer.enabled = true;

        yield return new WaitForSeconds(tracerDuration);

        bulletTracer.enabled = false;
    }

    /// <summary>
    /// recarga el cargador
    /// </summary>
    public void Reload()
    {
        if (currentAmmo >= magazineSize)
        {
            return;
        }

        currentAmmo = magazineSize;

        audioSource.PlayOneShot(gunReloadSound);

        Debug.Log("arma recargada: " + currentAmmo + "/" + magazineSize);
    }
}
