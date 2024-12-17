using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class ChromaticAberrationEffect
{
    private ChromaticAberration chromaticAberration;

    public ChromaticAberrationEffect(PostProcessVolume volume)
    {
        volume.profile.TryGetSettings(out chromaticAberration);
    }
}
