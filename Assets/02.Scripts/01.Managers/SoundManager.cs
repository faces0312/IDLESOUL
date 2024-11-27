using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : SingletonDDOL<SoundManager>
{
    public AudioMixer audioMixer;
    private AudioSource audioSource;

    public float masterVolume;// { get; private set; }
    public float musicVolume;// { get; private set; }
    public float soundEffectVolume;// { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true; 
    }

    private void Start()
    {
        SetBGMVolume(musicVolume);
        SetSoundEffectVolume(soundEffectVolume);
    }

    //씬바뀔때 BGM 바꾸기
    public void ChangeBGM(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    //전체음악 조절(슬라이더)
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        //masterVolume 범위를 0.001~1사이의 값으로
        //오디어 믹서는 20 ~ -80 사이의 값이기 때문
        masterVolume = Mathf.Clamp(masterVolume, 0.001f, 1f);
        audioMixer.SetFloat("Master", Mathf.Log10(masterVolume) * 20);
    }

    //배경음악 조절(슬라이더)
    public void SetBGMVolume(float volume)
    {
        musicVolume = volume;
        musicVolume = Mathf.Clamp(musicVolume, 0.001f, 1f);
        audioMixer.SetFloat("BGM", Mathf.Log10(musicVolume) * 20);
    }

    //효과음 조절(슬라이더)
    public void SetSoundEffectVolume(float volume)
    {
        soundEffectVolume = volume;
        soundEffectVolume = Mathf.Clamp(soundEffectVolume, 0.001f, 1f);
        audioMixer.SetFloat("SFX", Mathf.Log10(soundEffectVolume) * 20);
    }
}
