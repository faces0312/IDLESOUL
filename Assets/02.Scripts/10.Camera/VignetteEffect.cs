using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteEffect
{
    private Vignette vignette;

    private WaitForSeconds yieldTime;
    private float yieldSeconds = 2f;

    private float duration = 1f;
    private float minValue = 0f;
    private float maxRoundnessValue = 1f;
    private float maxIntensityValue = 0.45f;
    private float rate = 1.5f;

    public VignetteEffect(PostProcessVolume volume)
    {
        volume.profile.TryGetSettings(out vignette);
        yieldTime = new WaitForSeconds(yieldSeconds);
    }

    public void SwordSlashEffect()
    {
        CoroutineHelper.Instance.StartCoroutineHelper(CoroutineSwordSlash());
    }

    IEnumerator CoroutineSwordSlash()
    {
        float startTime = Time.time;
        vignette.enabled.value = true;

        while (Time.time - startTime < duration)
        {
            vignette.intensity.value = Mathf.Clamp((Time.time - startTime) * rate, minValue, maxIntensityValue);
            vignette.roundness.value = Mathf.Clamp((Time.time - startTime) * rate, minValue, maxRoundnessValue);
            yield return null;
        }

        yield return yieldTime;

        startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            vignette.intensity.value = Mathf.Clamp(1f - (Time.time - startTime), minValue, maxIntensityValue);
            vignette.roundness.value = Mathf.Clamp(1f - (Time.time - startTime), minValue, maxRoundnessValue);
            yield return null;
        }

        vignette.intensity.value = minValue;
        vignette.roundness.value = minValue;
        vignette.enabled.value = false;
    }
}
