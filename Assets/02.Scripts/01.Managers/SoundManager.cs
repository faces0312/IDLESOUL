using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonDDOL<SoundManager>
{
    private AudioSource audioSource;
    public float musicVolume { get; private set; }
    public float soundEffectVolume { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.loop = true;
    }

    //���ٲ� BGM �ٲٱ�
    public void ChangeBGM(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
    //ȿ����
    public GameObject PlayClip(AudioClip clip)
    {
        GameObject tempObj = new GameObject("SoundSource");
        SoundSource tempComp = tempObj.AddComponent<SoundSource>();
        //TODO :: new GameObject�� ObjectPool���� ������ �� �ְ�
        //AddComponent ��� GetComponent�� SoundSource�� ����
        //
        tempComp.SetClip(clip, soundEffectVolume, 0.1f);
        return tempObj;
    }
}
