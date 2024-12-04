using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialChange : MonoBehaviour
{
    [SerializeField] private bool isParticleSystem;
    [SerializeField] private Material inputMaterial;
    private Material objectMaterial;
    private MeshRenderer meshRenderer;
    private ParticleSystemRenderer particleRenderer;

    [Header("Factors")]
    [SerializeField] private float timeToReduce;
    [SerializeField] private float reduceFactor = 0.0f;
    private float time;
    private float submitReduceFactor;
    private float cutOutFactor;
    [SerializeField] private float upFactor;
    private float calcUpFactor;
    private bool isupfactor = true;

    [Header("Destroy")]
    [SerializeField] private bool isDestroyGroup;
    [SerializeField] private GameObject destroyGroup;

    void Awake()
    {
        if (isParticleSystem)
        {
            particleRenderer = gameObject.GetComponent<ParticleSystemRenderer>();
            particleRenderer.material = inputMaterial;
            objectMaterial = particleRenderer.material;
        }
        else
        {
            meshRenderer = gameObject.GetComponent<MeshRenderer>();
            meshRenderer.material = inputMaterial;
            objectMaterial = meshRenderer.material;
        }
        submitReduceFactor = 0.0f;
        cutOutFactor = 1.0f;
    }

    void LateUpdate()
    {
        time += Time.deltaTime;
        if (time > timeToReduce)
        {
            cutOutFactor -= submitReduceFactor;
            submitReduceFactor = Mathf.Lerp(submitReduceFactor, reduceFactor, Time.deltaTime / 50);
        }

        cutOutFactor = Mathf.Clamp01(cutOutFactor);
        if (cutOutFactor <= 0 && time > timeToReduce)
        {
            if (!isDestroyGroup)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(destroyGroup);
            }
        }
        objectMaterial.SetFloat("_MaskCutOut", cutOutFactor);

        if (upFactor != 0 && isupfactor != false)
        {
            calcUpFactor += upFactor * Time.deltaTime;
            calcUpFactor = Mathf.Clamp01(calcUpFactor);
            objectMaterial.SetFloat("_MaskCutOut", calcUpFactor);
            if (calcUpFactor >= 1)
                isupfactor = false;
        }
    }
}
