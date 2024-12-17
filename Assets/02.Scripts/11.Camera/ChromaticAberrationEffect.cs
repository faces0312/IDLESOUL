using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChromaticAberrationEffect
{
    private ChromaticAberration chromaticAberration;

    private WaitForSeconds yieldTime;
    private float yieldSeconds = 1.7f;

    private float duration = 1f;
    private float minValue = 0f;
    private float maxValue = 1f;
    private float rate = 1f;

    public ChromaticAberrationEffect(PostProcessVolume volume)
    {
        volume.profile.TryGetSettings(out chromaticAberration);

        yieldTime = new WaitForSeconds(yieldSeconds);
    }

    public void MeteorEffect()
    {
        CoroutineHelper.Instance.StartCoroutineHelper(CoroutineMeteor());
    }

    IEnumerator CoroutineMeteor()
    {
        float startTime = Time.time;
        chromaticAberration.enabled.value = true;

        while (Time.time - startTime < duration)
        {
            chromaticAberration.intensity.value = Mathf.Clamp((Time.time - startTime) * rate, minValue, maxValue);
            yield return null;
        }

        yield return yieldTime;

        startTime = Time.time;
        while (Time.time - startTime < duration)
        {
            chromaticAberration.intensity.value = Mathf.Clamp(1f - (Time.time - startTime), minValue, maxValue);
            yield return null;
        }

        chromaticAberration.intensity.value = minValue;
        chromaticAberration.enabled.value = false;
    }
}
