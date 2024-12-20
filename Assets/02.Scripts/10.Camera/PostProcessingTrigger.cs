using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingTrigger
{
    ColorGradingEffect colorGradingEffect;
    VignetteEffect vignetteEffect;
    ChromaticAberrationEffect chromaticAberrationEffect;

    public PostProcessingTrigger(PostProcessVolume volume)
    {
        colorGradingEffect = new ColorGradingEffect(volume);
        vignetteEffect = new VignetteEffect(volume);
        chromaticAberrationEffect = new ChromaticAberrationEffect(volume);
    }

    public void SwordSlashEffect()
    {
        colorGradingEffect.SwordSlashEffect();
        vignetteEffect.SwordSlashEffect();
    }

    public void MeteorEffect()
    {
        chromaticAberrationEffect.MeteorEffect();
    }
}
