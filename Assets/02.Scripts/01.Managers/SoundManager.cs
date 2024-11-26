using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonDDOL<SoundManager>
{
    private AudioSource audioSource;
    public float musicVolume;// { get; private set; }
    public float soundEffectVolume;// { get; private set; }

    [SerializeField] private AudioClip clipTest;

    protected override void Awake()
    {
        base.Awake();

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;
        SetMusicVolume(musicVolume);
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

    //������� ����
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        audioSource.volume = volume;
    }

    public void SetSoundEffectVolume(float volume)
    {
        soundEffectVolume = volume;
    }


    public void SoundTest()
    {
        GameObject soundObj = SoundManager.Instance.PlayClip(clipTest);
    }
}
