using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : SingletonDDOL<SoundManager>
{
    private AudioSource audioSource;
    public float musicVolume;// { get; private set; }
    public float soundEffectVolume;// { get; private set; }

    [SerializeField] private AudioClip clipTest;

    private const string SOUND_POOL_KEY = "SoundEffects";
    private const int INITIAL_POOL_SIZE = 10;

    protected override void Awake()
    {
        base.Awake();

        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.loop = true;
        SetMusicVolume(musicVolume);

        InitializeSoundPool();
    }

    //���ٲ� BGM �ٲٱ�
    public void ChangeBGM(AudioClip clip)
    {
        audioSource.Stop();
        audioSource.clip = clip;
        audioSource.Play();
    }
    private void InitializeSoundPool()
    {
        ObjectPool soundPool = new ObjectPool("SoundEffect", INITIAL_POOL_SIZE, "Prefabs/Sample/AudioSource");
        ObjectPoolManager.Instance.AddPool(SOUND_POOL_KEY, soundPool);
    }

    //ȿ����
    public GameObject PlayClip(AudioClip clip)
    {
        ObjectPool pool = ObjectPoolManager.Instance.GetPool(SOUND_POOL_KEY, "SoundEffect");
        GameObject tempObj = pool.GetObject();
        tempObj.SetActive(true);
        SoundSource tempComp = tempObj.GetComponent<SoundSource>();
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
