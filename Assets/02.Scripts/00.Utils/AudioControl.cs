using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        EndClip();
    }

    private void EndClip()
    {
        if (!audioSource.isPlaying)
        {
            gameObject.SetActive(false);
        }
    }
}
