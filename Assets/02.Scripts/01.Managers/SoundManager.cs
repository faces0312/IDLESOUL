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
        Init();
        ChangeBGMForScene("TitleScene");
    }

    public void Init()
    {
        //audioSource = Resources.Load<AudioSource>("Prefebs/Sample/AudioSource"); // 1
        audioSource = gameObject.AddComponent<AudioSource>(); // 2
        audioSource.loop = true; //BGM�̱⿡ true
        audioSource.volume = 0.2f;
        audioSource.outputAudioMixerGroup = audioMixer.FindMatchingGroups("BGM")[0];

        SetBGMVolume(musicVolume);
        SetSoundEffectVolume(soundEffectVolume);
    }

    public void ChangeBGMForScene(string sceneName)
    {
        Debug.Log($"Changing BGM for scene: {sceneName}");
        if (sceneName == "LoadingScene")
        {
            StopBGM();
            return;
        }

        AudioClip newBGM = null;
        switch (sceneName)
        {
            case "TitleScene":
                newBGM = Resources.Load<AudioClip>("Sound/BGM_Title");
                break;
            case "GameScene_SMS":
                newBGM = Resources.Load<AudioClip>("Sound/BGM_Game");
                break;
            default:
                return;
        }

        if (newBGM != null)
        {
            ChangeBGM(newBGM);
        }
    }

    //���ٲ� BGM �ٲٱ�
    public void ChangeBGM(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }

    public void StopBGM()
    {
        audioSource.Stop();
    }

    //��ü���� ����(�����̴�)
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume;
        //masterVolume ������ 0.001~1������ ������
        //����� �ͼ��� 20 ~ -80 ������ ���̱� ����
        masterVolume = Mathf.Clamp(masterVolume, 0.001f, 1f);
        audioMixer.SetFloat("Master", Mathf.Log10(masterVolume) * 20);
    }

    //������� ����(�����̴�)
    public void SetBGMVolume(float volume)
    {
        musicVolume = volume;
        musicVolume = Mathf.Clamp(musicVolume, 0.001f, 1f);
        audioMixer.SetFloat("BGM", Mathf.Log10(musicVolume) * 20);
    }

    //ȿ���� ����(�����̴�)
    public void SetSoundEffectVolume(float volume)
    {
        soundEffectVolume = volume;
        soundEffectVolume = Mathf.Clamp(soundEffectVolume, 0.001f, 1f);
        audioMixer.SetFloat("SFX", Mathf.Log10(soundEffectVolume) * 20);
    }
}
