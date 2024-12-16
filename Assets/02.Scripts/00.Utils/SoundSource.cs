using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//SFX(ȿ����) �߻���, ������Ʈ�� �߰��س����� �ʿ��ҋ� ȿ������ �����ų �� ����
//

public class SoundSource : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void SetClip(AudioClip clip, float volume, float pitchVariance)
    {
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
        audioSource.pitch = 1.0f + Random.Range(-pitchVariance, pitchVariance);

        StartCoroutine(DestroyAfterClipPlay(clip.length));
    }

    private IEnumerator DestroyAfterClipPlay(float time)
    {
        yield return new WaitForSeconds(time);
        audioSource.Stop();
        gameObject.SetActive(false);
    }
}
