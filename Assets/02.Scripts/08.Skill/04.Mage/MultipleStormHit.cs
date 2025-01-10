using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScottGarland;

public class MultipleStormHit : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    private float range;
    private BigInteger value;

    private float scalefactor;
    private LayerMask layerMask;

    private AudioClip clip;

    // Start is called before the first frame update
    private void Awake()
    {
        scalefactor = VariousEffectsScene.m_gaph_scenesizefactor;
        layerMask = 1 << LayerMask.NameToLayer("Enemy");

        clip = Resources.Load<AudioClip>("Sound/Skills/SFX_StormHit1");
    }

    private void Start()
    {
        InitSettings();
    }

    void LateUpdate()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * scalefactor);
    }

    private void InitSettings()
    {
        // MultipleStorm의 메서드를 호출해서 값을 넣어준다.
        if (transform.parent.transform.parent.TryGetComponent(out MultipleStorm component))
        {
            range = component.InitSettings(out value);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (Utils.IsInLayerMask(other.gameObject.layer, layerMask))
        {
            if (other.gameObject.TryGetComponent(out ITakeDamageAble damageable) && !damageable.IsInvulnerable)
            {
                var audioSource = ObjectPoolManager.Instance.GetPool(Const.AUDIO_SOURCE_KEY, Const.AUDIO_SOURCE_POOL_KEY).GetObject();
                audioSource.SetActive(true);
                AudioSource audio = audioSource.GetComponent<AudioSource>();
                audio.clip = clip;
                audio.Play();

                damageable.TakeDamage(Utils.CriticalCaculate(GameManager.Instance.player.StatHandler, value));
            }
        }
    }
}
