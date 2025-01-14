using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundType
{
    Attack1 = 0, Attack2 = 1,
    Skill1= 2, Skill2= 3,
    Clear1= 4, Clear2= 5,
    Death1= 6, Death2= 7,
    PickUp = 8
}

public class PlayerSFXController : MonoBehaviour
{
    [SerializeField] private AudioClip[] playerSfxClips;//플레이어 효과음 컨테이너
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource footStepAudioSource;

    public Dictionary<SoundType, AudioClip> playerSfxClipDict = new Dictionary<SoundType, AudioClip>();

    private void Awake()
    {
        for (int i = 0; i < playerSfxClips.Length; i++)
        {
            playerSfxClipDict.Add((SoundType)i, playerSfxClips[i]);
        }
    }

    public void StopFootStepSFX()
    {
        footStepAudioSource.Stop();
    }

    public void PlayFootStepSFX()
    {
        if (!footStepAudioSource.isPlaying)
        {
            footStepAudioSource.Play();
        }
    }

    public void PlayClipSFXOneShot(SoundType indexSFX)
    {
        if(playerSfxClipDict.ContainsKey(indexSFX))
        {
            audioSource.PlayOneShot(playerSfxClipDict[indexSFX]);
        }
    }
}
