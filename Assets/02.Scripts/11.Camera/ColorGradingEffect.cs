using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ColorGradingEffect
{
    private ColorGrading colorGrading;

    private WaitForSeconds yieldTime;
    private float yieldSeconds = 2f;

    private float duration = 1f;
    private float minValue = -75f;
    private float maxValue = 0f;
    private float rate = 75f;

    public ColorGradingEffect(PostProcessVolume volume)
    {
        volume.profile.TryGetSettings(out colorGrading);
        yieldTime = new WaitForSeconds(yieldSeconds);
    }

    public void SwordSlashEffect()
    {
        CoroutineHelper.Instance.StartCoroutineHelper(CoroutineSwordSlash());
    }

    IEnumerator CoroutineSwordSlash()
    {
        float startTime = Time.time;
        colorGrading.enabled.value = true;

        while (Time.time - startTime < duration)
        {
            colorGrading.saturation.value = Mathf.Clamp((startTime - Time.time) * rate, minValue, maxValue);
            yield return null;
        }

        yield return yieldTime;

        startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            colorGrading.saturation.value = Mathf.Clamp(minValue + (Time.time - startTime) * rate, minValue, maxValue);
            yield return null;
        }

        colorGrading.saturation.value = maxValue;
        colorGrading.enabled.value = false;
    }
}
