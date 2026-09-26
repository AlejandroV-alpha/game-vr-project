using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Haptics;

public class PlayerDamageFeedback : MonoBehaviour
{
    [Header("Flash")]
    [SerializeField] Image damageImage;
    [SerializeField] float flashAlpha = 0.25f;
    [SerializeField] float fadeDuration = 0.25f;

    [Header("Vibracion")]
    [SerializeField] HapticImpulsePlayer leftHaptic;
    [SerializeField] HapticImpulsePlayer rightHaptic;
    [Range(0f, 1f)]
    [SerializeField] float hapticAmplitude = 0.5f;
    [SerializeField] float hapticDuration = 0.1f;

    Coroutine flashCoroutine;

    /// <summary>
    /// reproduce el feedback al recibir dano
    /// </summary>
    public void PlayFeedback()
    {
        if (flashCoroutine != null)
        {
            StopCoroutine(flashCoroutine);
        }

        if (leftHaptic != null)
        {
            leftHaptic.SendHapticImpulse(hapticAmplitude, hapticDuration);
        }

        if (rightHaptic != null)
        {
            rightHaptic.SendHapticImpulse(hapticAmplitude, hapticDuration);
        }

        flashCoroutine = StartCoroutine(FlashCoroutine());
    }

    /// <summary>
    /// muestra el color rojo y lo desvanece
    /// </summary>
    IEnumerator FlashCoroutine()
    {
        Color color = damageImage.color;

        color.a = flashAlpha;
        damageImage.color = color;

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(flashAlpha, 0f, elapsedTime / fadeDuration);

            color.a = alpha;
            damageImage.color = color;

            yield return null;
        }

        color.a = 0f;
        damageImage.color = color;

        flashCoroutine = null;
    }
}
