using System;
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine.Rendering.PostProcessing;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    [SerializeField] private Camera minimapCamera;

    private PostProcessingTrigger postProcessingTrigger;

    public CinemachineVirtualCamera VirtualCamera { get => virtualCamera; }
    public Camera MinimapCamera { get => minimapCamera; }

    // TODO : 임시
    private void Awake()
    {
        // 포스트 프로세스 세팅
        if (Camera.main.TryGetComponent(out PostProcessVolume volume))
        {
            postProcessingTrigger = new PostProcessingTrigger(volume);
        }
    }

    public void SetMininapCamera()
    {

    }

    public void Initialize(Transform Follow , Transform LookAt)
    {
        //virtualCamera.Follow = GameManager.Instance._player.CamarePivot.transform;
        //virtualCamera.LookAt = GameManager.Instance._player.transform;
        
        virtualCamera.Follow = Follow;
        virtualCamera.LookAt = LookAt;

        // 포스트 프로세스 세팅
        if (Camera.main.TryGetComponent(out PostProcessVolume volume))
        {
            postProcessingTrigger = new PostProcessingTrigger(volume);
        }
    }

    public void ToggleFollowTarget(Transform newFollowTr , float closeUpTime)
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = newFollowTr;
            virtualCamera.LookAt = newFollowTr;

            Invoke("ResetFollowTarget", closeUpTime);
        }
    }

    public void ResetFollowTarget()
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = GameManager.Instance.player.CamarePivot.transform;
            virtualCamera.LookAt = GameManager.Instance.player.transform;
            GameManager.Instance.player.enabled = true;
        }
    }

    public void SwordSlashEffect()
    {
        postProcessingTrigger.SwordSlashEffect();
    }
}

