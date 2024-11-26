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

    //씬바뀔때 BGM 바꾸기
    public void ChangeBGM(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
    //효과음
    public GameObject PlayClip(AudioClip clip)
    {
        GameObject tempObj = new GameObject("SoundSource");
        SoundSource tempComp = tempObj.AddComponent<SoundSource>();
        //TODO :: new GameObject가 ObjectPool에서 접근할 수 있게
        //AddComponent 대신 GetComponent로 SoundSource에 접근
        //
        tempComp.SetClip(clip, soundEffectVolume, 0.1f);
        return tempObj;
    }

    //배경음악 조절
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
