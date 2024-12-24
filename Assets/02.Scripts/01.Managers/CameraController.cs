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

    private void Awake()
    {
        // PostProcessing
        if (Camera.main.TryGetComponent(out PostProcessVolume volume))
        {
            postProcessingTrigger = new PostProcessingTrigger(volume);
        }
    }

    public void Initialize(Transform Follow , Transform LookAt)
    {        
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
        UIManager.Instance.ShowUI<UIBossSummonAlarmController>();

        if (virtualCamera != null)
        {
            virtualCamera.Follow = newFollowTr;
            virtualCamera.LookAt = newFollowTr;

            Invoke("ResetFollowTarget", closeUpTime);
        } 
    }

    public void ResetFollowTarget()
    {
        UIManager.Instance.HideUI<UIBossSummonAlarmController>();

        if (virtualCamera != null)
        {
            virtualCamera.Follow = GameManager.Instance.player.CamarePivot.transform;
            virtualCamera.LookAt = GameManager.Instance.player.transform;
            GameManager.Instance.player.enabled = true;
        }
    }

    public void ShakeCamera(CinemachineImpulseSource source)
    {
        source.GenerateImpulse();
    }

    public void SwordSlashEffect()
    {
        postProcessingTrigger.SwordSlashEffect();
    }

    public void MeteorEffect()
    {
        postProcessingTrigger.MeteorEffect();
    }
}

